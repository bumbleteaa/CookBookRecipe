namespace CookBookRecipe.Domain.Models;

//Buat ingredient as abstract class (tulis reason)
public abstract class Ingredient
{
    protected int Id { get; }
    protected string Name { get; }

    protected Ingredient(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    //Template method buat keeping structure display bahan
    //supaya tidak hard coding nama bahan dan intruksinya berulang kali
    public string DisplayIngredient()
    {
        return $"{Name}. {GetInstructions()}";
    }
    
    //Hal yang subclasses harus lakukan, allow override karena mereka punya behavior sendiri
    public abstract string GetInstructions();
    
    //Public accessor (kurang tahu apa itu)
    public int GetId() => Id;
    public string GetName() => Name;
}