using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class CategoriaCreateModel : PageModel
{
    [BindProperty]
    public Categoria Categoria { get; set; }

    public void OnGet()
    {
        // Il metodo OnGet viene usato solo per mostrare il form di creazione
    }

    public IActionResult OnPost()
    {
        // Controllo se il modello è valido
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // Query SQL per inserire una nuova categoria
        var sql = "INSERT INTO Categorie (Nome) VALUES (@nome)";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", Categoria.Nome);

        command.ExecuteNonQuery();

        // Reindirizzo alla lista delle categorie dopo l'inserimento
        return RedirectToPage("Categorie");
    }
}
