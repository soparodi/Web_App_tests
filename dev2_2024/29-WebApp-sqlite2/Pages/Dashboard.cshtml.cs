using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina

public class DashboardModel : PageModel
{
    public List<string> Categorie { get; set; } = new();
    public string CategoriaSelezionata { get; set; } = string.Empty;
    public List<ProdottoViewModel> ProdottiPerCategoria { get; set; } = new();

    public void OnGet(string? categoria)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Recupera le categorie
        var getCategorie = "SELECT Nome FROM Categorie;";
        using var command = new SqliteCommand(getCategorie, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Categorie.Add(reader.GetString(0));
        }

        // Se è stata selezionata una categoria, carica i prodotti corrispondenti
        if (!string.IsNullOrEmpty(categoria))
        {
            CategoriaSelezionata = categoria;
            var getProdottiCategoria = @"
            SELECT Id, Nome, Prezzo FROM Prodotti 
            WHERE CategoriaId = (SELECT Id FROM Categorie WHERE Nome = @categoria)";

            using var prodCommand = new SqliteCommand(getProdottiCategoria, connection);
            prodCommand.Parameters.AddWithValue("@categoria", categoria);
            using var prodReader = prodCommand.ExecuteReader();

            while (prodReader.Read())
            {
                ProdottiPerCategoria.Add(new ProdottoViewModel
                {
                    Id = prodReader.GetInt32(0),
                    Nome = prodReader.GetString(1),
                    Prezzo = prodReader.GetDouble(2)
                });
            }
        }
    }
}