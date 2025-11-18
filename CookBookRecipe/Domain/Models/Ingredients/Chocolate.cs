namespace CookBookRecipe.Domain.Models.Ingredients;

public class Chocolate : Ingredient
{
    public Chocolate() : base(4, "Chocolate", requiresPreparation: true) { }
    protected override string GetPreparations() => "Melt in a water bath";
}