
using System.Data.SQLite;
//using + importazione pacchetto
//dotnet add package System.Data.SQLite <-- comando
//creo rotta del file del database

string path = @"database.db"; //la rotta del file del database

if (!File.Exists(path))// se il file del database mon esiste
{
    SQLiteConnection.CreateFile(path); // crea il file del database
                                       //crea la connessione al database la versione 3 è un' indicazione della versione del database e può essere personalizzata
    SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");

    connection.Open(); // apre la connessione al database

    string sql = @"
CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, prezzo REAL, quantita INTEGER CHECK (quantita >=0));
INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('prodotto1' ,1, 10);
INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('prodotto2' ,2, 20);
INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('prodotto3' ,3, 30);
";
    //oppure insert multiplo cosi
    //INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('prodotto1',1, 10), ('prodotto2', 2, 20)...

    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
                                                                //sql è la stringa che contiene il comando sql

    command.ExecuteNonQuery(); // esegue il comando  sql sulla connessione al database nonquery significa che non si aspetta un risultato

    connection.Close(); //chiude la connessione al database
}
while (true)
{
    Console.WriteLine("");
    Console.WriteLine("1. Inserisci prodotto");
    Console.WriteLine("2. visualizza prodotto");
    Console.WriteLine("3. elimina prodotto");
    Console.WriteLine("4. modifica prodotto");
    Console.WriteLine("0. esci");
    string scelta = Console.ReadLine();
    if (scelta == "1")
    {
        Console.Clear();
        InserisciProdotto();
    }
    else if (scelta == "2")
    {
        Console.Clear();
        VisualizzaProdotti();
    }
    else if (scelta == "3")
    {
        Console.Clear();
        VisualizzaProdotti();
        Console.WriteLine("");
        EliminaProdotto();
    }
    else if (scelta == "4")
    {
        Console.Clear();
        VisualizzaProdotti();
        Console.WriteLine("");
        ModificaProdotto();
    }
    else if (scelta == "0")
    {
        break;
    }

}
static void InserisciProdotto()
{
    Console.WriteLine("inserisci il nome del prodotto:");
    string nome = Console.ReadLine();
    Console.WriteLine("inserisci prezzo:");
    string prezzo = Console.ReadLine();
    Console.WriteLine("inserisci quantita:");
    string quantita = Console.ReadLine();
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('{nome}' , {prezzo} , {quantita})";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    //command.ExecuteNonQuery(); //crea il comando sql da eseguire sulla connessione al database
    command.ExecuteNonQuery();
    connection.Close(); // chiude la connessione al database
}
static void VisualizzaProdotti()
{
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = "SELECT * FROM prodotti";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    SQLiteDataReader reader = command.ExecuteReader();// esegue il comando sql sulla connessione al database e  salva i dati in reader che è un oggwtto di tipo SQLiteReader incaricato di leggere i dati

    while (reader.Read())// legge i dati dal read finchè ce ne sono
    {
        Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]},");
    }
    connection.Close();// chiude la connessione al database
}
static void EliminaProdotto()
{
    Console.WriteLine("inserisci il nome del prodotto:");
    string nome = Console.ReadLine();

    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"DELETE FROM prodotti WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    command.ExecuteNonQuery(); //crea il comando sql da eseguire sulla connessione al database
    connection.Close(); // chiude la connessione al database
}
static void ModificaProdotto()
{
    Console.WriteLine("inserisci il nome del prodotto da modificare:");
    string nome = Console.ReadLine();

    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");// crea la connessione al database
    connection.Open(); //apre la connessione al database
    string sql = $"SELECT * FROM prodotti WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    SQLiteDataReader reader = command.ExecuteReader();// esegue il comando sql sulla connessione al database e salva i dati in reader
                                                      // che è un oggwtto di tipo SQLiteReader incaricato di leggere i dati
    if (reader.Read())
    {
        Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}");
        Console.WriteLine("inserisci il nuovo nome del prodotto:");
        string nuovoNome = Console.ReadLine();
        Console.WriteLine("inserisci il nuovo prezzo:");
        string nuovoPrezzo = Console.ReadLine();
        Console.WriteLine("inserisci la nuova quantita:");
        string nuovaQuantita = Console.ReadLine();

        sql = $"UPDATE prodotti SET nome = '{nuovoNome}', prezzo = {nuovoPrezzo}, quantita = {nuovaQuantita} WHERE nome = '{nome}' ";
        command = new SQLiteCommand(sql, connection);// crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
    }
    connection.Close(); // chiude la connessione al database
}
//console.clear all'inizio di tutti i case in questo caso gli if e self if.
// inserisco prima visualizza prodotti e creo linea vuota Console.WriteLine("");
