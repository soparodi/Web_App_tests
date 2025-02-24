using Microsoft.Data.Sqlite; // Importa il namespace per lavorare con SQLite

public class DatabaseInitializer
{
    // Stringa di connessione al database SQLite, specifica il file in cui sarà salvato
    private static string _connectionString = "Data Source=prodottiapp.db"; 

    // Inizializza il database creando le tabelle necessarie e facendo il seeding dei dati iniziali
    public static void InitializeDatabase()
    {
        // Crea una connessione al database e la chiude automaticamente dopo l'uso
        using var connection = new SqliteConnection(_connectionString);
        connection.Open(); // Apre la connessione al database

        // Creazione tabella Categorie se non esiste
        var createCategorieTable = @"
        CREATE TABLE IF NOT EXISTS Categorie (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL
        );";
        ExecuteNonQuery(connection, createCategorieTable);

        // Creazione tabella Fornitori se non esiste
        var createFornitoriTable = @"
        CREATE TABLE IF NOT EXISTS Fornitori (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL
        );";
        ExecuteNonQuery(connection, createFornitoriTable);

        // Creazione tabella Prodotti se non esiste
        var createProdottiTable = @"
        CREATE TABLE IF NOT EXISTS Prodotti (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Prezzo REAL NOT NULL,
            CategoriaId INTEGER,
            FornitoreId INTEGER,
            FOREIGN KEY(CategoriaId) REFERENCES Categorie(Id),
            FOREIGN KEY(FornitoreId) REFERENCES Fornitori(Id)
        );";
        ExecuteNonQuery(connection, createProdottiTable);

        // Inserisce i dati iniziali se necessario
        SeedData(connection);
    }

    // Inserisce dati predefiniti nelle tabelle Categorie, Fornitori e Prodotti se queste sono vuote.
    private static void SeedData(SqliteConnection connection)
    {
        // Seeding Categorie: Inserisce i dati solo se la tabella è vuota
        if (GetTableCount(connection, "Categorie") == 0)
        {
            var insertCategorie = @"
            INSERT INTO Categorie (Nome) VALUES 
            ('Pasta'),
            ('Verdure'),
            ('Condimenti');";
            ExecuteNonQuery(connection, insertCategorie);
        }

        // Seeding Fornitori: Inserisce i dati solo se la tabella è vuota
        if (GetTableCount(connection, "Fornitori") == 0)
        {
            var insertFornitori = @"
            INSERT INTO Fornitori (Nome) VALUES
            ('Michele'),
            ('Giovanni'),
            ('Oreste');";
            ExecuteNonQuery(connection, insertFornitori);
        }

        // Seeding Prodotti: Inserisce i dati solo se la tabella è vuota
        if (GetTableCount(connection, "Prodotti") == 0)
        {
            var insertProdotti = @"
            INSERT INTO Prodotti (Nome, Prezzo, CategoriaId, FornitoreId) VALUES 
            ('Trofie', 1.59, (SELECT Id FROM Categorie WHERE Nome = 'Pasta'), (SELECT Id FROM Fornitori WHERE Nome = 'Michele')),
            ('Aglio', 0.59, (SELECT Id FROM Categorie WHERE Nome = 'Verdure'), (SELECT Id FROM Fornitori WHERE Nome = 'Giovanni')),
            ('Pesto', 2.59, (SELECT Id FROM Categorie WHERE Nome = 'Condimenti'), (SELECT Id FROM Fornitori WHERE Nome = 'Oreste'));";
            ExecuteNonQuery(connection, insertProdotti);
        }
    }

    // Esegue una query SQL che non restituisce risultati (CREATE, INSERT, UPDATE, DELETE)
    private static void ExecuteNonQuery(SqliteConnection connection, string query)
    {
        using var command = new SqliteCommand(query, connection);
        command.ExecuteNonQuery(); // Esegue la query senza restituire dati
    }

    // Restituisce il numero di record presenti in una tabella specificata
    private static long GetTableCount(SqliteConnection connection, string tableName)
    {
        using var command = new SqliteCommand($"SELECT COUNT(*) FROM {tableName}", connection);
        return (long)command.ExecuteScalar(); // Ritorna il numero di righe nella tabella
    }

    // Restituisce una nuova connessione al database SQLite
    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString); // Crea una connessione senza aprirla
    }
}