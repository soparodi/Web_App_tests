using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class FornitoreDeleteModel : PageModel
{
    public FornitoreViewModel Fornitore { get; set; }

    public IActionResult OnGet(int id)
    {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();

            var sql = "SELECT Id, Nome FROM Fornitori WHERE Id = @id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Fornitore = new FornitoreViewModel
                {
                    FornitoreId = reader.GetInt32(0),
                    FornitoreNome = reader.GetString(1)
                };
            }
            else
            {
                return NotFound(); // Se non trovi il fornitore, ritorna un errore 404
            }

        return Page();
    }

    public IActionResult OnPost(int id)
    {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();

            // Elimino il fornitore
            var deleteSql = "DELETE FROM Fornitori WHERE Id = @id";
            using var deleteCommand = new SqliteCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@id", id);
            deleteCommand.ExecuteNonQuery();  // Esegui l'eliminazione 

            return RedirectToPage("Fornitori");  // Reindirizza alla lista dei fornitori
    }
}