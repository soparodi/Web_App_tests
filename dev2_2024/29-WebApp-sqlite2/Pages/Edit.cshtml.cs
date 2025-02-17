using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;

    [BindProperty]
    public Prodotto Prodotto { get; set; } = new();

    public List<SelectListItem> CategorieSelectList { get; set; } = new();

    public EditModel(ILogger<EditModel> logger)
    {
        _logger = logger;
    }

    // Metodo GET per recuperare il prodotto
    public IActionResult OnGet(int id)
    {
        try
        {
            // Query per ottenere il prodotto con l'ID passato
            var sql = "SELECT Id, Nome, Prezzo, CategoriaId FROM Prodotti WHERE Id = @id";

            var prodotti = UtilityDB.ExecuteReader(sql, command =>
            {
                // Aggiungi il parametro @id al comando
                command.Parameters.AddWithValue("@id", id);
            },
            reader => new Prodotto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
            });

            // Se non ci sono prodotti trovati, reindirizza alla lista
            if (prodotti.Count == 0)
            {
                return RedirectToPage("Prodotti");
            }

            // Assegna il primo prodotto trovato
            Prodotto = prodotti[0];

            // Carica le categorie per il menu a tendina
            CaricaCategorie();

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero del prodotto con ID {Id}", id);
            SimpleLogger.Log(ex);
            return RedirectToPage("Prodotti");
        }
    }

    // Metodo POST per aggiornare il prodotto
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CaricaCategorie();  // Ricarica le categorie se la validazione fallisce
            return Page();
        }

        try
        {
            // Esegui l'aggiornamento del prodotto nel database
            var sql = "UPDATE Prodotti SET Nome = @nome, Prezzo = @prezzo, CategoriaId = @categoriaId WHERE Id = @id";

            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                // Aggiungi i parametri al comando SQL
                command.Parameters.AddWithValue("@nome", Prodotto.Nome);
                command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
                command.Parameters.AddWithValue("@categoriaId", Prodotto.CategoriaId);
                command.Parameters.AddWithValue("@id", Prodotto.Id);
            });

            // Reindirizza alla lista dei prodotti dopo l'aggiornamento
            return RedirectToPage("Prodotti");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'aggiornamento del prodotto con ID {Id}", Prodotto.Id);
            SimpleLogger.Log(ex);
            ModelState.AddModelError("", "Si è verificato un errore durante la modifica del prodotto.");
            CaricaCategorie();  // Ricarica le categorie in caso di errore
            return Page();
        }
    }

    // Metodo per caricare le categorie nel menu a tendina
    public void CaricaCategorie()
    {
        using (var connection = DatabaseInitializer.GetConnection())
        {
            // aprire la connessione 
            connection.Open();

            // leggere la tabella categorie
            var sql = @" SELECT * FROM Categorie";

            using (var command = new SqliteCommand(sql, connection))
            {
                // mentre il reader legge
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // aggiungi nuovo oggetto SelectListItem con Value e Text
                        CategorieSelectList.Add(new SelectListItem
                        {
                            Value = reader.GetInt32(0).ToString(),
                            Text = reader.GetString(1)
                        });
                    }
                }
            }
        }
    }
}