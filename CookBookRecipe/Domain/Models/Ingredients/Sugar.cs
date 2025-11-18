namespace CookBookRecipe.Domain.Models.Ingredients;

/*Mungkin sugar dapat menjadi category karena in real world case
 banyak jenis gula dalam pembuatan kue*/
public class Sugar : Ingredient
{
    public Sugar() : base(5, "Sugar", requiresPreparation: false) { }
   //Jika requiredPreparation: false, GetPreparations seharusnya tidak akan dieksekusi
   //Jika tidak sengaja GerPreparation() di call di tempat lain, throw exception error
    protected override string GetPreparations() => throw new InvalidOperationException("Sugar not supposed to have instruction");
}