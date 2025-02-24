using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class FornitoreCreateModel : PageModel
{
    [BindProperty] // Collega automaticamente i dati del form alla proprietà
    public Fornitore Fornitore { get; set; }

    public void OnGet()
    {
        // Il metodo OnGet serve solo per mostrare il form di creazione
    }

    public IActionResult OnPost()
    {
        // Controllo se il modello è valido
        if (!ModelState.IsValid)
        {
            return Page(); // Ritorna la pagina se i dati non sono validi
        }

        try
        {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();

            // Query SQL per inserire un nuovo fornitore
            var sql = "INSERT INTO Fornitori (Nome) VALUES (@nome)";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", Fornitore.Nome); // Corretto l'errore

            command.ExecuteNonQuery(); // Esegue l'inserimento nel database
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Errore durante l'inserimento del fornitore.");
            Console.WriteLine($"Errore: {ex.Message}");
            return Page(); // Mostra di nuovo la pagina con il messaggio di errore
        }

        // Reindirizza alla lista dei fornitori dopo l'inserimento
        return RedirectToPage("Fornitori");
    }
}