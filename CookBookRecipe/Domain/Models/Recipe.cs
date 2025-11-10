using System.Collections.Generic;
using System.Linq;

namespace CookBookRecipe.Domain.Models;
/*
 * Domain recipe, yang terdiri dari beberapa ingredient yang diassemnly
 * as a container of ingredient component
 * jadi recipe adalah wadah dari ingredient
 */
public class Recipe
{
    private readonly List<Ingredient> _ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }
    
    //Membuat return read only buat proteksi internal state Ingredient
    public IReadOnlyList<Ingredient> GetIngredients()
    {
        return _ingredients.AsReadOnly();
    }
    
    public bool IsEmpty => _ingredients.Count == 0;
    
    /*
     * Resep otomatis menggunakan separation comma (,) setiap ingredient masuk
     */
    public string GetIngredientIds()
    {
        return string.Join(",", _ingredients.Select(i => i.GetId()));
    }
}