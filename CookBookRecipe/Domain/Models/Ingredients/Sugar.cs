namespace CookBookRecipe.Domain.Models.Ingredients;

/*Mungkin sugar dapat menjadi category karena in real world case
 banyak jenis gula dalam pembuatan kue*/
public class Sugar : Ingredient
{
    public Sugar() : base(5, "Sugar") { }

    public override string GetInstructions()
    {
        return "Add to other ingredients.";
    }
}