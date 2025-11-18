using CookBookRecipe.Application.Services;
using CookBookRecipe.Infrastrucuture.Catalogs;
using CookBookRecipe.Infrastrucuture.Interfaces;
using CookBookRecipe.View.ConsoleView.Formatting;
using CookBookRecipe.View.ConsoleView.Messages;
using CookBookRecipe.View.Interface;

namespace CookBookRecipe.Application;

public class CookBookRecipeApp
{
    private readonly IRecipesCatalog _recipesCatalog;
    private readonly IUserInterface _userInterface;
    private readonly RecipeBuilder _recipeBuilder;
    private readonly RecipeValidator _recipeValidator;
    private readonly IngredientProvider _ingredientsProvider;
    private readonly ConsoleFormatter _consoleFormatter;
    private readonly ConsoleMessages _consoleMessages;

    public CookBookRecipeApp(
        IRecipesCatalog recipesCatalog,
        IUserInterface userInterface,
        RecipeBuilder recipeBuilder,
        RecipeValidator recipeValidator,
        IngredientProvider ingredientsProvider,
        ConsoleFormatter consoleFormatter,
        ConsoleMessages consoleMessages)
    {
        _recipesCatalog = recipesCatalog;
        _userInterface = userInterface;
        _recipeBuilder = recipeBuilder;
        _recipeValidator = recipeValidator;
        _ingredientsProvider = ingredientsProvider;
        _consoleFormatter = consoleFormatter;
        _consoleMessages = consoleMessages;
    }

    public void Run()
    {
        //Load and display existing recipe
        var existingRecipe = _recipesCatalog.LoadRecipes();
        var formattedRecipes = _consoleFormatter.FormatRecipes(existingRecipe);
        if (formattedRecipes.Count > 0)
        {
            _userInterface.Display(formattedRecipes);
            _userInterface.DisplayBlankLine();
        }

        //Display promt and avaliable ingredient
        _userInterface.Display(_consoleMessages.CreateRecipePrompt);
        var avaliableIngredients = _ingredientsProvider.GetIngredients();
        var formattedIngredients = _consoleFormatter.FormatAvaliableIngredients(avaliableIngredients);
        _userInterface.Display(formattedIngredients);

        //Collect user ingredient from ID
        var ingredientIds = CollectIngredientIds();

        //Build recipe from input user
        var newRecipe = _recipeBuilder.RecipeFromIds(ingredientIds);

        //Validate and save or turn error
        if (!_recipeValidator.IsNotEmptyRecipe(newRecipe))
        {
            _userInterface.Display(_consoleMessages.NoIngredients);
        }
        else
        {
            _userInterface.Display(_consoleMessages.RecipeAdded);
            var formattedRecipe = _consoleFormatter.FormatRecipe(newRecipe);
            _userInterface.Display(formattedRecipe);
            _recipesCatalog.SaveRecipe(newRecipe);
        }
        
        //Exit
        _userInterface.Display(_consoleMessages.ExitMessage);
        _userInterface.WaitForKeyPress();
    }
    private List<int> CollectIngredientIds()
    {
        var ingredientIds = new List<int>();

        while (true)
        {
            _userInterface.Display(_consoleMessages.IngredientSelectionPrompt);
            var input = _userInterface.ReadLine();

            if (int.TryParse(input, out int id))
            {
                ingredientIds.Add(id);
            }
            else
            {
                break;
            }
        }

        return ingredientIds;
    }
}