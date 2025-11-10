using System;
using CookBookRecipe.DataController.Factories;
using CookBookRecipe.DataController.Catalogs;
using CookBookRecipe.Domain.Enumeration;
using CookBookRecipe.View.ConsoleView;

/*
 * Program.cs sebagai orchestrator atau pemanggil semua method ke dalam satu program
 * jika ada perubahan pada save file dan GUI, dapat disesuaikan dengan mudah tanpa merubah domain (ingredient & recipe)
 * dan logic savefile
 */
namespace CookBookRecipe;

public class Program
{
    private const FileFormat Format = FileFormat.JSON; 

    public static void Main(string[] args)
    {
        var catalogs = RecipesCatalogFactory.Create(Format);
        var consoleInterface = new RecipesConsoleView(); //View dengan CLI

        var existingRecipes = catalogs.LoadRecipes();
        consoleInterface.PrintExistingRecipe(existingRecipes); //Print atau display reciep yang sudah ada
        
        consoleInterface.ShowMessage("Create a new cookie recipe! avaliable ingredient are:");
        var avaliableIngredients = IngredientsCatalog.GetIngredients();
        consoleInterface.PrintAvaliableIngredients(avaliableIngredients); //Display list bahan yang terdaftar
        
        var newRecipes = consoleInterface.ReadIngredientsUserInput(); //newRecipe adalah hasil inputan ingredient dari user
        
        //Jika user belum menambahkan apa-apa buat pesan bahwa resep tidak akan disimpan karena input kosong
        if (newRecipes.IsEmpty)
        {
            consoleInterface.ShowMessage("No ingredients were selected! Recipe will not saved");
        }
        else
        {
            consoleInterface.ShowMessage("Recipes were added successfully!");
            consoleInterface.PrintRecipe(newRecipes);
            catalogs.SaveRecipe(newRecipes);
        }
        
        consoleInterface.ShowMessage("Press any key to exit");
        Console.ReadKey();
    }
}