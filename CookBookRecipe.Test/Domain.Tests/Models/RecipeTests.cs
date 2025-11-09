using Xunit;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.Domain.Models.Ingredients;

namespace CookBookRecipe.Test.Domain.Tests.Models;

public class RecipeTests
{
    [Fact]
    public void Constructor_CreateEmptyRecipe()
    {
        var recipe = new Recipe();
        
        Assert.True(recipe.IsEmpty);
        Assert.Empty(recipe.GetIngredients());
    }

    [Fact]
    public void AddIngredient_WithValidIngredient_IncreasesCount()
    {
        var recipe = new Recipe();
        var ingredient = new WheatFlour();
        
        recipe.AddIngredient(ingredient);
        
        Assert.False(recipe.IsEmpty);
        Assert.Single(recipe.GetIngredients());
    }

    [Fact]
    public void AddIngredient_WithMultipleIngredients_MaintainsOrder()
    {
        var recipe = new Recipe();
        var wheat = new WheatFlour();
        var butter =  new Butter();
        var sugar = new Sugar();
        
        recipe.AddIngredient(wheat);
        recipe.AddIngredient(butter);
        recipe.AddIngredient(sugar);
        
        var ingredients = recipe.GetIngredients();
        Assert.Equal(3, ingredients.Count);
        Assert.Equal(wheat.GetId(), ingredients[0].GetId());
        Assert.Equal(butter.GetId(), ingredients[1].GetId());
        Assert.Equal(sugar.GetId(), ingredients[2].GetId());
    }

    [Fact]
    public void AddIngredient_WithDuplicateIngredients_AllowDuplicates()
    {
        var recipe = new Recipe();
        var wheat = new WheatFlour();
        
        recipe.AddIngredient(wheat);
        recipe.AddIngredient(wheat);
        
        Assert.Equal(2, recipe.GetIngredients().Count);
    }
    
    [Fact]
    public void AddIngredient_WithMultipleIngredients_ReturnCommaSeparatedString()
    {
        var recipe = new Recipe();
        recipe.AddIngredient(new WheatFlour());
        recipe.AddIngredient(new Butter());
        recipe.AddIngredient(new Sugar());

        var ids = recipe.GetIngredientIds();
        
        Assert.Equal("1,3,5", ids);
    }
    
    [Fact]
    public void GetIngredients_ReturnReadonlyCollection()
    {
        var recipe = new Recipe();
        recipe.AddIngredient(new WheatFlour());
        
        var ingredients = recipe.GetIngredients();
        
        Assert.IsAssignableFrom<System.Collections.Generic.IReadOnlyList<Ingredient>>(ingredients);;
    }

    [Fact]
    public void IsEmpty_WithNoIngredients_ReturnsTrue()
    {
        var recipe = new Recipe();
        
        Assert.True(recipe.IsEmpty);
    }

    [Fact]
    public void IsEmpty_WithIngredients_ReturnsFalse()
    {
        var recipe = new Recipe();
        recipe.AddIngredient(new Sugar());
        
        Assert.False(recipe.IsEmpty);
    }
    
}