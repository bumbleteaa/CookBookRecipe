using System.Collections.Generic;
using CookBookRecipe.Infrastrucuture.Catalogs;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.Infrastrucuture.Interfaces;
using CookBookRecipe.View.Interface;

namespace CookBookRecipe.Application.Services;

//Responsibility : Build recipe from ingredient IDs
public class RecipeBuilder
{
    public Recipe RecipeFromIds(List<int> ingredientIds)
    {
        var recipe = new Recipe();

        foreach (var id in ingredientIds)
        {
            var ingredient = IngredientsCatalog.GetIngredientById(id);
            if (ingredient != null)
            {
                recipe.AddIngredient(ingredient);
            }
        }
        return recipe;
    }
}