using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering;

public class Dashboard : PageModel
{
    public List<ProdottoViewModel>? ProdottiCostosi { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiRecenti { get; set; } = new();
    public List<ProdottoViewModel>? ProdottiPerCategoria { get; set; } = new();

    public void OnGet()
    {
        // carico i prodotti
        var prodCostosi = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Prezzo DESC LIMIT 5";

        ProdottiCostosi = ExecuteQuery(prodCostosi);

        var prodRecenti = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                ORDER BY p.Id DESC LIMIT 5";

        ProdottiRecenti = ExecuteQuery(prodRecenti);

        var prodCategoria = @"
                SELECT p.Id, p.Nome, p.Prezzo, c.Nome as Categoria
                FROM Prodotti p
                LEFT JOIN Categorie c ON p.CategoriaId = c.Id
                WHERE p.CategoriaId = 2 LIMIT 5"; // il 2 è l'id della categoria di prodotti che voglio visualizzare

        ProdottiPerCategoria = ExecuteQuery(prodCategoria);

    }

    public List<ProdottoViewModel> ExecuteQuery(string query)
    {
        List<ProdottoViewModel> ProdottiFiltrati = new List<ProdottoViewModel>();
        using (var connection = DatabaseInitializer.GetConnection())
        {
            // apriamo la connessione
            connection.Open();

            // Occorre creare una query di join con una LEFT JOIN tra la tabella Prodotti e la tabella Categorie
            // Usiamo gli alias in SQLite per rendere più leggibile il codice. Useremo p per Prodotti e c per Categorie

            // Creiamo il comando
            using (var command = new SqliteCommand(query, connection))
            {
                // Eseguiamo il comando
                using (var reader = command.ExecuteReader())
                {
                    // Leggiamo i dati
                    while (reader.Read())
                    {
                        // Creiamo un nuovo prodotto e lo aggiungiamo alla lista
                        ProdottiFiltrati?.Add(new ProdottoViewModel
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Prezzo = reader.GetDouble(2),
                            // se la categoria è nulla, restituiamo "Nessuna categoria"
                            CategoriaNome = reader.IsDBNull(3) ? "Nessuna categoria" : reader.GetString(3)
                        });
                    }
                }
            }
        };
        return ProdottiFiltrati;
    }
}