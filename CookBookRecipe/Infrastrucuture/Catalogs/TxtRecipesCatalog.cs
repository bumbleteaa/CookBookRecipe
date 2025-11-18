using System;
using System.Collections.Generic;
using System.IO;
using CookBookRecipe.Infrastrucuture.Interfaces;
using CookBookRecipe.Domain.Models;

/*
 * Savefile resep dengan format txt, in the scenario jika json tidak digunakan
 */

namespace CookBookRecipe.Infrastrucuture.Catalogs;

public class TxtRecipesCatalog : IRecipesCatalog //Belongs to method yang ada di interface 
{
    private readonly string _filePath;
    
    public TxtRecipesCatalog(string filePath)
    {
        _filePath = filePath;
    }
    
    //Load atau tampilkan resep yang ada pada format txt
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
            var ids = line.Split(','); //setiap inputan di split menggunakan comma (,)

            foreach (var idStr in ids)
            {
                if (int.TryParse(idStr.Trim(), out int id))
                {
                    var inputIngredient = IngredientsCatalog.GetIngredientById(id);
                    if (inputIngredient != null)
                    {
                        recipe.AddIngredient(inputIngredient); //Jika user memasukkan id maka ingredient masuk ke recipe
                    }
                }
            }

            if (!recipe.IsEmpty)
            {
                recipes.Add(recipe); //Jika list resep kosong maka langsung return ke tambah resep
            }
        }
        return recipes;
    }
    
    //Method untuk save file txt setelah user input ingredient dengan id yang benar
    public void SaveRecipe(Recipe recipe)
    {
        var line = recipe.GetIngredientIds();
        File.AppendAllText(_filePath, line + Environment.NewLine);
    }
}