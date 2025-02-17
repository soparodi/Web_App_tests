using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class ProdottiModel : PageModel
{
    // Creo una proprietà pubblica di tipo lista di prodotti view model
    public List<ProdottoViewModel> Prodotti { get; set; } = new List<ProdottoViewModel>(); // la inizializzo come una lista vuota

    public void OnGet()
    {
        // Invoco il metodo GetConnection per ottenere la connessione al db
        using var connection = DatabaseInitializer.GetConnection();
        // Apro la connessione
        connection.Open();

        // Creo la query SQL per ottenere i dati dei prodotti
        // Voglio accedere al nome della categoria quindi devo fare una join tra la tabella prodotti e la tabella categorie
        // Uso JOIN per ottenere i prodotti con categoria
        // Uso LEFT JOIN per ottenere anche i prodotti che non hanno categorie associate
        // Posso usare p e c come alias per le tabelle prodotti e categorie se voglio usare i nomi completi delle tabelle uso Prodotti e Categorie
        // Il vantaggio di usare gli alias è che dopo posso usare p e c per accedere ai campi delle tabelle
        var sql = @"
        SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
        FROM Prodotti p
        LEFT JOIN Categorie c ON p.CategoriaId = c.Id";

        // Creo un comando SQL per eseguire la query
        using var command = new SqliteCommand(sql, connection);

        // Uso il reader come cursore per scorrere i dati restituiti dalla query
        using var reader = command.ExecuteReader();

        // Leggo i record restituiti dalla query finché ce ne sono
        while (reader.Read())
        {
            // Aggiungo i dati del prodotto alla lista di prodotti
            // Uso prodotto view model perché voglio visualizzare il nome della categoria
            Prodotti.Add(new ProdottoViewModel {
                // Faccio il get dei campi del record restituito dalla query
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // Versione senza il controllo se la categoria è nulla
                // CategoriaNome = reader.GetString(3)
                // IsDBNull controlla se il campo è null e restituisce true se è null
                // Se è null restituisco l'elemento alla sinistra dei due punti
                // Se non è null restituisco l'elemento alla destra dei due punti
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna" : reader.GetString(3) // evitiamo le {} e quindi if, usando un operatore ternario
            });
        }
    }
}

/*

namespace _04_webapp_sqlite.Prodotti;

public class IndexProdottiModel : PageModel
{
    // private readonly ILogger<PrivacyModel> _logger;

    #region Proprietà prodotti
    public List<ProdottoViewModel>? Prodotti { get; set; } = new();
    public int totaleProdotti { get; set; }
    #endregion

    [BindProperty(SupportsGet = true)]
    public int Ordine { get; set; }

    public IndexProdottiModel()
    {
        //_logger = logger;
        OnGet();
    }

    public void OnGet()
    {
        try
        {
            Prodotti = UtilityDB.ExecuteReader(@"SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Nome", reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // se la categoria è nulla, restituiamo "Nessuna categoria"
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            });
        }
        catch(Exception ex)
        {
            SimpleLogger.Log(ex);
        }

        // todo: filtri lambda (da implementare in SQL)
        if (Ordine == 0)
        {
            Prodotti = Prodotti?.OrderBy(p => p.Prezzo).ToList();
        }
        else if (Ordine == 1)
        {
            Prodotti = Prodotti?.OrderByDescending(p => p.Prezzo).ToList();
        }
        else
        {
            Prodotti = Prodotti?.OrderBy(p => p.Id).ToList();
        }
    }

    public IActionResult OnPost()
    {

        return RedirectToPage("Index", new { Ordine });
    }

}

*/