using CookBookRecipe.Domain.Models;

/*
 * Buat interface supaya tidak repeating prosedur walaupun punya 2 file format
 */
namespace CookBookRecipe.DataController.Interfaces;
public interface IRecipesCatalog
{
    List<Recipe> LoadRecipes();
    void SaveRecipe(Recipe recipe);
}