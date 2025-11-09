namespace CookBookRecipe.Domain.Models.Ingredients;

public class CocoaPowder : Ingredient
{
    public CocoaPowder() : base(8, "Cocoa Powder") { }

    public override string GetInstructions()
    {
        return "Add to other ingredients.";
    }
}