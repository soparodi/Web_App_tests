using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class CategorieModel : PageModel
{
    // Proprietà pubblica per memorizzare l'elenco delle categorie
    public List<CategoriaViewModel> Categorie { get; set; } = new List<CategoriaViewModel>();

    public void OnGet()
    {
        // Otteniamo la connessione al database
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per ottenere tutte le categorie
        var sql = "SELECT Id, Nome FROM Categorie";

        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        // Iteriamo sui risultati della query e popolo la lista
        while (reader.Read())
        {
            Categorie.Add(new CategoriaViewModel
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1)
            });
        }
    }
}