`DatabaseInitializer.cs`:

```csharp

// file DatabaseInitializer.cs

// questo file gestisce la connessione al database ed inizializza i dati tramite seeding

using Microsoft.Data.Sqlite;

public class DatabaseInitializer

{
    // Il database verrà creato nella cartella dell'app

    private static string _connectionString = "Data Source=prodottiapp.db"; // utilizzeremo _connectionString in un metodo in modo da ottenere la connessione al db

    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString); // creiamo una connessione al db tramite using per garantire che venga chiusa correttamente
        {
            connection.Open(); // apriamo la connessione

            // gestisco l'eccezione se il db esiste già in sql

            // creazione tabella Categorie

            // se non metto la @ non posso andare a capo
            var createCategorieTable = @"
            CREATE TABLE IF NOT EXISTS Categorie
                (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL
                );
                ";



            // lancio il comando sulla connessione che ho appena creato
            using (var command = new SqliteCommand(createCategorieTable, connection)) // lo using usa quello che gli passiamo in argomento nel blocco di codice tra {} 
                                                                                      // questo per la tabella categorie, quello prima per la connessione
            {
                command.ExecuteNonQuery(); // eseguiamo il comando sql però non ritorna nulla, lo crea e basta
            }

            var createProdottiTable = @"
            CREATE TABLE IF NOT EXISTS Prodotti
                (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Prezzo REAL NOT NULL,
                CategoriaId INTEGER,
                FOREIGN KEY(CategoriaId) REFERENCES Categorie(Id)
                );
                ";

            // lancio il comando sulla connessione che ho appena creato
            using (var command = new SqliteCommand(createProdottiTable, connection))

            {
                command.ExecuteNonQuery();
            }

            // seed dei dati per Categorie solo la prima volta
            // seleziono il numero di categorie presenti nel db
            var countCommand = new SqliteCommand("SELECT COUNT(*) FROM Categorie", connection);

            // dato che count di sql è un valore numerico, posso usare execute scalar per ottenere il valore
            // execute scalar ritorna un oggetto quindi faccio il casting a long per ottenere il valore numerico
            var count = (long)countCommand.ExecuteScalar();

            // se il count è uguale a zero, allora non ci sono categorie nel db e posso fare il seed dei dati
            if (count == 0)
            {
                // sto inserendo più valori in una sola query quindi devo mettere le parentesi tonde intorno ai valori
                var insertCategorie = @"
                INSERT INTO Categorie (Nome) VALUES 
                ('Pasta'),
                ('Verdure'),
                ('Condimenti');
                ";

                // lancio il comando sulla connessione che ho appena creato
                using (var command = new SqliteCommand(insertCategorie, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Seed dei dati per Prodotti (solo se non esistono già)
                countCommand = new SqliteCommand("SELECT COUNT(*) FROM Prodotti", connection);
                count = (long)countCommand.ExecuteScalar();

                if (count == 0)
                {
                    var insertProdotti = @"
                INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES 
                ('Trofie', 1.59, (SELECT Id FROM Categorie WHERE Nome = 'Pasta')),
                ('Aglio', 0.59, (SELECT Id FROM Categorie WHERE Nome = 'Verdure')),
                ('Pesto', 2.59, (SELECT Id FROM Categorie WHERE Nome = 'Condimenti'));
                ";

                    // lancio il comando sulla connessione che ho appena creato
                    using (var command = new SqliteCommand(insertProdotti, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
    // Metodo per ottenere la connessione al database in modo da poter essere utilizzato in altr parti del codice
    // oltretutto database initializer è una classe statica quindi posso chiamare questo metodo senza creare un'istanza della classe

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString); // in questo modo la connessione è creata ma non aperta però puo essere utilizzata in altri metodi
    }
}

```

#### Aggiunta del Carrello:

