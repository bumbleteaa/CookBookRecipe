using System;
using CookBookRecipe.DataController.Factories;
using CookBookRecipe.DataController.Catalogs;
using CookBookRecipe.Domain.Enumeration;
using CookBookRecipe.View.ConsoleView;


namespace CookBookRecipe;

public class Program
{
    private const FileFormat Format = FileFormat.JSON; 

    public static void Main(string[] args)
    {
        var catalogs = RecipesCatalogFactory.Create(Format);
        var consoleInterface = new RecipesConsoleView();

        var existingRecipes = catalogs.LoadRecipes();
        consoleInterface.PrintExistingRecipe(existingRecipes);
        
        consoleInterface.ShowMessage("Create a new cookie recipe! avaliable ingredient are:");
        var avaliableIngredients = IngredientsCatalog.GetIngredients();
        consoleInterface.PrintAvaliableIngredients(avaliableIngredients);
        
        var newRecipes = consoleInterface.ReadIngredientsUserInput();

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