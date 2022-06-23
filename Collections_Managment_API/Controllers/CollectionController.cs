using System.Security.Claims;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Entity.Models.Collection;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]
[Authorize (Roles = "Admin")]
[SwaggerTag("Collection")]
[Route("Collection")]
public class CollectionController : Controller
{
    private readonly ICollectionService _collectionService;
    private readonly IUserService _userService;

    public CollectionController(ICollectionService collectionService, IUserService userService)
    {
        _collectionService = collectionService;
        _userService = userService;
    }

    [HttpGet("")]
    public ActionResult<List<CollectionModel>> GetAll()
    {
        var collections = _collectionService.GetAll();

        List<CollectionModel> resultCollections = new List<CollectionModel>();
        foreach (var collection in collections)
        {
            resultCollections.Add(collection.ConvertToCollectionModel());
        }

        return Ok(resultCollections);
    }

    [HttpGet("{skip:int}/{take:int}")]
    public ActionResult<List<CollectionModel>> GetRange(int skip, int take)
    {
        var collections = _collectionService.GetRange(skip, take);
        
        List<CollectionModel> resultCollections = new List<CollectionModel>();
        foreach (var collection in collections)
        {
            resultCollections.Add(collection.ConvertToCollectionModel());
        }

        return Ok(resultCollections);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CollectionModel>> GetById(int id)
    {
        var collection = await _collectionService.GetById(id);
        if (collection is null)
            return NotFound("Collection not found.");
        
        return Ok(collection.ConvertToCollectionModel());
    }

    [HttpPost("")]
    public async Task<ActionResult<CollectionModel>> Create(CollectionCreateModel createModel)
    {
        var name = this.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Name);

        var user = _userService.SearchByLogin(name.Value);
        
        var collection = new CollectionEntity()
        {
            Name = createModel.Name,
            Description = createModel.Description,
            Topic = createModel.Topic,
            UserId = user.Id
        };
        await _collectionService.Create(collection);

        return Ok(collection.ConvertToCollectionModel());
    }

    [HttpPut("")]
    public async Task<ActionResult<CollectionModel>> Update(CollectionModel updateModel)
    {
        var name = this.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Name);

        var user = _userService.SearchByLogin(name.Value);
        
        var collection = await _collectionService.GetById(updateModel.Id);
        
        if (user.Id != collection.UserId)
        {
            return BadRequest("Not your collection");
        }

        collection.Name = updateModel.Name;
        collection.Description = updateModel.Description;
        collection.Topic = updateModel.Topic;

        await _collectionService.Update(collection);
        
        return Ok(collection.ConvertToCollectionModel());
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> Delete(int id)
    {
        return await _collectionService.Delete(id);
    }

    [HttpGet("{name}")]
    public ActionResult<CollectionModel> SearchByName(string name)
    {
        var collection = _collectionService.SearchByName(name);
        if (collection is null)
            return NotFound("CollectionNotFound");

        return Ok(collection.ConvertToCollectionModel());
    }
}