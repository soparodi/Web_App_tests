using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina

public class ProdottiCostosiModel : PageModel
{
    public List<ProdottoViewModel> Prodotti { get; set; } = new();

    public void OnGet()
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        var sql = "SELECT Id, Nome, Prezzo FROM Prodotti ORDER BY Prezzo DESC LIMIT 5;";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Prodotti.Add(new ProdottoViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2)
            });
        }
    }
}