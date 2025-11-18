using CookBookRecipe.Domain.Models;
using CookBookRecipe.Domain.Models.Ingredients;

namespace CookBookRecipe.Infrastrucuture.Catalogs;

/*
 * Memastikan ingredient di dalam urutan yang benar dan tidak berubah dimanapun
 */
public static class IngredientsCatalog
{
    private static readonly List<Ingredient> _ingredients = new List<Ingredient>
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };

    public static List<Ingredient> GetIngredients()
    {
        return _ingredients;
    }
    //Method untuk panggil ingredient dengan ID
    public static Ingredient GetIngredientById(int id)
    {
        return _ingredients.FirstOrDefault(i => i.GetId() == id);
    }
}