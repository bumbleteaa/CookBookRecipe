namespace CookBookRecipe.Domain.Models.Ingredients;

public class Butter : Ingredient
{
    public Butter() : base(3, "Butter") { }
    
    //Override perilaku lewat method GetInstruction
    public override string GetInstructions()
    {
        return "Melt on low heat. Add to other ingredients.";
    }
}