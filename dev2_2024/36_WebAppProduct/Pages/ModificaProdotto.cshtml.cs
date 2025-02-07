using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ModificaProdottoModel : PageModel
{
    private readonly ILogger<ModificaProdottoModel> _logger;

    public ModificaProdottoModel(ILogger<ModificaProdottoModel> logger) // costruisce dei messaggi che ci danno informazioni sulla pagina
    {
        _logger = logger;
        _logger.LogInformation("ModificaProdottoModel");
        _logger.LogDebug("ModificaProdottoModel");
        _logger.LogWarning("ModificaProdottoModel");
        _logger.LogError("ModificaProdottoModel");
    }
    public Prodotto Prodotto { get; set; } // passiamo la variabile Prodotto al post

    public void OnGet(int id) // inizia quando si accede alla pagina di ModificaProdotto, riceve un parametro id in ingresso che mi da il prodotto da modificare
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json"); // legge il file json
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json); // deserializza il file in una lista
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Id == id)
            {
                Prodotto = prodotto; // quando trova l'id corrispondente, lo assegna alla proprietà del modello Prodotto
                break; // esce dal ciclo
            }
        }
    }


    public IActionResult OnPost(int id, string nome, decimal prezzo, string dettaglio, string immagine) // accede al form della pagina
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json"); // legge dal file Json
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json); // deserializza il file json in una lista di prodotti
        
        Prodotto prodotto = null; // dobbiamo dichiare che è una variabile prodotto di tipo Prodotto a cui assegnamo la variabile null

        foreach (var p in prodotti) // itera su tutti i prodotti
        {
            if(p.Id == id)
            {
                prodotto = p; // assegnamo alla variabile prodotto l'id preso dal form della pagina ModificaProdotto
            }
        }
        // aggiorniamo i vari campi del prodotto
        prodotto.Nome = nome;
        prodotto.Prezzo = prezzo;
        prodotto.Dettaglio = dettaglio;
        prodotto.Immagine = immagine;

        System.IO.File.WriteAllText("wwwroot/json/prodotti.json", JsonConvert.SerializeObject(prodotti, Formatting.Indented)); // serializziamo il prodotto modificato e lo scriviamo sul file json
        return RedirectToPage("Prodotti");
    }
}