## Indovina numero con persistenza dei dati

- Implementare la persistenza dei dati.
- Implementare una funzione che scriva l'elenco dei tentativi fatti dall'utente su un file di testo.
- Il programma chiede all'utente di inserire il proprio nome ed usa l'input dell'utente per creare un file di testo col nome dell'utente.

```csharp

Random random = new Random();
int numeroDaIndovinare = random.Next(1, 11);

    Console.Write("Inserisci il tuo nome: ");
    string nomeUtente = Console.ReadLine().Trim();

    // file di testo con nome utente
    string nomeFile = $"{nomeUtente}.txt";

    // Se il file non esiste
    if (!File.Exists(nomeFile))
    {
        File.Create(nomeFile).Close();  // Creo il file vuoto
    }

    int numeroInserito = 0;
    int tentativi = 0;

    while (numeroInserito != numeroDaIndovinare)
    {

        Console.WriteLine("Indovina il numero tra 1 e 10");
        try
        {
            numeroInserito = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("inserisci un numero valido.");
            continue;
        }


        tentativi++;

        // Salvo il tentativo nel file
        File.AppendAllText(nomeFile, $"Tentativo {tentativi}: {numeroInserito}\n");


        if (numeroInserito < numeroDaIndovinare)
        {
            Console.WriteLine("Il numero da indovinare è maggiore.");
        }
        else if (numeroInserito > numeroDaIndovinare)
        {
            Console.WriteLine("Il numero da indovinare è minore.");
        }
    }

    Console.WriteLine($"Hai indovinato! Il numero da indovinare era: {numeroDaIndovinare}");
    Console.WriteLine($"Hai fatto {tentativi} tentativi.");


    File.AppendAllText(nomeFile, $"Hai indovinato! Il numero da indovinare era: {numeroDaIndovinare}. Tentativi: {tentativi}\n");
    
    ```

  
    //StreamWriter
    //esempio
void ScriviTentativiSuFile(Dictionary<string, List<int> tentativiUtenti, string nomeUtente)
{
    using (StreamWriter sw = new StreamWriter($"{nomeUtente}.txt"))
    {
        foreach (var tentativoUtente in tentativiUtenti)
        {
            if (tentativoUtente.Key == nomeUtente)
            {
                sw.WriteLine($"{tentativoUtente.Key}: {string.Join(",", tentativoUtente.Value)}");
            }
        }
    }
}

```