using System.Collections.Generic;
using System.Linq;

namespace CookBookRecipe.Domain.Models;

public class Recipe
{
    private readonly List<Ingredient> _ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public IReadOnlyList<Ingredient> GetIngredients()
    {
        return _ingredients.AsReadOnly();
    }
    
    public bool IsEmpty => _ingredients.Count == 0;

    public string GetIngredientIds()
    {
        return string.Join(",", _ingredients.Select(i => i.GetId()));
    }
}