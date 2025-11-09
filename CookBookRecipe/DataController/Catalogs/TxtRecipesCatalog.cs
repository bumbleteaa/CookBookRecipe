using System;
using System.Collections.Generic;
using System.IO;
using CookBookRecipe.DataController.Interfaces;
using CookBookRecipe.Domain.Models;


namespace CookBookRecipe.DataController.Catalogs;

public class TxtRecipesCatalog : IRecipesCatalog
{
    private readonly string _filePath;

    public TxtRecipesCatalog(string filePath)
    {
        _filePath = filePath;
    }

    public List<Recipe> LoadRecipes()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Recipe>();
        }
        
        var recipes = new List<Recipe>();
        var lines = File.ReadAllLines(_filePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var recipe = new Recipe();
            var ids = line.Split(',');

            foreach (var idStr in ids)
            {
                if (int.TryParse(idStr.Trim(), out int id))
                {
                    var ingredient = IngredientsCatalog.GetIngredientById(id);
                    if (ingredient != null)
                    {
                        recipe.AddIngredient(ingredient);
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

    public void SaveRecipe(Recipe recipe)
    {
        var line = recipe.GetIngredientIds();
        File.AppendAllText(_filePath, line + Environment.NewLine);
    }
}