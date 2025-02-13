using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CategoriaEditModel : PageModel
{
    [BindProperty]
    public Categoria Categoria { get; set; }

    public IActionResult OnGet(int id)
    {
        // Ottenere la connessione al database
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per ottenere i dati della categoria da modificare
        var sql = "SELECT Id, Nome FROM Categorie WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        // Se la categoria esiste, carico i dati
        if (reader.Read())
        {
            Categoria = new Categoria
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1)
            };
        }
        else
        {
            return NotFound(); // Se la categoria non esiste, restituisco errore 404
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        // Se il modello non è valido, restituisco la pagina
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per aggiornare la categoria
        var sql = "UPDATE Categorie SET Nome = @nome WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", Categoria.Nome);
        command.Parameters.AddWithValue("@id", Categoria.Id);

        command.ExecuteNonQuery();

        return RedirectToPage("Categorie"); // Torno alla lista delle categorie
    }
}