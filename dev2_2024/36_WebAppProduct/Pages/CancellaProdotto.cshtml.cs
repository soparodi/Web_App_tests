using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class CancellaProdottoModel : PageModel
{
    private readonly ILogger<CancellaProdottoModel> _logger;

    public CancellaProdottoModel(ILogger<CancellaProdottoModel> logger) // costruisce dei messaggi che ci danno informazioni sulla pagina
    {
        _logger = logger;
    }
    public Prodotto Prodotto { get; set; } // assegna la proprietà del modello Prodotto alla variabile Prodotto

    public void OnGet(int id) // l'id è sufficiente perché gli abbiamo passato gli altri parametri tramite file json
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json"); // legge i prodotti dal file json
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json); // deserializza il file in una lista
            foreach (var prodotto in prodotti)
            {
                if (prodotto.Id == id)
                {
                    Prodotto = prodotto; // quando trova l'id corrispondente, lo assegna alla proprietà del modello Prodotto
                    break;
                }
            }
    }

    public IActionResult Onpost(int id, string nome, decimal prezzo, string dettaglio, string immagine) // accede al form della pagina
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json");
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json);
            for(int i = 0; i < prodotti.Count; i++)
            {
                if (prodotti[i].Id == id) // se il puntatore del prodotto è uguale al numero di id
                {
                    prodotti.RemoveAt(i); // elimino il prodotto a quel punto
                    break;
                }
            }
        System.IO.File.WriteAllText("wwwroot/json/prodotti.json", JsonConvert.SerializeObject(prodotti)); // serializziamo il prodotto modificato e lo scriviamo sul file json
        return RedirectToPage("Prodotti");
    }
}