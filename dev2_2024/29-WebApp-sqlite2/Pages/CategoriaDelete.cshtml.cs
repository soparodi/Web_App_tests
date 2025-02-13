using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class CategoriaDeleteModel : PageModel
{
    public CategoriaViewModel Categoria { get; set; }

    public IActionResult OnGet(int id)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        var sql = "SELECT Id, Nome FROM Categorie WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            Categoria = new CategoriaViewModel
            {
                CategoriaId = reader.GetInt32(0),
                CategoriaNome = reader.GetString(1)
            };
        }
        else
        {
            return NotFound(); // Se la categoria non esiste, restituisco errore 404
        }

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Voglio che mi elimini la categoria anche se è già associata ad un prodotto, quindi imposto a NULL il CategoriaId nei prodotti già associati
        var updateSql = "UPDATE Prodotti SET CategoriaId = NULL WHERE CategoriaId = @id";
        using var updateCommand = new SqliteCommand(updateSql, connection);
        updateCommand.Parameters.AddWithValue("@id", id);
        updateCommand.ExecuteNonQuery();

        // Elimino la categoria
        var deleteSql = "DELETE FROM Categorie WHERE Id = @id";
        using var deleteCommand = new SqliteCommand(deleteSql, connection);
        deleteCommand.Parameters.AddWithValue("@id", id);
        deleteCommand.ExecuteNonQuery();

        return RedirectToPage("Categorie");
    }
}