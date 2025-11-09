using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CookBookRecipe.DataController.Interfaces;
using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.DataController.Catalogs;

public class JsonRecipesCatalog : IRecipesCatalog
{
    private readonly string _filePath;

    public JsonRecipesCatalog(string filePath)
    {
        _filePath = filePath;
    }

    public List<Recipe> LoadRecipes()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Recipe>();
        }
        //idStrings & idString ambigu, coba cari cara lain
        var json = File.ReadAllText(_filePath);
        var recipeIdStrings = JsonSerializer.Deserialize<List<string>>(json);

        if (recipeIdStrings == null)
        {
            return new List<Recipe>();
        }
        
        var recipes = new List<Recipe>();

        foreach (var idString in recipeIdStrings)
        {
            var recipe = new Recipe();
            var ids = idString.Split(',');

            foreach (var idStr in ids)
            {
                if (int.TryParse(idStr.Trim(), out int id))
                {
                    var addedIngredient = IngredientsCatalog.GetIngredientById(id);
                    if (addedIngredient != null)
                    {
                        recipe.AddIngredient(addedIngredient);
                    }
                }
            }

            if (!recipe.IsEmpty)
            {
                recipes.Add(recipe);
            }
        }
        return recipes;
    }

    //Bikin var baru, jangan recipes, terlalu generic
    public void SaveRecipe(Recipe recipe)
    {
        var savedRecipes = new List<string>();

        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            savedRecipes= JsonSerializer.Deserialize<List<string>>(json)??new List<string>();
        }
        
        savedRecipes.Add(recipe.GetIngredientIds());
        
        var newJson = JsonSerializer.Serialize(savedRecipes);
        File.WriteAllText(_filePath, newJson);
    }
}