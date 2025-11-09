namespace CookBookRecipe.Domain.Models;

public abstract class SpiceIngredient : Ingredient 
{
    protected SpiceIngredient(int id, string name) : base(id, name) { }

    public override string GetInstructions()
    {
        return "Take a half teaspoon. Add to other ingredients.";
    }
}