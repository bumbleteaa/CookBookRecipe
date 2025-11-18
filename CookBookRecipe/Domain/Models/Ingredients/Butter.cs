namespace CookBookRecipe.Domain.Models.Ingredients;

public class Butter : Ingredient
{
    public Butter() : base(3, "Butter",  requiresPreparation: true) { }
    protected override string GetPreparations() => "Melt on low heat";
}