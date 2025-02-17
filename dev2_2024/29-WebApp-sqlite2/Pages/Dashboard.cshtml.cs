using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // Si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering;

// Dichiarazione della classe della pagina Dashboard che eredita da PageModel
public class Dashboard : PageModel
{
    // Liste che conterranno i dati da mostrare nelle partial views
    public List<ProdottoViewModel>? ProdottiCostosi { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiRecenti { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiPerCategoria { get; set; } = new();

    // Metodo che viene eseguito quando la pagina viene caricata
    public void OnGet()
    {
        // Query per ottenere i 5 prodotti più costosi ordinati per prezzo decrescente
        var prodCostosi = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Prezzo DESC LIMIT 5";

        // Eseguo la query e assegno i risultati alla lista
        ProdottiCostosi = ExecuteQuery(prodCostosi);

        // Query per ottenere i 5 prodotti più recenti (ordinati per Id decrescente)
        var prodRecenti = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Id DESC LIMIT 5";

        ProdottiRecenti = ExecuteQuery(prodRecenti);

        // Query per ottenere i prodotti di una categoria specifica (es. con id = 2)
        var prodCategoria = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                WHERE p.CategoriaId = 2 LIMIT 5"; // il 2 è l'id della categoria da visualizzare

        ProdottiPerCategoria = ExecuteQuery(prodCategoria);
    }

    // Metodo per eseguire una query SQL e restituire una lista di prodotti
    public List<ProdottoViewModel> ExecuteQuery(string query)
    {
        List<ProdottoViewModel> ProdottiFiltrati = new List<ProdottoViewModel>();

        // Ottengo la connessione al database
        using (var connection = DatabaseInitializer.GetConnection())
        {
            connection.Open(); // Apro la connessione

            // Creo il comando SQL da eseguire
            using (var command = new SqliteCommand(query, connection))
            {
                // Eseguo il comando e leggo i dati ottenuti
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) // Ciclo sui risultati della query
                    {
                        // Creo un nuovo oggetto ProdottoViewModel e lo aggiungo alla lista
                        // dopo ProdottiFiltrati, l'operatore ? verifica se l'oggetto a sinistra è null
                        // se non è null, esegue l'operazione (Add in questo caso)
                        // se è null, restituisce semplicemente null e non genera un'eccezione
                        // senza ? il codice sopra genera l'eccezione NullReferenceException
                        ProdottiFiltrati?.Add(new ProdottoViewModel
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Prezzo = reader.GetDouble(2),
                            // se la categoria è nulla restituisce "Nessuna categoria"
                            CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
                        });
                    }
                }
            }
        }
        return ProdottiFiltrati;
    }
}

/*

namespace _04_webapp_sqlite.Prodotti;

public class Dashboard : PageModel
{
    private readonly ILogger<Dashboard> _logger;

    #region Proprietà prodotti
    public List<ProdottoViewModel>? ProdottiPiuCostosi { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiRecenti { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiCategoria { get; set; } = new();

    #endregion

    public Dashboard(ILogger<Dashboard> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var queryCostosi = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Prezzo DESC LIMIT 5";

        try
        {
            ProdottiPiuCostosi = UtilityDB.ExecuteReader(queryCostosi, reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // se la categoria è nulla, restituiamo "Nessuna categoria"
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }

        var queryRecenti = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Id DESC LIMIT 5";

        try
        {
            ProdottiRecenti = UtilityDB.ExecuteReader(queryRecenti, reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // se la categoria è nulla, restituiamo "Nessuna categoria"
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }

        var queryCategoria = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                WHERE p.CategoriaId = 11 LIMIT 5";

        try
        {
            ProdottiCategoria = UtilityDB.ExecuteReader(queryCategoria, reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // se la categoria è nulla, restituiamo "Nessuna categoria"
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
    }
}

*/