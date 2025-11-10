namespace CookBookRecipe.Domain.Models;
/*
 Aku buat semacam kategori pada rempah (spices)
 Karena mereka memiliki behavior yang sama, yaitu "Take a half of teaspoon"
 allow overengineering jika bahan menggunakan real world case (banyak jenis rempah)
*/
public abstract class SpiceIngredient : Ingredient 
{
    protected SpiceIngredient(int id, string name) : base(id, name) { }

    public override string GetInstructions()
    {
        return "Take a half teaspoon. Add to other ingredients.";
    }
}