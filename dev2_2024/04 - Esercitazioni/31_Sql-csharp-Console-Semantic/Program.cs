
using System.Data.SQLite;
//using + importazione pacchetto
//dotnet add package System.Data.SQLite <-- comando
string path = @"database.db"; //la rotta del file del database

if (!File.Exists(path))// se il file del database mon esiste
{
    SQLiteConnection.CreateFile(path); // crea il file del database
                                       //crea la connessione al database la versione 3 è un' indicazione della versione del database e può essere personalizzata
    SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");
     connection.Open(); // apre la connessione al database

    string sql= @"
    CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);
    CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT, prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, FOREIGN KEY (id_categoria) REFERENCES categorie(id));
    INSERT INTO categorie (nome) VALUES ('categoria1'), ('categoria2'), ('categoria3'), ('categoria4');
    INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('prodotto1', 1, 10, 1), ('prodotto2', 2, 20, 2) ,('prodotto3',3, 30, 3), ('prodotto4', 4, 40, 4);
    ";

   SQLiteCommand command = new SQLiteCommand(sql, connection);
   command.ExecuteNonQuery();
   connection.Close();
}
while (true)
{
    Console.WriteLine("");
    Console.WriteLine("1. Inserisci prodotto");
    Console.WriteLine("2. Visualizza prodotto");
    Console.WriteLine("3. Elimina prodotto");
    Console.WriteLine("4. Modifica prodotto");
    Console.WriteLine("5. Inserisci categoria");
    Console.WriteLine("6. Visualizza categoria");
    Console.WriteLine("7. Elimina categoria");
    Console.WriteLine("8. Modifica categoria");
    Console.WriteLine("0. Esci");
    
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
        VisualizzaCategoria();
        Console.WriteLine("");
        ModificaProdotto();
    }
    else if (scelta == "5")
    {
         Console.Clear();
        InserisciCategoria();
    }
    else if (scelta == "6")
    {
         Console.Clear();
        VisualizzaCategoria();
    }
    else if (scelta == "7")
    {
        Console.Clear();
        VisualizzaCategoria();
        Console.WriteLine("");
        EliminaCategoria();
    }
    else if (scelta == "8")
    {
        Console.Clear();
        VisualizzaCategoria();
        Console.WriteLine("");
        ModificaCategoria();
    }
    else if (scelta == "0")
    {
        break;
    }
}
static void InserisciProdotto() //1
{
    Console.WriteLine("inserisci il nome del prodotto:");
    string nome = Console.ReadLine();
    Console.WriteLine("inserisci prezzo:");
    string prezzo = Console.ReadLine();
    Console.WriteLine("inserisci quantita:");
    string quantita = Console.ReadLine();
    Console.WriteLine("Inserisci l'id della categoria");
    string id_categoria = Console.ReadLine();
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('{nome}' , {prezzo} , {quantita} , {id_categoria})";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    //command.ExecuteNonQuery(); //crea il comando sql da eseguire sulla connessione al database
    command.ExecuteNonQuery();
    connection.Close(); // chiude la connessione al database
}


