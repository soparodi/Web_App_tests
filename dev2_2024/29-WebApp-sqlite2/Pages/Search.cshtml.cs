using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
// namespace Pages.Prodotti

public class SearchModel : PageModel
{
    //proprietà pubblica che memorizzi la stringa di ricerca
    public string SearchTerm { get; set; }
    public List<ProdottoViewModel> Prodotto { get; set; } = new List<ProdottoViewModel>();

    public void OnGet(string q)
    {
        // assegno la stringa di ricerca alla proprietà pubblica SearchTerm
        SearchTerm = q;
        // se la stringa di ricerca non è vuota
        if (!string.IsNullOrWhiteSpace(q))
        {
            // Invoco il metodo GetConnection per ottenere la connessione al db
            using var connection = DatabaseInitializer.GetConnection();
            // Apro la connessione
            connection.Open();
            // query per selezionare i prodotti che contengono la stringa di ricerca
            // il like è case insensitive di default in sqlite
            // il like è una clausola che permette di fare una ricerca parziale
            var sql = @"
            SELECT p.Id, p.Nome, p.prezzo, c.Nome as CategoriaNome
            FROM Prodotti p
            LEFT JOIN Categorie c ON p.CategoriaId = c.Id
            WHERE p.Nome LIKE @searchTerm";
            // lanciamo il comando sql sulla connessione
            using var command = new SqliteCommand(sql, connection);
            // uso il parametro per evitare sql injection con % + q + % in modo da cercare la stringa in qualsiasi parte del nome
            command.Parameters.AddWithValue("@searchTerm", $"%{q}%");
            // ottengo il reader
            using var reader = command.ExecuteReader();
            // Leggo i record restituiti dalla query finché ce ne sono
            while (reader.Read())
            {
                // Aggiungo i dati del prodotto alla lista di prodotti
                // Uso prodotto view model perché voglio visualizzare il nome della categoria
                Prodotto.Add(new ProdottoViewModel
                {
                    // Faccio il get dei campi del record restituito dalla query
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Prezzo = reader.GetDouble(2),
                    // Versione senza il controllo se la categoria è nulla
                    // CategoriaNome = reader.GetString(3)
                    // IsDBNull controlla se il campo è null e restituisce true se è null
                    // Se è null restituisco l'elemento alla sinistra dei due punti
                    // Se non è null restituisco l'elemento alla destra dei due punti
                    CategoriaNome = reader.IsDBNull(3) ? "Nessuna" : reader.GetString(3)
                });
            }
        }
    }
}