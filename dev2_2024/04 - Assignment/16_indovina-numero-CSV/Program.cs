/*


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

Console.WriteLine($"Hai indovinato! Il numero era: {numeroDaIndovinare}");
Console.WriteLine($"Hai fatto {tentativi} tentativi.");


File.AppendAllText(nomeFile, $"Hai indovinato! Il numero era: {numeroDaIndovinare}. Tentativi: {tentativi}\n");

//Leggere da un file
// string content = File.ReadAllText(ennesimoTentativo.txt);
//stampo il contenuto del file
Console.WriteLine("Contenuto del file CSV");

string[] lines = File.ReadAllLines(csvFile);
foreach (var line in lines)
{
    Console.WriteLine(line);
}
*/



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

        // Percorso del file CSV per le partite salvate
        string csvFile = "partite_salvate.csv";

        // Se il file CSV non esiste, crealo con le intestazioni
        if (!File.Exists(csvFile))
        {
            File.AppendAllText(csvFile, "NomeGiocatore,Punteggio,Tentativi\n");
        }

        while (numeroInserito != numeroDaIndovinare)
        {
            Console.WriteLine("Indovina il numero tra 1 e 10");
            try
            {
                numeroInserito = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Inserisci un numero valido.");
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

        Console.WriteLine($"Hai indovinato! Il numero era: {numeroDaIndovinare}");
        Console.WriteLine($"Hai fatto {tentativi} tentativi.");

        // Salvo il risultato finale nel file di testo
        File.AppendAllText(nomeFile, $"Hai indovinato! Il numero era: {numeroDaIndovinare}. Tentativi: {tentativi}\n");

        // Scrivo i dati nel file CSV
        File.AppendAllText(csvFile, $"{nomeUtente},{numeroDaIndovinare},{tentativi}\n");

        Console.WriteLine("Dati salvati nel file CSV.");

        // Per visualizzare i dati salvati nel CSV
        Console.WriteLine("Contenuto del file CSV:");
        string[] lines = File.ReadAllLines(csvFile);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }