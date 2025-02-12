using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DetailsModel : PageModel
{
    [BindProperty]
    public Prodotto Prodotto { get; set; }

    public string CategoriaNome { get; set; } = "Nessuna";

    public IActionResult OnGet(int id)
    {
        // Ottenere la connessione al database
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per ottenere il prodotto e il nome della categoria
        var sql = @"
            SELECT p.Id, p.Nome, p.Prezzo, p.CategoriaId, c.Nome as CategoriaNome
            FROM Prodotti p
            LEFT JOIN Categorie c ON p.CategoriaId = c.Id
            WHERE p.Id = @id";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Prodotto = new Prodotto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
            };

            CategoriaNome = reader.IsDBNull(4) ? "Nessuna" : reader.GetString(4);
        }
        else
        {
            return NotFound();
        }

        return Page();
    }
}
