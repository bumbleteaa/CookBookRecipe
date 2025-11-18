using System;
using System.Collections.Generic;
using CookBookRecipe.Infrastrucuture.Catalogs;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.View.Interface;

namespace CookBookRecipe.View.ConsoleView;
/*
 * View console terpusat pada satu file, jika akan diganti dengan GUI
 * dapat membuat class view GUI yang di-orchestrate ke program.css
 */
public class ConsoleViewIo : IUserInterface
{
    public void Display(string message)
    {
        Console.WriteLine(message);
    }

    public void Display(List<string> lines)
    {
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }

    public void DisplayBlankLine()
    {
        Console.WriteLine();
    }

    public string ReadLine()
    {
      return Console.ReadLine();
    }

    public void WaitForKeyPress()
    {
        Console.ReadKey();
    }
}