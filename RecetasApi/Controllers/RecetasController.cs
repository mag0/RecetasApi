using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RecetasApi.Data;
using RecetasApi.Entities;

namespace RecetasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IMongoCollection<Recipe> _recipes;

    public RecipeController(MongoDbService mongoDbService)
    {
        _recipes = mongoDbService.Database?.GetCollection<Recipe>("receta");
    }

    [HttpGet]
    public async Task<IEnumerable<Recipe>> Get()
    {
        return await _recipes.Find(FilterDefinition<Recipe>.Empty).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe?>> GetById(string id)
    {
        var filter = Builders<Recipe>.Filter.Eq(x => x.Id, id);
        var recipe = _recipes.Find(filter).FirstOrDefault();
        return recipe is not null ? Ok(recipe) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Post(Recipe recipe)
    {
        await _recipes.InsertOneAsync(recipe);
        return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Recipe recipe)
    {
        var filter = Builders<Recipe>.Filter.Eq(x => x.Id, recipe.Id);
        await _recipes.ReplaceOneAsync(filter, recipe);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var filter = Builders<Recipe>.Filter.Eq(x => x.Id, id);
        await _recipes.DeleteOneAsync(filter);
        return Ok();
    }
}


