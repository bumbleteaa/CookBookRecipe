namespace CookBookRecipe.Domain.Models;

/*
 Aku buat semacam kategori pada tepung
 Karena mereka memiliki behavior yang sama, yaitu "sieve"
 allow overengineering jika bahan menggunakan real world case (banyak jenis tepung)
*/
public abstract class FlourIngredient : Ingredient
{
    protected FlourIngredient(int id, string name) : base(id, name,  requiresPreparation: true) { }
    protected override string GetPreparations() => "Sieve";
}