using System;
using CookBookRecipe.Application;
using CookBookRecipe.Application.Services;
using CookBookRecipe.Infrastrucuture.Factories;
using CookBookRecipe.Infrastrucuture.Catalogs;
using CookBookRecipe.Domain.Enumeration;
using CookBookRecipe.View.ConsoleView;
using CookBookRecipe.View.ConsoleView.Formatting;
using CookBookRecipe.View.ConsoleView.Messages;

/*
 * Program.cs sebagai orchestrator atau pemanggil semua method ke dalam satu program
 * jika ada perubahan pada save file dan GUI, dapat disesuaikan dengan mudah tanpa merubah domain (ingredient & recipe)
 * dan logic savefile
 */

/*
 * What changed?
 * Program.cs sebagai composition root dengan satu method yaitu run
 * semua proses orchestrasi akan dihandle oleh CookBookRecipeApp.cs, jadi Program.cs should be cleaner entry point.
 */
namespace CookBookRecipe;

public class Program
{
    private const FileFormat Format = FileFormat.JSON; 
    
    //Main
    public static void Main(string[] args)
    {
        //Panggil dependencynya
        var catalog = RecipesCatalogFactory.Create(Format);
        var userInterface = new ConsoleViewIo();
        var recipeBuilder = new RecipeBuilder();
        var recipeValidator = new RecipeValidator();
        var ingredientProvider = new IngredientProvider();
        var recipeFormatter = new ConsoleFormatter();
        var messageProvider = new ConsoleMessages();
        
        var app = new CookBookRecipeApp(
            catalog,
            userInterface,
            recipeBuilder,
            recipeValidator, 
            ingredientProvider,
            recipeFormatter,
            messageProvider
            );

        app.Run(); //Jalankan method run dari class CookBookApp
    }
}