using System.Collections.Generic;
using System.Linq;
using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.View.ConsoleView.Formatting;

public class ConsoleFormatter
{
    public List<string> FormatRecipe(Recipe recipe)
    {
        return recipe.GetIngredients()
            .Select(i => i.DisplayIngredient())
            .ToList();
    }

    public List<string> FormatRecipes(List<Recipe> recipes)
    {
        var result = new List<string>();
        for (int i = 0; i < recipes.Count; i++)
        {
            result.Add($"***** {i + 1} *****");
            result.AddRange(FormatRecipe(recipes[i]));
            
            if (i<recipes.Count-1)
                result.Add("");
        }
        return result;
    }

    public List<string> FormatAvaliableIngredients(List<Ingredient> ingredients)
    {
        return ingredients
            .Select(i => $"{i.GetId()}. {i.GetName()}")
            .ToList();
    }
}