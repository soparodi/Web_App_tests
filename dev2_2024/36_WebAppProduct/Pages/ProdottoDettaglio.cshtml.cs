using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ProdottoDettaglioModel : PageModel
{
    private readonly ILogger<ProdottoDettaglioModel> _logger;

    public ProdottoDettaglioModel(ILogger<ProdottoDettaglioModel> logger)
    {
        _logger = logger;
    }
    public Prodotto Prodotto { get; set; } // assegna la proprietà del modello Prodotto alla variabile Prodotto

    public void OnGet(int id) // l'id è sufficiente perché gli abbiamo passato gli altri parametri tramite file json
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json"); // legge i prodotti dal file json
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json); // deserializza i prodotti
            foreach (var prodotto in prodotti)
            {
                if (prodotto.Id == id)
                {
                    Prodotto = prodotto; // assegna il prodotto al quale corrisponde l'id che gli abbiamo passato dal file json alla proprietà Prodotto
                    break; // esce dal ciclo
                }
            }
    }
}