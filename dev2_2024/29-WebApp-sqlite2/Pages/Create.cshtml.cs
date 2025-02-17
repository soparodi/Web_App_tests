using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina
using Microsoft.Data.Sqlite;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;

    [BindProperty] // attributo (decorator) per collegare il metodo al form
    public Prodotto Prodotto { get; set; } = new(); // proprietà pubblica di tipo prodotto per contenere i dati del prodotto

    // Creo una lista di SelectListItem per contenere le categorie
    // Serve per poi creare un menu a tendina in HTML
    public List<SelectListItem> Categorie { get; set; } = new();

    // Costruttore per iniettare il logger (opzionale per debug)
    public CreateModel(ILogger<CreateModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        CaricaCategorie();
    }

    public IActionResult OnPost()
    {
        // Controllo se il modello è valido (cioè se i dati inseriti rispettano le regole di validazione)
        if (!ModelState.IsValid)
        {
            CaricaCategorie(); // Ricarico le categorie per il menu a tendina
            return Page(); // Se il modello non è valido, torno alla stessa pagina
        }

        try
        {
            // Query SQL per inserire un nuovo prodotto usando i parametri (per prevenire SQL injection)
            var sql = @"INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES (@nome, @prezzo, @categoria)";

            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                command.Parameters.AddWithValue("@nome", Prodotto.Nome);
                command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
                command.Parameters.AddWithValue("@categoria", Prodotto.CategoriaId);
            });

            return RedirectToPage("Prodotti"); // Reindirizza alla pagina dell'elenco prodotti
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'inserimento del prodotto");
            SimpleLogger.Log(ex); // Logga l'errore
            ModelState.AddModelError("", "Errore durante l'aggiunta del prodotto.");
            CaricaCategorie();
            return Page();
        }
    }

    // Metodo per caricare le categorie dal database
    private void CaricaCategorie()
    {
        try
        {
            var sql = "SELECT Id, Nome FROM Categorie";

            // Uso UtilityDB.ExecuteReader per semplificare la lettura dal database
            Categorie = UtilityDB.ExecuteReader(sql, reader => new SelectListItem
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