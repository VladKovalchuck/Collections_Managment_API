using System.Security.Claims;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Entity.Models.Collection;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]

[SwaggerTag("Collection")]
[Authorize]
[Route("[controller]")]
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
    [Authorize (Roles = "User")]
    public ActionResult<List<CollectionModel>> GetAll()
    {
        return Ok(_collectionService.GetAll());
    }

    [HttpGet("{skip:int}/{take:int}")]
    [Authorize (Roles = "User")]
    public ActionResult<List<CollectionModel>> GetRange(int skip, int take)
    {
        return Ok(_collectionService.GetRange(skip, take));
    }

    [HttpGet("{id:int}")]
    [Authorize (Roles = "User")]
    public async Task<ActionResult<CollectionModel>> GetById(int id)
    {
        var collection = await _collectionService.GetById(id);
        if (collection is null)
            return NotFound("Collection not found.");
        
        return Ok(collection);
    }

    [HttpPost("")]
    [Authorize (Roles = "User")]
    public async Task<ActionResult<CollectionModel>> Create(CollectionModel createModel)
    {
        var collection = await _collectionService.Create(createModel);

        return Ok(collection);
    }

    [HttpPut("")]
    [Authorize (Roles = "User, Admin")]
    public async Task<ActionResult<CollectionModel>> Update(CollectionModel updateModel)
    {
        await _collectionService.Update(updateModel);

        return Ok(updateModel);
    }

    [HttpDelete("{id:int}")]
    [Authorize (Roles = "User")]
    public async Task<bool> Delete(int id)
    {
        return await _collectionService.Delete(id);
    }

    [HttpGet("{name}")]
    [Authorize (Roles = "User")]
    public ActionResult<CollectionModel> SearchByName(string name)
    {
        var collection = _collectionService.SearchByName(name);
        if (collection is null)
            return NotFound("CollectionNotFound");

        return Ok(collection);
    }
}