```csharp

// Questo file gestisce la connessione al database ed inizializza i dati tramite seeding

using Microsoft.Data.Sqlite;

public class DatabaseInitializer
{
    // Il database verrà creato nella cartella dell'app
    private static string _connectionString = "Data Source=prodottiapp.db"; // Stringa di connessione al DB

    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString); // Crea la connessione
        connection.Open(); // Apre la connessione

        // Creazione tabella Categorie
        var createCategorieTable = @"
        CREATE TABLE IF NOT EXISTS Categorie
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL
        );";

        using (var command = new SqliteCommand(createCategorieTable, connection))
        {
            command.ExecuteNonQuery();
        }

        // Creazione tabella Prodotti
        var createProdottiTable = @"
        CREATE TABLE IF NOT EXISTS Prodotti
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Prezzo REAL NOT NULL,
            CategoriaId INTEGER,
            FOREIGN KEY(CategoriaId) REFERENCES Categorie(Id)
        );";

        using (var command = new SqliteCommand(createProdottiTable, connection))
        {
            command.ExecuteNonQuery();
        }

        // Creazione tabella Carrello
        var createCarrelloTable = @"
        CREATE TABLE IF NOT EXISTS Carrello
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            ProdottoId INTEGER NOT NULL,
            Quantita INTEGER NOT NULL DEFAULT 1,
            FOREIGN KEY (ProdottoId) REFERENCES Prodotti(Id)
        );";

        using (var command = new SqliteCommand(createCarrelloTable, connection))
        {
            command.ExecuteNonQuery();
        }

        // Svuotare il carrello all'avvio
        var clearCarrello = "DELETE FROM Carrello;";
        using (var command = new SqliteCommand(clearCarrello, connection))
        {
            command.ExecuteNonQuery();
        }

        // Seed dei dati per Categorie solo la prima volta
        var countCommand = new SqliteCommand("SELECT COUNT(*) FROM Categorie", connection);
        var count = (long)countCommand.ExecuteScalar();

        if (count == 0)
        {
            var insertCategorie = @"
            INSERT INTO Categorie (Nome) VALUES 
            ('Pasta'),
            ('Verdure'),
            ('Condimenti');";

            using (var command = new SqliteCommand(insertCategorie, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Seed dei dati per Prodotti (solo se non esistono già)
        countCommand = new SqliteCommand("SELECT COUNT(*) FROM Prodotti", connection);
        count = (long)countCommand.ExecuteScalar();

        if (count == 0)
        {
            var insertProdotti = @"
            INSERT INTO Prodotti (Nome, Prezzo, CategoriaId) VALUES 
            ('Trofie', 1.59, (SELECT Id FROM Categorie WHERE Nome = 'Pasta')),
            ('Aglio', 0.59, (SELECT Id FROM Categorie WHERE Nome = 'Verdure')),
            ('Pesto', 2.59, (SELECT Id FROM Categorie WHERE Nome = 'Condimenti'));";

            using (var command = new SqliteCommand(insertProdotti, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    // Metodo per ottenere la connessione al database da altre parti del codice
    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}

```


# PROSSIMI PASSI DA FARE:

