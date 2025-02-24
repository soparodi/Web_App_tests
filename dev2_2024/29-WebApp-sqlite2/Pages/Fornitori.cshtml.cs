using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class FornitoriModel : PageModel
{
    // Proprietà pubblica per memorizzare l'elenco dei fornitori
    public List<FornitoreViewModel> Fornitore { get; set; } = new List<FornitoreViewModel>();

    public void OnGet()
    {
        // Apro la connessione al database
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query per ottenere tutti i fornitori
        var sql = "SELECT Id, Nome FROM Fornitori";

        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        // Itero sui risultati della query e popolo la lista
        while (reader.Read())
        {
            Fornitore.Add(new FornitoreViewModel
            {
                FornitoreId = reader.GetInt32(0),
                FornitoreNome = reader.GetString(1)
            });
        }
    }
}