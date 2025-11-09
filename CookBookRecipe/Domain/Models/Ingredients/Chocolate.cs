namespace CookBookRecipe.Domain.Models.Ingredients;

public class Chocolate : Ingredient
{
    public Chocolate() : base(4, "Chocolate") { }

    public override string GetInstructions()
    {
        return "Melt in a water bath. Add to other ingredients.";
    }
}