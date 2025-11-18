using CookBookRecipe.Domain.Models;
using CookBookRecipe.Infrastrucuture.Catalogs;

namespace CookBookRecipe.Application.Services;
/*
 * IngredientProvider berperan sebagai wrapper yang menyediakan daftar bahan makanan, tanpa mereka perlu tahu
 * bagaimana data tersebut disimpan atau diambil.
 */
public class IngredientProvider
{
    public List<Ingredient> GetIngredients()
    {
        return IngredientsCatalog.GetIngredients();
    }
}