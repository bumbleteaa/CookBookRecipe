using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.DataController.Interfaces;
public interface IRecipesCatalog
{
    List<Recipe> LoadRecipes();
    void SaveRecipe(Recipe recipe);
}