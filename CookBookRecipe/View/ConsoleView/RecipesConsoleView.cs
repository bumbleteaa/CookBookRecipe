using System;
using System.Collections.Generic;
using CookBookRecipe.DataController.Catalogs;
using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.View.ConsoleView;

public class RecipesConsoleView
{
    public void PrintExistingRecipe(List<Recipe> recipes)
    {
        if (recipes.Count == 0) return;
        
        Console.WriteLine("existing recipes are");
        Console.WriteLine();
        
        for (int i = 0; i < recipes.Count; i++)
        {
            Console.WriteLine($"***** {i + 1} *****");
            PrintRecipe(recipes[i]);
            if (i < recipes.Count - 1)
            {
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    
    //Method untuk menampilkan ingredient yang sudah tersedia
    public void PrintAvaliableIngredients(List<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            Console.WriteLine($"{ingredient.GetId()} {ingredient.GetName()}");
        }
    }

    public Recipe ReadIngredientsUserInput()
    {
        var recipe =  new Recipe();
        while (true)
        {
            Console.WriteLine("Enter recipe ID: ");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var ingredient = IngredientsCatalog.GetIngredientById(id);
                if (ingredient != null) //ganti local var
                {
                  recipe.AddIngredient(ingredient);  
                } 
            }
            else
            {
                break;
            }
        }
        return recipe;
    }

    public void PrintRecipe(Recipe recipe)
    {
        foreach (var ingredient in recipe.GetIngredients())
        {
            Console.WriteLine(ingredient.DisplayIngredient());
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}