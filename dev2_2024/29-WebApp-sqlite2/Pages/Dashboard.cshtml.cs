using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class Dashboard : PageModel
{
    private readonly ILogger<Dashboard> _logger;

    // Liste che conterranno i dati da mostrare nelle partial views
    public List<ProdottoViewModel>? ProdottiPiuCostosi { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiRecenti { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiCategoria { get; set; } = new();

    // Costruttore per iniettare il logger
    public Dashboard(ILogger<Dashboard> logger)
    {
        _logger = logger;
    }

    // Metodo che viene eseguito quando la pagina viene caricata
    public void OnGet()
    {
        try
        {
            // Query per ottenere i 5 prodotti più costosi ordinati per prezzo decrescente
            var queryCostosi = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Prezzo DESC LIMIT 5";

            ProdottiPiuCostosi = ExecuteQuery(queryCostosi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero dei prodotti più costosi");
            SimpleLogger.Log(ex);
        }

        try
        {
            // Query per ottenere i 5 prodotti più recenti (ordinati per Id decrescente)
            var queryRecenti = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Id DESC LIMIT 5";

            ProdottiRecenti = ExecuteQuery(queryRecenti);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero dei prodotti più recenti");
            SimpleLogger.Log(ex);
        }

        try
        {
            // Query per ottenere i prodotti di una categoria specifica
            var queryCategoria = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                WHERE p.CategoriaId = 2 LIMIT 5"; // 2 è l'id della categoria per cui voglio filtrare

            ProdottiCategoria = ExecuteQuery(queryCategoria);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero dei prodotti per categoria");
            SimpleLogger.Log(ex);
        }
    }

    // Metodo per eseguire una query SQL e restituire una lista di prodotti
    private List<ProdottoViewModel> ExecuteQuery(string query)
    {
        try
        {
            return UtilityDB.ExecuteReader(query, reader => new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                // Se la categoria è nulla restituisce "Nessuna categoria"
                CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'esecuzione della query: {Query}", query);
            SimpleLogger.Log(ex);
            return new List<ProdottoViewModel>(); // Restituisce una lista vuota in caso di errore
        }
    }
}