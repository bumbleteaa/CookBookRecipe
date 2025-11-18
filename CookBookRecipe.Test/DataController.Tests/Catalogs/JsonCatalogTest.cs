using System;
using System.IO;
using Xunit;
using CookBookRecipe.Infrastrucuture.Catalogs;
using CookBookRecipe.Domain.Models;
using CookBookRecipe.Domain.Models.Ingredients;

namespace CookBookRecipe.Test.DataController.Tests.Catalogs;

public class JsonCatalogTest : IDisposable
{
    private readonly string _filePathTesting;
    private JsonRecipesCatalog  _jsonCatalog;

    public JsonCatalogTest()
    {
        _filePathTesting = $"test_recipe_{Guid.NewGuid()}.json";
        _jsonCatalog = new JsonRecipesCatalog(_filePathTesting);
    }
    
    //Method untuk hapus json setelah testing
    public void Dispose()
    {
        if (File.Exists(_filePathTesting))
        {
            File.Delete(_filePathTesting);
        }
    }
    
    /*
     * SKENARIO 1 -Jika belum ada file json(karena belum input resep)
     * apakah list bersih dan kosong?
     */
    [Fact]
    public void LoadRecipes_WhenJsonFileDoesNotExist_ReturnEmptyList()
    {
        var recipes = _jsonCatalog.LoadRecipes();
        
        Assert.Empty(recipes);
    }
    
    /*
     * SKENARIO 2 - Apakah format json benar terpakai?
     */
    [Fact]
    public void SaveRecipe_CreateCorrectFileFormat()
    {
        //Arrange sample testing
        var recipe = new Recipe();
        recipe.AddIngredient(new WheatFlour());
        recipe.AddIngredient(new Sugar());
        
        //Act trying to save file
        _jsonCatalog.SaveRecipe(recipe);
        
        //Assert, apakah sudah sesuai dengan skenario?
        Assert.True(File.Exists(_filePathTesting));
        var json = File.ReadAllText(_filePathTesting);
        Assert.Contains("1,5", json); //Apakah ID sudah sesuai dengan sample input?
    }
    
    /*
     * SKENARIO 2 - Jika user pilih ID yang invalid, apakah
     * accidently tersimpan oleh json?
     */
    [Fact]
    public void LoadRecipeWithValidId_SkipInvalidIngredients()
    {
        //Arrange - Input beberapa recipe ID
        File.WriteAllText(_filePathTesting, "[\"1,28,5\"]"); //28 invalid, apakah terismpan?
        
        //Act - Load resep yang tersimpan dari Arrange
        var recipes = _jsonCatalog.LoadRecipes();
        
        //Assert - Apakah invalid ID dapat tersimpan?
        Assert.Single(recipes);
        Assert.Equal(2, recipes[0].GetIngredients().Count); //Make sure valid ID yang tertampil
    }
}