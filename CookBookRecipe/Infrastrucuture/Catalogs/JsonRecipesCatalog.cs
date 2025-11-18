using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CookBookRecipe.Infrastrucuture.Interfaces;
using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.Infrastrucuture.Catalogs;

public class JsonRecipesCatalog : IRecipesCatalog
{
    private readonly string _filePath;

    public JsonRecipesCatalog(string filePath)
    {
        _filePath = filePath;
    }
    
    //Method untuk load atau tampilkan resep (jika sudah ada input resep dari user)
    public List<Recipe> LoadRecipes()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Recipe>();
        }
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
            var ids = idString.Split(','); //Tiap inputan baru otomatis split dengan comma (,)

            foreach (var idStr in ids)
            {
                if (int.TryParse(idStr.Trim(), out int id))
                {
                    var addedIngredient = IngredientsCatalog.GetIngredientById(id);
                    if (addedIngredient != null)
                    {
                        recipe.AddIngredient(addedIngredient); //Jika user memasukkan ID ingredient, maka penambahan ke resep diproses
                    }
                }
            }

            if (!recipe.IsEmpty)
            {
                recipes.Add(recipe); //Jika resep kosong, maka langsung buat resep baru sebagai perintah
            }
        }
        return recipes;
    }

    //Method untuk save recipe ke dalam catalog JSON
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