static void VisualizzaProdotti() //2
{
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();

    string sql = @"SELECT prodotti.id, prodotti.nome, prodotti.prezzo, prodotti.quantita, categorie.nome AS nome_categoria FROM prodotti JOIN categorie ON prodotti.id_categoria = categorie.id";
    // AS nome_categoria serve per rinominare la colonna in modo da poterla visualizzare co un nome diverso
    // JOIN serve per unire le tabelle in bas a una condizione
    // la consizione è che il campi id_categoria della tabella prodotti sia uguale al campo id della tabella categorie
 SQLiteCommand command = new SQLiteCommand(sql, connection);
 SQLiteDataReader reader = command.ExecuteReader();

 while(reader.Read())
 {
     Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, categoria: {reader["nome_categoria"]}");
 }
  connection.Close();// chiude la connessione al database
}
static void EliminaProdotto() //3
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
static void ModificaProdotto() //4
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
        Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]} id_categoria: {reader["id_categoria"]}");
        Console.WriteLine("inserisci il nuovo nome del prodotto: ");
        string nuovoNome = Console.ReadLine();
        Console.WriteLine("inserisci il nuovo prezzo: ");
        string nuovoPrezzo = Console.ReadLine();
        Console.WriteLine("inserisci la nuova quantita: ");
        string nuovaQuantita = Console.ReadLine();
        Console.WriteLine("Inserisci l'id del nuovo prodotto: ");
        string nuovoIdcategoria = Console.ReadLine();

        sql = $"UPDATE prodotti SET nome = '{nuovoNome}', prezzo = {nuovoPrezzo}, quantita = {nuovaQuantita}, id_categoria = {nuovoIdcategoria} WHERE nome = '{nome}' ";
        command = new SQLiteCommand(sql, connection);// crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
        reader.Close();
    }
    connection.Close(); // chiude la connessione al database
}
static void InserisciCategoria() //5
{ 
    Console.WriteLine("inserisci il nome della categoria:");
    string nome = Console.ReadLine();
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"INSERT INTO categorie (nome) VALUES ('{nome}')";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    //command.ExecuteNonQuery(); //crea il comando sql da eseguire sulla connessione al database
    command.ExecuteNonQuery();
    connection.Close(); // chiude la connessione al database
}
static void VisualizzaCategoria() //6
{
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();

    string sql = @"SELECT * FROM categorie" ;
    // AS nome_categoria serve per rinominare la colonna in modo da poterla visualizzare co un nome diverso
    // JOIN serve per unire le tabelle in bas a una condizione
    // la consizione è che il campi id_categoria della tabella prodotti sia uguale al campo id della tabella categorie
 SQLiteCommand command = new SQLiteCommand(sql, connection);
 SQLiteDataReader reader = command.ExecuteReader();

 while(reader.Read())
 {
     Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]},");
 }
  connection.Close();// chiude la connessione al database
}
static void EliminaCategoria() //7
{
    Console.WriteLine("inserisci il nome della categoria:");
    string nome = Console.ReadLine();

    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"DELETE FROM categorie WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    command.ExecuteNonQuery(); //crea il comando sql da eseguire sulla connessione al database
    connection.Close(); // chiude la connessione al database
}
static void ModificaCategoria() //8
{
    Console.WriteLine("inserisci il nome della categoria da modificare:");
    string nome = Console.ReadLine();

    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");// crea la connessione al database
    connection.Open(); //apre la connessione al database
    string sql = $"SELECT * FROM categorie WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection); //crea il comando sql da eseguire sulla connessione al database
    SQLiteDataReader reader = command.ExecuteReader();// esegue il comando sql sulla connessione al database e salva i dati in reader
                                                      // che è un oggwtto di tipo SQLiteReader incaricato di leggere i dati
    if (reader.Read())
    {
        Console.WriteLine($"nome: {reader["nome"]}");
        Console.WriteLine("inserisci il nuovo nome della categoria: ");
        string nuovoNome = Console.ReadLine();

        sql = $"UPDATE categorie SET nome = '{nuovoNome}' WHERE nome = '{nome}' ";
        command = new SQLiteCommand(sql, connection);// crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
        reader.Close();
    }
    connection.Close(); // chiude la connessione al database
   
}
static void VisualizzaCategoriaDesc() // funzione per visualizzare il comando sqlite dalla console (DAA RIVEDERE)
{
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();

    string ordineDesc = @"SELECT * FROM categorie ORDER BY DESC" ;
    // AS nome_categoria serve per rinominare la colonna in modo da poterla visualizzare co un nome diverso
    // JOIN serve per unire le tabelle in bas a una condizione
    // la consizione è che il campi id_categoria della tabella prodotti sia uguale al campo id della tabella categorie
 SQLiteCommand command = new SQLiteCommand(ordineDesc, connection);
 SQLiteDataReader reader = command.ExecuteReader();

 while(reader.Read())
 {
     Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]},");
 }
  connection.Close();// chiude la connessione al database
}


