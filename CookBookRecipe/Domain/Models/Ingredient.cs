namespace CookBookRecipe.Domain.Models;

/*
 * Ingredient adalah domain utama yang berupa komponen tunggat
 * dia memiliki ID dan Name yang unik, tetapi ada beberapa behavior yang berbeda
 * maka dari itu butuh template method supaya beberapa ingredient boleh override behavior mereka
 * lewat abstract class GetInstruction()
 */
public abstract class Ingredient
{
    public int Id { get; }
    public string Name { get; }
    public bool RequiresPreparation { get; } //Flag buat menentukan apakah ingredient punya instruksi, kalau kosong maka false dan langsung ke instruksi "Add to another ingredient"

    protected Ingredient(int id, string name,  bool requiresPreparation)
    {
        Id = id;
        Name = name;
        RequiresPreparation = requiresPreparation;
    }
    
    //Flag buat menentukan apakah ingredient punya instruksi, kalau kosong maka false dan langsung ke instruksi "Add to another ingredient"
    //Menghindari override yang kosong
    
    //Template method buat keeping structure display bahan
    //supaya tidak hard coding nama bahan dan intruksinya berulang kali
    public string DisplayIngredient()
    {
        return $"{Name}. {CompleteInstructions()}"; //Change: CompleteInstruction terdiri dari unique instruction + generic instruction
    }
    
    //Contain unique instruction + repeating generic instruction
    public string CompleteInstructions() 
    {
        if (!RequiresPreparation) //jika ingredient tidak memiliki preparation, maka langsung ke Add another ingredient
        {
            return $"Add to other ingredients.";
        }
        return $"{GetPreparations()}. Add to ingredients."; //kondisi ini jalan jika ingredient punya prep instruction
    }
    
    //Hal yang subclasses harus lakukan, allow override karena mereka punya behavior sendiri
    protected abstract string GetPreparations(); //abstrak class untuk subclass instruksi yang identik
    public int GetId() => Id;
    public string GetName() => Name;
}