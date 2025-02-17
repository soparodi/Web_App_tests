using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class ProdottiModel : PageModel
{
    // Creo una proprietà pubblica di tipo lista di prodotti view model
    public List<ProdottoViewModel> Prodotti { get; set; } = new List<ProdottoViewModel>(); // la inizializzo come una lista vuota
    
    // Proprietà per il conteggio dei prodotti totali
    public int TotaleProdotti { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Ordine { get; set; }

    public void OnGet()
    {
        try
        {
            // Invoco ExecuteReader per eseguire la query SQL e ottenere i dati
            Prodotti = UtilityDB.ExecuteReader(@"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Nome", 
                reader => new ProdottoViewModel
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Prezzo = reader.GetDouble(2),
                    // Se la categoria è nulla, restituiamo "Nessuna categoria"
                    CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
                });

            // Calcolo del numero totale di prodotti restituiti dalla query
            TotaleProdotti = Prodotti.Count;

            // Applico l'ordinamento in base al valore di Ordine
            if (Ordine == 0)
            {
                Prodotti = Prodotti.OrderBy(p => p.Prezzo).ToList();
            }
            else if (Ordine == 1)
            {
                Prodotti = Prodotti.OrderByDescending(p => p.Prezzo).ToList();
            }
            else
            {
                Prodotti = Prodotti.OrderBy(p => p.Id).ToList();
            }
        }
        catch (Exception ex)
        {
            // Log dell'errore (per evitare crash dell'app in caso di errore SQL)
            SimpleLogger.Log(ex);
        }
    }

    public IActionResult OnPost()
    {
        // Reindirizza alla stessa pagina con il valore aggiornato di Ordine
        return RedirectToPage("Index", new { Ordine });
    }
}