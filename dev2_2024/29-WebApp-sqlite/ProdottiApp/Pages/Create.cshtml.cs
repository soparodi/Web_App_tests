using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class CreateModel : PageModel

{
    // proprietà pubblica di tipo prodotto per contenere i dati del prodotto
    public Prodotto Prodotto { get; set; }

    // creo una lista di select list item per contenere le categorie
    // select list item è un oggetto che rappresenta un elemento in una select list
    public List<SelectListItem> CategorieSelectList { get; set; } = new List<SelectListItem>();

    public void OnGet()
    {
        CaricaCategorie();
    }

    public IActionResult OnPost()
    {
        // controllo se il modello è valido cioè se i dati inseriti dall'utente rispettano le regole di validazione
        // se il modello non è valido ritorno la pagina errori
        if (!ModelState.IsValid)
        {
            CaricaCategorie(); // carico le categorie se no si carica senza categorie
            // page è un metodo di PageModel che restituisce un oggetto page result che rappresenta la pagina nella quale siamo
            return Page(); // se il modello non è valido ritorno la pagina
        }

        // invoco il metodo GetConnection per ottenere la connessione al db
        using var connection = DatabaseInitializer.GetConnection();
        // apro la connessione
        connection.Open();

        // creo la query sql per inserire un nuovo prodotto usando i parametri
        // i parametri servono per evitare sql injection
        // la sql injection è un attacco informatico che sfrutta le query sql per inserire codice
        // in pratica dobbiamo separare i dati dalla quey sql e validarli (passarli come parametri una volta controllati)
        // si mette davanti al valore di parametro il carattere @
        var sql = "INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES (@nome, @prezzo,@categoriaId)";
        
        // creo un comando sql per eseguire la query sulla connessione che ho creato
        using var command = new SqliteCommand(sql, connection);

        // aggiungo i parametri al comando con il metodo add with value che prende il nome del parametro e il valore
        command.Parameters.AddWithValue("@nome", Prodotto.Nome);
        command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
        command.Parameters.AddWithValue("@categoriaId", Prodotto.CategoriaId);

        // eseguo il comando
        command.ExecuteNonQuery();

        // reindirizzo l'utente alla pagina di elenco dei prodotti
        return RedirectToPage("Index");
    }

    // metodo per caricare le categorie
    private void CaricaCategorie()
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();

        // creo la query sql per ottenere i dati delle categorie
        var sql = "SELECT Id, Nome FROM Categorie";
        
        // creo un comando per eseguire la query
        using var command = new SqliteCommand(sql, connection);
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