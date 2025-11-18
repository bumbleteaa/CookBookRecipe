namespace CookBookRecipe.Domain.Models.Ingredients;

public class CocoaPowder : Ingredient
{
    public CocoaPowder() : base(8, "Cocoa Powder", requiresPreparation: false) { }
    //Jika requiredPreparation: false, GetPreparations seharusnya tidak akan dieksekusi
    //Jika tidak sengaja GerPreparation() di call di tempat lain, throw exception error
    protected override string GetPreparations() => throw new InvalidOperationException("Cocoa powder not supposed to have instruction");
}