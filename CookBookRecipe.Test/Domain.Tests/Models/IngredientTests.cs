using Xunit;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.Domain.Models.Ingredients;

namespace CookBookRecipe.Test.Domain.Tests.Models;

public class IngredientTests
{
    //TEST SCENARIO 1: Memastikan abstract class FlourIngredient berjalan dengan baik
    [Fact]
    public void CoconutFlour_HasSameInstructionAsWheatFlour()
    {
        var wheat =  new CoconutFlour();
        var coconut = new CoconutFlour();
        
        //Cek, apakah behavior coconut dan wheat sama?
        Assert.Equal(wheat.GetInstructions(), coconut.GetInstructions());
    }
    
    //TEST SCENARIO 2: Memastikan abstract class SpiceIngredient berjalan dengan baik
    [Fact]
    public void Cardamom_HasSameInstructionAsCinnamon()
    {
        var cinnamon = new Cinnamon();
        var cardamom = new Cardamom();
        
        //Cek, apakah behavior cinnamon dan cardamom sama?
        Assert.Equal(cinnamon.GetInstructions(), cardamom.GetInstructions());
    }
    
    //TEST SCENARIO 3: Memastikan ID dan Name unique (tidak ada yang overwrite atau salah arragement)
    [Theory]
    [InlineData(1, "Wheat Flour")]
    [InlineData(2, "Coconut Flour")]
    [InlineData(3, "Butter")]
    [InlineData(4, "Chocolate")]
    [InlineData(5, "Sugar")]
    [InlineData(6, "Cardamom")]
    [InlineData(7, "Cinnamon")]
    [InlineData(8, "Cocoa Powder")]
    public void AllIngredients_HaveUniqueIds(int uniqueId, string ingredientName)
    {
        Ingredient ingredient = uniqueId switch
        {
            1 => new WheatFlour(),
            2 => new CoconutFlour(),
            3 => new Butter(),
            4 => new Chocolate(),
            5 => new Sugar(),
            6 => new Cardamom(),
            7 => new Cinnamon(),
            8 => new CocoaPowder(),
            _ => throw new ArgumentOutOfRangeException(nameof(uniqueId), uniqueId, null)
        };
        
        Assert.NotNull(ingredient);
        Assert.Equal(ingredientName, ingredient.GetName());
        Assert.Equal(uniqueId, ingredient.GetId());
    }
    
}