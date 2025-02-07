using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
public class ProdottiModel : PageModel
{
    private readonly ILogger<ProdottiModel> _logger;
    public ProdottiModel(ILogger<ProdottiModel> logger)
    {
        _logger = logger;

    }
    public IEnumerable<Prodotto> Prodotti { get; set; }//una sequenza di elementi che non supporta la modifica
    public string Ricerca { get; set; }
    //public string Ricerca;
    public void OnGet(string ricerca)
    {
        Ricerca = ricerca;
        Prodotti = new List<Prodotto>
            {
                new Prodotto {Nome = "Coca-cola", Prezzo = 10, Dettaglio = "1L"},
                new Prodotto {Nome = "Fanta", Prezzo = 20, Dettaglio = "1L"},
                new Prodotto {Nome = "Vino", Prezzo = 30, Dettaglio = "Lambrusco"},
                new Prodotto {Nome = "Redbull", Prezzo = 40, Dettaglio = "Dettaglio4"},
                new Prodotto {Nome = "Succo all'albicocca", Prezzo = 50, Dettaglio = "Dettaglio5"},
                new Prodotto {Nome = "Biscotti integrali", Prezzo = 60, Dettaglio = "Dettaglio6"}
            };

        //creo una lista di prodotti filtrati
        List<Prodotto> prodottiFiltrati = new List<Prodotto>();

        if (!string.IsNullOrEmpty(Ricerca))
        //aggiungo alla lista di prodotti filtrati
        {
            foreach (Prodotto prodotto in Prodotti)
            {
                if (prodotto.Nome.Contains(Ricerca, StringComparison.OrdinalIgnoreCase)) // StringComparison.OriginalIgnoreCase ignora
                                                                                         //il carattere della stringa
                {
                    prodottiFiltrati.Add(prodotto);
                }
            }
            Prodotti = prodottiFiltrati;
        }
    }
}