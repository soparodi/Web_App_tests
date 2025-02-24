using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class FornitoreEditModel : PageModel
{
    [BindProperty]
    public Fornitore Fornitore { get; set; }

    public IActionResult OnGet(int id)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per ottenere il fornitore da modificare
        var sql = "SELECT Id, Nome FROM Fornitori WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        // Se il fornitore esiste, lo carico
        if (reader.Read())
        {
            Fornitore = new Fornitore
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1)
            };
        }
        else
        {
            return Page();
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Ritorna alla pagina se il modello non è valido
        }

        try
        {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();

            // Query per aggiornare il fornitore
            var sql = "UPDATE Fornitori SET Nome = @nome WHERE Id = @id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", Fornitore.Nome);
            command.Parameters.AddWithValue("@id", Fornitore.Id);

            // Esegui la query di aggiornamento
            command.ExecuteNonQuery();  // Aggiunto per eseguire l'aggiornamento
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Errore durante l'aggiornamento del fornitore.");
            Console.WriteLine($"Errore: {ex.Message}");
            return Page();
        }

        // Reindirizza alla pagina della lista dei fornitori
        return RedirectToPage("Fornitori");
    }
}