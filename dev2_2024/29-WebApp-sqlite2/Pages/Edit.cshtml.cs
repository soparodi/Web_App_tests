using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina

public class EditModel : PageModel
{
    [BindProperty]
    public Prodotto Prodotto { get; set; }
    public List<SelectListItem> CategorieSelectList { get; set; } = new List<SelectListItem>();

    // passo l'id come parametro perché voglio modificare un prodotto esistente sul quale ho cliccato in precedenza
    public IActionResult OnGet(int id)
    {
        // invoco il metodo GetConnection per ottenere la connessione al db
        using var connection = DatabaseInitializer.GetConnection();
        // apro la connessione
        connection.Open();
        // uso la clausola WHERE di sql in modo da ottenere solo il prodotto con l'id passato come parametro
        var sql = "SELECT Id, Nome, Prezzo, CategoriaId FROM Prodotti WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        // eseguo il comando e ottengo un reader che è un oggetto che mi permette di leggere i dati
        using var reader = command.ExecuteReader();

        // se il reader ha dati
        if (reader.Read())
        {
            Prodotto = new Prodotto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Prezzo = reader.GetDouble(2),
                CategoriaId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
            };
        }
        else
        {
            return NotFound();
        }

        // carico le categorie in modo da poterle visualizzare nella select list
        CaricaCategorie();
        // restituisco la pagina con i dati del prodotto da modificare
        return Page();
    }

    public IActionResult OnPost()
    {
        // se il modello non è valido carico le categorie e restituisco la pagina
        if (!ModelState.IsValid)
        {
            CaricaCategorie();
            return Page();
        }
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // costruisco la query basandomi sull'input dell'utente
        var sql = "UPDATE Prodotti SET Nome = @nome, Prezzo = @prezzo, CategoriaId = @categoriaId WHERE Id = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", Prodotto.Nome);
        command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
        command.Parameters.AddWithValue("@categoriaId", Prodotto.CategoriaId);
        command.Parameters.AddWithValue("@id", Prodotto.Id);

        command.ExecuteNonQuery();
        return RedirectToPage("Prodotti");
    }

    private void CaricaCategorie()
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // creo la query sql per ottenere i dati delle categorie
        var sql = "SELECT Id, Nome FROM Categorie";

        // creo un comando per eseguire la query
        using var command = new SqliteCommand(sql, connection); // se usassi System dovrei scrivere SQLiteCommand
        // eseguo il comando e ottengo un reader che è un oggetto che mi permette di leggere i dati
        using var reader = command.ExecuteReader();
        // finché il reader ha elementi vado ad aggiungere il select list item di prima
        while (reader.Read())
        {
            CategorieSelectList.Add(new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(), // converto in stringa l'id così da poterlo usare come valore
                Text = reader.GetString(1)
            });
        }
    }
}