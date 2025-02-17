```csharp
using MicroSoft.Data.Sqlite;

public static DbUtils
{
    /// <summary>
    /// Esegue una query che non restituisce i dati (INSERT, UPDATE, DELETE).
    /// </summary>
    /// <param name="sql">La query SQL.</param>
    /// <param name="setupParameters">Opzionale: callback per aggiungere parametri al comando.</param>
    /// <returns>Il numero di righe interessate.</returns>

    public static int ExecuteNonQuery(string sql, Action<SqliteCommand> setupParameters = null)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();
        using var command = new SqliteCommand(sql, connection);
        setupParameters?.Invoke(command); // il metodo invoke esegue il delegate setupParameters, cioè la funzione che gli passiamo
        return command.ExecuteNonQuery();
    }
    /// <summary>
    /// Esegue una query che restituisce un valore scalare.
    /// </summary>
    /// <typeparam name="T">Il tipo del valore atteso.</typeparam>
    /// <typeparam name="sql">La query SQL.</typeparam>
    /// <param name="setupParameters">Opzionale: callback per aggiungere parametri al comando.</param>
    /// <returns>Il valore restituito convertito al tipo T.</returns>
    public static T ExecuteScalar<T>(string sql, Action<SqliteCommand> setupParameters = null)
    {
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();
        using var command = new SqliteCommand(sql, connection);
        setupParameters?.Invoke(command); // il metodo invoke esegue il delegate setupParameters, cioè la funzione che gli passiamo
        var result = command.ExecuteScalar();
        if(result == null || result == DBNull.Value)
        return default(T);
        return (T)Convert.ChangeType(result, typeof(T));
    }
    /// <summary>
    /// Esegue una query che restituisce più righe e le converte in una lista di oggetti di tipo T.
    /// </summary>
    /// <typeparam name="T">Il tipo di oggetto da restituire per ogni riga.</typeparam>
    /// <param name="sql">La query SQL.</param>
    /// <param name="setupParameters">Opzionale: callback per aggiungere parametri al comando.</param>
    /// <returns>Una lista di oggetti di tipo T.</returns>
    public static List<T> EvecuteReader<T>(string sql, Func<SqliteDataReader, T> converter, Action<SqliteCommand> setupParameters)
    {
        var list = new List<T>();
        using var connection = DatabaseInitializer.GetConnection();
        connection.Open();
        using var command = new SqliteCommand(sql, connection);
        setupParameters?.Invoke(command); // il metodo invoke esegue il delegate setupParameters, cioè la funzione che gli passiamo
        using var reader = command.ExecuteReader();
        while(reader.Read())
        {
            list.Add(converter(reader));
        }
        return list;
    }
}
```

```csharp
using System.IO;
public static class SimpleLogger
{
    // Percorso del file di log (puoi modificare il percorso se necessario)
    private static readonly string logFilePath = "log.txt";
    /// <summary>
    /// Registra un messaggio nel file di log con data e ora.
    /// </summary>
    /// <param name="message">Il messaggio da loggare.</param>
    public static void Log(string message)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(logFilePath, append: true);
            writer.WriteLine($"{DateTime.Now:yyy-MM-dd HH:mm:ss} - {message}");
        }
        catch(Exception)
        {
            // Se il logging fallisce, l'errore viene ignorato.
        }
    }
    /// <summary>
    /// Registra un'eccezione nel file di log.
    /// </summary>
    /// <param name="ex">L'eccezione da loggare.</param>
    public static void Log(Exception ex)
    {
        Log($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
    }
}
```

Pages/Prodotti/Index.cshtml.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

public class ProdottiModel : PageModel
{
    // Creo una proprietà pubblica di tipo lista di prodotti view model
    public List<ProdottoViewModel> Prodotti { get; set; } = new List<ProdottoViewModel>(); // la inizializzo come una lista vuota

    public void OnGet()
    {
        try
        {
            // Utilizzo di DbUtils per leggere la lista dei prodotti
            Prodotti = DbUtils.ExecuteReader(@"
            SELECT p.Id, p.Nome, p.Prezzo, c.Nome as CategoriaNome
            FROM Prodotti p
            LEFT JOIN Categorie c ON p.CategoriaId = c.Id";
            reader => new ProdottoViewModel
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Prezzo = reader.GetDouble(2),
                    CategoriaNome = reader.IsDBNull(3) ? "Nessuna" : reader.GetString(3) // evitiamo le {} e quindi if, usando un operatore ternario
                }
            );
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
    }
}
```