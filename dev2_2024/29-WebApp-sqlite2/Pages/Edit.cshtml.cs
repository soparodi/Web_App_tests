using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina
// using Microsoft.Extensions.Logging;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;

    [BindProperty]
    public Prodotto Prodotto { get; set; } = new(); // proprietà pubblica per contenere i dati del prodotto

    public List<SelectListItem> CategorieSelectList { get; set; } = new();

    // Costruttore per iniettare il logger (opzionale per debug)
    public EditModel(ILogger<EditModel> logger)
    {
        _logger = logger;
    }

    // passo l'id come parametro perché voglio modificare un prodotto esistente sul quale ho cliccato in precedenza
    public IActionResult OnGet(int id)
    {
        try
        {
            // Query per ottenere il prodotto con l'id passato come parametro
            var sql = "SELECT Id, Nome, Prezzo, CategoriaId FROM Prodotti WHERE Id = @id";

            var prodotto = UtilityDB.ExecuteReader(sql, command =>
            {
                command.Parameters.AddWithValue("@id", id);
            },
            reader => new Prodotto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
            });

            if (prodotto.Count == 0)
            {
                return NotFound();
            }

            Prodotto = prodotto[0]; // Assegno il primo (e unico) risultato alla proprietà

            CaricaCategorie(); // carico le categorie per la select list
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero del prodotto con ID {Id}", id);
            SimpleLogger.Log(ex);
            return StatusCode(500, "Errore interno del server");
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CaricaCategorie();
            return Page();
        }

        try
        {
            // Query per aggiornare il prodotto nel database
            var sql = "UPDATE Prodotti SET Nome = @nome, Prezzo = @prezzo, CategoriaId = @categoriaId WHERE Id = @id";

            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                command.Parameters.AddWithValue("@nome", Prodotto.Nome);
                command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
                command.Parameters.AddWithValue("@categoriaId", Prodotto.CategoriaId);
                command.Parameters.AddWithValue("@id", Prodotto.Id);
            });

            return RedirectToPage("Prodotti"); // reindirizza alla pagina dell'elenco prodotti
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'aggiornamento del prodotto con ID {Id}", Prodotto.Id);
            SimpleLogger.Log(ex);
            ModelState.AddModelError("", "Si è verificato un errore durante la modifica del prodotto.");
            CaricaCategorie();
            return Page();
        }
    }

    private void CaricaCategorie()
    {
        try
        {
            var sql = "SELECT Id, Nome FROM Categorie";

            CategorieSelectList = UtilityDB.ExecuteReader(sql, reader => new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(), // Converto in stringa l'ID per usarlo nel menu a tendina
                Text = reader.GetString(1)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il caricamento delle categorie");
            SimpleLogger.Log(ex);
            ModelState.AddModelError("", "Impossibile caricare le categorie.");
        }
    }
}