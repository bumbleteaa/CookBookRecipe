using CookBookRecipe.Domain.Models;

namespace CookBookRecipe.Application.Services;
/*
 * Class ini berguna untuk menjaga rules:
 * Jika resep kosong, maka automatis buat resep baru, bukan menamilkan resep kosong
 */
public class RecipeValidator
{
    public bool IsNotEmptyRecipe(Recipe recipe)
    {
        //Resep harus tidak boleh kosong jika ingin ditampilkan
        return !recipe.IsEmpty; //return jika IsEmpty state adalah false
    }
}