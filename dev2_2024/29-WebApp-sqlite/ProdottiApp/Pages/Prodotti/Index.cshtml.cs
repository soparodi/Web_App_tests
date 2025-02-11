using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class IndexModel : PageModel

{
    // creo una proprietà pubblica di tipo lista di prodotti view model
    public List<ProdottoViewModel> Prodotti { get; set; }; = new List<ProdottoViewModel>();

    public void Onget()
    {
        // invoco il metodo GetConnection per ottenere la connessione al db
        using var connection = DatabaseInitializer.GetConnection();
        // apro la connessione
        connection.Open();

        // creo la query sql per ottenere i dati dei prodotti
        // voglio accedere al nome della categoria quindi devo fare una join tra la tabella prodotti e la tabella categorie
        // uso JOIN per ottenere i prodotti con categoria
        // uso LEFT JOIN per ottenere i prodotti senza categoria
        // posso usare p e c come alias per le tabelle prodotti e categorie se voglio usare i nomi completi delle tabelle uso Prodotti e Categorie
        // il vantaggio di usare gli alias è che dopo posso usare p e c per accedere ai campi delle tabelle
        var sql = @"
        SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
        FROM Prodotti.p
        LEFT JOIN Categorie c ON p.CategoriaId = c.Id";
    }
}