using System;
using System.IO;
using Xunit;
using CookBookRecipe.DataController.Catalogs;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.Domain.Models.Ingredients;

namespace CookBookRecipe.Test.DataController.Tests.Catalogs;

/*
 * Sama seperti unit testing untuk file .json
 * hanya dirubah format filenya
 * untuk memastikan txt file juga run properly
 */
public class TxtCatalogTest : IDisposable
{
    private readonly string _filePathTesting;
    private TxtRecipesCatalog  _txtCatalog;

    public TxtCatalogTest()
    {
        _filePathTesting = $"test_recipe_{Guid.NewGuid()}.txt";
        _txtCatalog = new TxtRecipesCatalog(_filePathTesting);
    }
    
    //Method untuk hapus txt setelah testing
    public void Dispose()
    {
        if (File.Exists(_filePathTesting))
        {
            File.Delete(_filePathTesting);
        }
    }
    
    /*
     * SKENARIO 1 -Jika belum ada file txt(karena belum input resep)
     * apakah list bersih dan kosong?
     */
    [Fact]
    public void LoadRecipes_WhenTxtFileDoesNotExist_ReturnEmptyList()
    {
        var recipes = _txtCatalog.LoadRecipes();
        
        Assert.Empty(recipes);
    }
    
    /*
     * SKENARIO 2 - Apakah format .txt benar terpakai?
     */
    [Fact]
    public void SaveRecipe_CreateCorrectFileFormat()
    {
        //Arrange sample testing
        var recipe = new Recipe();
        recipe.AddIngredient(new WheatFlour());
        recipe.AddIngredient(new Sugar());
        
        //Act trying to save file
        _txtCatalog.SaveRecipe(recipe);
        
        //Assert, apakah sudah sesuai dengan skenario?
        Assert.True(File.Exists(_filePathTesting));
        var txt = File.ReadAllText(_filePathTesting);
        Assert.Contains("1,5", txt); //Apakah ID sudah sesuai dengan sample input?
    }
    
}