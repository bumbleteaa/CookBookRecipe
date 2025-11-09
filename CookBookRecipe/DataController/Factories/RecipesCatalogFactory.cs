using System;
using CookBookRecipe.DataController.Interfaces;
using CookBookRecipe.DataController.Catalogs;
using CookBookRecipe.Domain.Enumeration;
namespace CookBookRecipe.DataController.Factories;

public static class RecipesCatalogFactory
{
    public static IRecipesCatalog Create(FileFormat saveFormat)
    {
        switch (saveFormat)
        {
            case FileFormat.TXT:
                return new TxtRecipesCatalog("recipes.txt");
            case FileFormat.JSON:
                return new JsonRecipesCatalog("recipes.json");
            default:
                throw new ArgumentException($"FileFormat {saveFormat} is not supported");
        }
    }
}