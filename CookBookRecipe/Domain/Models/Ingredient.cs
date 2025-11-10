namespace CookBookRecipe.Domain.Models;

/*
 * Ingredient adalah domain utama yang berupa komponen tunggat
 * dia memiliki ID dan Name yang unik, tetapi ada beberapa behavior yang berbeda
 * maka dari itu butuh template method supaya beberapa ingredient boleh override behavior mereka
 * lewat abstract class GetInstruction()
 */
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
    public int GetId() => Id;
    public string GetName() => Name;
}