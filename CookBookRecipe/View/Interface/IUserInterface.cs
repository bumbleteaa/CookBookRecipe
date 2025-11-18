namespace CookBookRecipe.View.Interface;

public interface IUserInterface
{
    //Write operations
    void Display(string message);
    void Display(List<string> lines);
    void DisplayBlankLine();
    //Read operations
    string ReadLine();
    void WaitForKeyPress();
}