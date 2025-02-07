using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ProdottiModel : PageModel
{
    private readonly ILogger<ProdottiModel> _logger;

    public ProdottiModel(ILogger<ProdottiModel> logger)
    {
        _logger = logger;
        _logger.LogInformation("Prodotti Caricati");
    }

    public IEnumerable<Prodotto> Prodotti { get; set; }
    public int numeroPagine { get; set; }

    public void OnGet(decimal? minPrezzo, decimal? maxPrezzo, int? pageIndex)
    {
        // Legge il file JSON contenente i prodotti
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json");
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json);

        if (prodotti == null) // Verifica che il file JSON non sia vuoto o non valido
        {
            _logger.LogError("Errore nel caricamento dei prodotti dal file JSON.");
            Prodotti = new List<Prodotto>();
            numeroPagine = 0;
            return;
        }

        var prodottiFiltrati = new List<Prodotto>();

        // Filtra i prodotti in base ai prezzi minimi e massimi
        foreach (var prodotto in prodotti)
        {
            bool aggiungi = true;

            if (minPrezzo.HasValue && prodotto.Prezzo < minPrezzo.Value)
            {
                aggiungi = false;
            }
            if (maxPrezzo.HasValue && prodotto.Prezzo > maxPrezzo.Value)
            {
                aggiungi = false;
            }

            if (aggiungi)
            {
                prodottiFiltrati.Add(prodotto);
            }
        }

        // Assegna i prodotti filtrati alla proprietà
        Prodotti = prodottiFiltrati;

        // Calcola il numero di pagine
        numeroPagine = (int)Math.Ceiling(Prodotti.Count() / 6.0);

        // Applica la paginazione
        int indicePagina = (pageIndex ?? 1) - 1;
        Prodotti = Prodotti.Skip(indicePagina * 6).Take(6);
    }
}