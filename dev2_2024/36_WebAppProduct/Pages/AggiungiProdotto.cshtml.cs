using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class AggiungiProdottoModel : PageModel
{
    public void OnGet()
    {
    }
    public IActionResult OnPost(string nome, decimal prezzo, string dettaglio)
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