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

            // lancio il comando sulla coonessione che ho appena creato
            using (var command = new SqliteCommand(createCategorieTable, connection));

            {
                command.ExecuteNonQuery();
            }
    }
}