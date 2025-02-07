using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class AggiungiProdottoModel : PageModel
{
    public void OnGet() // è vuoto perché non serve che vengano visualizzati i dati del prodotto
    {
    }
    public IActionResult OnPost(string nome, decimal prezzo, string dettaglio) // anzichè recuperare le informazioni, le inserisce
    // IActionResult è un tipo di dato e restituisce un RISULTATO che gli daremo poi nel return da manipolare in questo ambito web
    {
        var json = System.IO.File.ReadAllText("wwwroot/json/prodotti.json");
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json);

        var id = 1;
        if (prodotti.Count > 0)
        {
            id = prodotti[prodotti.Count - 1].Id + 1;
        }
        prodotti.Add(new Prodotto{Nome = nome, Prezzo= prezzo, Dettaglio= dettaglio});
        System.IO.File.WriteAllText("wwwroot/json/prodotti.json", JsonConvert.SerializeObject(prodotti));
        return RedirectToPage("Prodotti");
    }
}