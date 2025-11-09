namespace CookBookRecipe.Domain.Models;

//Aku buat semacam kategori pada tepung,
//Karena mereka memiliki behavior yang sama, yaitu "sieve"
public abstract class FlourIngredient : Ingredient
{
    protected FlourIngredient(int id, string name) : base(id, name){ }

    public override string GetInstructions()
    {
        return "Sieve. Add to other ingredients.";
    }
}