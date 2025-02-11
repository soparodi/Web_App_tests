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
            using (var command = new SqliteCommand(createCategorieTable, connection));

            {
                command.ExecuteNonQuery(); // eseguiamo il comando sql però non ritorna nulla, lo crea e basta
            }

            var createCategorieTable = @"
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
            using (var command = new SqliteCommand(createProdottiTable, connection));

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
                ('Trofie', 1,59, (SELECT Id FROM Categorie WHERE Nome = 'Pasta')),
                ('Aglio', 0,59, (SELECT Id FROM Categorie WHERE Nome = 'Verdure')),
                ('Pesto', 2,59, (SELECT Id FROM Categorie WHERE Nome = 'Condimenti')),
                ";

                // lancio il comando sulla connessione che ho appena creato
                using (var command = new SqliteCommand(insertCategorie, connection))
                {
                    command.ExecuteNonQuery();
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