- Aggiungere la funzionalità di carrello:

    - Modifico `DatabaseInitializer.cs` per aggiungere la tabella Carrello e fare in modo che il carrello sia sempre vuoto all'avvio:
    
    ```csharp
    
    // Aggiungo la tabella Carrello
    var createCarrelloTable = @"
    CREATE TABLE IF NOT EXISTS Carrello
    (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        ProdottoId INTEGER NOT NULL,
        Quantita INTEGER NOT NULL DEFAULT 1,
        FOREIGN KEY (ProdottoId) REFERENCES Prodotti(Id)
    );";

    using (var command = new SQLiteCommand(createCarrelloTable, connection))
    {
        command.ExecuteNonQuery();
    }

    ```
    
    - Struttura della tabella:
    
    - `Id`: chiave primaria del carrello
    - `ProdottoId`: riferimento al prodotto
    - `Quantita`: quantità del prodotto nel carrello


    - Creo il modello `Cart.cs`:

    ```csharp

    namespace ProdottiApp.Model
    {
        public class CartItem
        {
            public int Id { get; set; }  // Identificativo del prodotto
            public string Nome { get; set; } = string.Empty;
            public double Prezzo { get; set; }
            public int Quantita { get; set; }  // Quantità nel carrello
            public double Totale => Prezzo * Quantita;  // Prezzo totale per questo prodotto
        }
    }
    
    ```  
    
    - Modifico `InitializerDatabase()` per svuotare il carrello quando l'app viene avviata:
        
        ```csharp
        
        public static void InitializerDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            // Creazione tabella Carrello
            var createCarrelloTable = @"
            CREATE TABLE IF NOT EXISTS Carrello
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProdottoId INTEGER NOT NULL,
                Quantita INTEGER NOT NULL DEFAULT 1,
                FOREIGN KEY (ProdottoId) REFERENCES Prodotti(Id)
            );";

            using (var command = new SQLiteCommand(createCarrelloTable, connection))
            {
                command.ExecuteNonQuery();
            }
            
            // Svuota il carrello all'avvio
            var clearCarrello = "DELETE FROM Carrello;";
            using (var command = new SQLiteCommand(clearCarrello, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        ```

    - Creo `Cart.cshtml.cs` ed il metodo `OnPost`:
        
    ```csharp    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Data.SQLite;
    
    public class CartModel : PageModel
    {
        public List<CartItem> Carrello { get; set; } = new();
        
        public void OnGet()
        {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();
    
            var sql = @"
            SELECT c.ProdottoId, p.Nome, p.Prezzo, c.Quantita
            FROM Carrello c
            JOIN Prodotti p ON c.ProdottoId = p.Id";

            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Carrello.Add(new CartItem
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Prezzo = reader.GetDouble(2),
                    Quantita = reader.GetInt32(3)
                });
            }
        }

        public IActionResult OnPost(int? addId, int? removeId, bool clear = false)
        {
            using var connection = DatabaseInitializer.GetConnection();
            connection.Open();

            if (addId.HasValue)
            {
                var sql = @"
                INSERT INTO Carrello (ProdottoId, Quantita)
                VALUES (@prodottoId, 1)
                ON CONFLICT(ProdottoId) DO UPDATE SET Quantita = Quantita + 1;";

                using var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@prodottoId", addId.Value);
                command.ExecuteNonQuery();
            }
            else if (removeId.HasValue)
            {
                var sql = "DELETE FROM Carrello WHERE ProdottoId = @prodottoId;";
                using var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@prodottoId", removeId.Value);
                command.ExecuteNonQuery();
            }
            else if (clear)
            {
                var sql = "DELETE FROM Carrello;";
                using var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }

            return RedirectToPage();
        }
    }

    ```

    `addId`: Aggiunge un prodotto
    `removeId`: Rimuove un prodotto
    `clear = true`: Svuota tutto il carrello


    - Creo `Cart.cshtml`:


    ```html

    @page
    @model CartModel
    @{
        ViewData["Title"] = "Carrello";
    }

    <h2>Carrello</h2>

    @if (Model.Carrello.Count == 0)
    {
        <p>Il carrello è vuoto.</p>
    }
    else
    {
    <table class="table">
            <thead>
                <tr>
                    <th>Nome</th>
                    <th>Prezzo</th>
                    <th>Quantità</th>
                    <th>Totale</th>
                    <th>Azioni</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model.Carrello)
                {
                    <tr>
                        <td>@item.Nome</td>
                        <td>@item.Prezzo €</td>
                        <td>@item.Quantita</td>
                        <td>@(item.Prezzo * item.Quantita) €</td>
                        <td>
                            <form method="post">
                                <input type="hidden" name="addId" value="@item.Id" />
                                <button type="submit" class="btn btn-success">+</button>
                            </form>
                            <form method="post">
                                <input type="hidden" name="removeId" value="@item.Id" />
                                <button type="submit" class="btn btn-danger">-</button>
                            </form>
                        </td>
                    </tr>
                }
            </tbody>
        </table>

        <form method="post">
            <input type="hidden" name="clear" value="true" />
            <button type="submit" class="btn btn-warning">Svuota Carrello</button>
        </form>
    }

    ```

    - Il pulsante `+` invia `addId` a `OnPost()`
    - Il pulsante `-` invia `removeId` a `OnPost()`
    - Il pulsante `Svuota Carrello` invia `clear = true`


    - Aggiungo il pulsante in `Prodotti.cshtml`:

    ```html

    <form method="post" asp-page="/Cart">
    <input type="hidden" name="addId" value="@prodotto.Id" />
    <button type="submit" class="btn btn-primary">Aggiungi al carrello</button>
    </form>

    ```