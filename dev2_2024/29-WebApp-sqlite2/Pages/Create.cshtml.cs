using Microsoft.AspNetCore.Mvc; // using in modo da usare IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite; // si può usare anche System.Data.SQLite
using Microsoft.AspNetCore.Mvc.Rendering; // using in modo da usare SelectListItem per fare il menu a tendina

public class CreateModel : PageModel

{
    [BindProperty] // attributo (decorator) bind property per collegare il metodo al form
    public Prodotto Prodotto { get; set; } // proprietà pubblica di tipo prodotto per contenere i dati del prodotto

    // aggiungo logger se voglio fare un debug, non è strettamente necessario
    // creo una lista di select list item per contenere le categorie
    // select list item è un oggetto che rappresenta un elemento in una select list
    // serve per poi creare un menu a tendina in html
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
        // i parametri servono per evitare sql injection - dove anziché scrivere il nome scrivono un pezzo di codice malevolo
        // la sql injection è un attacco informatico che sfrutta le query sql per inserire codice
        // in pratica dobbiamo separare i dati dalla quey sql e validarli (passarli come parametri una volta controllati)
        // si mette davanti al valore di parametro il carattere @
        var sql = "INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES (@nome, @prezzo,@categoriaId)"; // quelli dentro VALUES sono dei placeholder,
                                                                                                           // potrei scrivere direttamente @categoria

        // creo un comando sql per eseguire la query sulla connessione che ho creato
        using var command = new SqliteCommand(sql, connection);

        // aggiungo i parametri al comando con il metodo add with value (che evita l'injection) che prende il nome del parametro e il valore
        command.Parameters.AddWithValue("@nome", Prodotto.Nome);
        command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
        command.Parameters.AddWithValue("@categoriaId", Prodotto.CategoriaId);

        // eseguo il comando
        command.ExecuteNonQuery();

        // reindirizzo l'utente alla pagina di elenco dei prodotti
        return RedirectToPage("Prodotti");
    }

    // metodo per caricare le categorie
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

/*

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace _04_webapp_sqlite.Prodotti;
public class AggiungiProdottoModel : PageModel
{
    private readonly ILogger<AggiungiProdottoModel> _logger;

    [BindProperty]
    public Prodotto Prodotto { get; set; }

    public List<SelectListItem> Categorie { get; set; } = new List<SelectListItem>();


    public AggiungiProdottoModel()
    {
        Prodotto = new Prodotto();
        CaricaCategorie();
    }

    // public AggiungiProdottoModel(ILogger<AggiungiProdottoModel> logger)
    // {
    //     _logger = logger;
    // }

    public void OnGet()
    {
        CaricaCategorie();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CaricaCategorie();
            return Page();
        }

        var sql = @"INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES (@nome, @prezzo, @categoria)";

        UtilityDB.ExecuteNonQuery(sql, command =>
        {
            command.Parameters.AddWithValue("@nome", Prodotto.Nome);
            command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
            command.Parameters.AddWithValue("@categoria", Prodotto.CategoriaId);
        }
        );

        // using (var connection = DatabaseInitializer.GetConnection())
        // {
        //     // aprire la connessione
        //     connection.Open();
        //     var sql = @"INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES (@nome, @prezzo, @categoria)";

        //     using (var command = new SQLiteCommand(sql,connection))
        //     {
        //         command.Parameters.AddWithValue("@nome", Prodotto.Nome);
        //         command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
        //         command.Parameters.AddWithValue("@categoria", Prodotto.CategoriaId);
        //         command.ExecuteNonQuery();
        //     }
        // }
        return RedirectToPage("Index");
    }

    public void CaricaCategorie()
    {
        using (var connection = DatabaseInitializer.GetConnection())
        {
            // aprire la connessione
            connection.Open();

            // leggere la tabella categorie
            var sql = @" SELECT * FROM Categorie";

            using (var command = new SQLiteCommand(sql, connection))
            {
                // mentre il reader legge
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // aggiungi nuovo oggetto SelectListItem con Value e Text
                        Categorie.Add(new SelectListItem
                        {
                            Value = reader.GetInt32(0).ToString(),
                            Text = reader.GetString(1)
                        });
                    }
                }
            }
        }
    }
}

*/