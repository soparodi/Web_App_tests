using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina

public class DeleteModel : PageModel
{
    public ProdottoViewModel Prodotto { get; set; }
    public IActionResult OnGet(int id)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        var sql = @"
        SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
        FROM Prodotti p
        LEFT JOIN Categorie c ON p.CategoriaId = c.Id
        WHERE p.Id = @id";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        // eseguo il comando e ottengo un reader che è un oggetto che mi permette di leggere i dati
        using var reader = command.ExecuteReader();

        // se il reader ha dati
        if (reader.Read())
        {
            Prodotto = new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna" : reader.GetString(3)
            };
        }
        else
        {
            return NotFound();
        }
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        var sql = "DELETE FROM Prodotti WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
        return RedirectToPage("Prodotti");
    }
}