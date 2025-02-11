using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class IndexModel : PageModel

{
    // creo una proprietà pubblica di tipo lista di prodotti view model
    public List<ProdottoViewModel> Prodotti { get; set; } = new List<ProdottoViewModel>();

    public void Onget()
    {
        // invoco il metodo GetConnection per ottenere la connessione al db
        using var connection = DatabaseInitializer.GetConnection();
        // apro la connessione
        connection.Open();

        // creo la query sql per ottenere i dati dei prodotti
        // voglio accedere al nome della categoria quindi devo fare una join tra la tabella prodotti e la tabella categorie
        // uso JOIN per ottenere i prodotti con categoria
        // uso LEFT JOIN per ottenere i prodotti senza categoria
        // posso usare p e c come alias per le tabelle prodotti e categorie se voglio usare i nomi completi delle tabelle uso Prodotti e Categorie
        // il vantaggio di usare gli alias è che dopo posso usare p e c per accedere ai campi delle tabelle
        var sql = @"
        SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
        FROM Prodotti.p
        LEFT JOIN Categorie c ON p.CategoriaId = c.Id";

        // creo un comando sql per eseguire la query
        using var command = new SqliteCommand(sql, connection);

        // uso il reader come cursore per scorrere i dati restituiti dalla query
        using var reader = command.ExecuteReader();

        // leggo i record restituiti dalla query finché ce ne sono
        while (reader.Read())
        {
            // aggiungo i dati del prodotto alla lista di prodotti
            // uso prodotto view model perché voglio visualizzare il nome della categoria
            Prodotti.Add(new ProdottoViewModel {
            // faccio il get dei campi del record restituito dalla query
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Prezzo = reader.GetDouble(2),
            // versione senza il controllo se la categoria è nulla
            // CategoriaNome = reader.GetString(3)
            // IsDBNull controlla se il campo è null e restituisce true se è null
            // se è null restituisco l'elemento alla sinistra dei due punti
            // se non è null restituisco l'elemento alla destra dei due punti
            CategoriaNome = reader.IsDBNull(3)? "Nessuna" : reader.GetString(3)
            });
        }
    }
}