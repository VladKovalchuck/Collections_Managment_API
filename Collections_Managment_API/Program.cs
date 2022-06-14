using CollectionsManagmentAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo{Title = "CollectionManagmentAPI", Version = "v1"});
});
builder.Services.AddBusiness();
builder.Services.AddDatabase(builder.Configuration);
/*builder.Services.AddDbContext<CollectionsDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("CollectionsDB")));*/

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "CollectionManagmentAPI"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.MapControllers();

app.Run();