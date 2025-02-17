using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.Extensions.Logging;

public class DeleteModel : PageModel
{
    private readonly ILogger<DeleteModel> _logger;

    public ProdottoViewModel Prodotto { get; set; }

    // Costruttore per iniettare il logger
    public DeleteModel(ILogger<DeleteModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet(int id)
    {
        try
        {
            var sql = @"
            SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
            FROM Prodotti p
            LEFT JOIN Categorie c ON p.CategoriaId = c.Id
            WHERE p.Id = @id";

            var result = UtilityDB.ExecuteReader(sql, command =>
            {
                command.Parameters.AddWithValue("@id", id);
            },
            reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna" : reader.GetString(3)
            });

            if (result.Count == 0)
            {
                return RedirectToPage("Prodotti"); // Se il prodotto non esiste, torniamo alla lista
            }

            Prodotto = result[0]; // Assegno il primo (e unico) risultato alla proprietà
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero del prodotto con ID {Id}", id);
            SimpleLogger.Log(ex);
            return RedirectToPage("Prodotti"); // In caso di errore, reindirizziamo alla lista prodotti
        }
    }

    public IActionResult OnPost(int id)
    {
        try
        {
            var sql = "DELETE FROM Prodotti WHERE Id = @id";

            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                command.Parameters.AddWithValue("@id", id);
            });

            return RedirectToPage("Prodotti");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'eliminazione del prodotto con ID {Id}", id);
            SimpleLogger.Log(ex);
            return RedirectToPage("Prodotti"); // In caso di errore, torniamo alla lista
        }
    }
}