using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.Extensions.Logging;

public class DeleteModel : PageModel
{
    private readonly ILogger<DeleteModel> _logger;

    [BindProperty]
    public ProdottoViewModel Prodotto { get; set; } = new ProdottoViewModel();

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    // Costruttore per iniettare il logger
    public DeleteModel(ILogger<DeleteModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet(int id)
    {
        try
        {
            var sql = "SELECT Id, Nome, Prezzo, CategoriaId FROM Prodotti WHERE id = @id";

            var prodotti = UtilityDB.ExecuteReader(sql, reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            },
            command =>
            {
                command.Parameters.AddWithValue("@id", id);
            });

            if (prodotti.Count == 0)
            {
                return RedirectToPage("Prodotti"); // Se il prodotto non esiste, torniamo alla lista
            }

            Prodotto = prodotti[0]; // Assegno il primo (e unico) risultato alla proprietà
            Id = Prodotto.Id; // Associo l'ID al BindProperty

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero del prodotto con ID {Id}", id);
            SimpleLogger.Log(ex);
            return RedirectToPage("Prodotti"); // In caso di errore, torniamo alla lista prodotti
        }
    }

    public IActionResult OnPost()
    {
        try
        {
            var sql = "DELETE FROM Prodotti WHERE id = @id";

            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                command.Parameters.AddWithValue("@id", Id);
            });

            return RedirectToPage("Prodotti");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'eliminazione del prodotto con ID {Id}", Id);
            SimpleLogger.Log(ex);
            return RedirectToPage("Prodotti"); // In caso di errore, torniamo alla lista
        }
    }
}