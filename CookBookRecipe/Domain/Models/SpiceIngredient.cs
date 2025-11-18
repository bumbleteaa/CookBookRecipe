namespace CookBookRecipe.Domain.Models;
/*
 Aku buat semacam kategori pada rempah (spices)
 Karena mereka memiliki behavior yang sama, yaitu "Take a half a teaspoon"
 allow overengineering jika bahan menggunakan real world case (banyak jenis rempah)
*/
public abstract class SpiceIngredient : Ingredient 
{
    protected SpiceIngredient(int id, string name) : base(id, name, requiresPreparation: true) { }
    protected override string GetPreparations() => "Take half a teaspoon";
}