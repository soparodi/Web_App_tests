
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {
        // Creare un oggetto di tipo ProdottoRepository per gestire il salvataggio e il caricamento dei dati
        ProdottoRepository repository = new ProdottoRepository();

        // Caricare i dati da file con il metodo CaricaProdotti della classe ProdottoRepository (repository)
        List<ProdottoAdvanced> prodotti = repository.CaricaProdotti();

        // Creare un oggetto di tipo ProdottoAdvancedManager per gestire i prodotti
        ProdottoAdvancedManager manager = new ProdottoAdvancedManager(prodotti);

        // Menu interattivo per eseguire operazioni CRUD sui prodotti

        // variabile per controllare se il programma deve continuare o uscire
        bool continua = true;

        // il ciclo while continua finché la variabile continua è true
        while (continua)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Visualizza Prodotti");
            Console.WriteLine("2. Aggiungi Prodotto");
            Console.WriteLine("3. Trova Prodotto per ID");
            Console.WriteLine("4. Aggiorna Prodotto");
            Console.WriteLine("5. Elimina Prodotto");
            Console.WriteLine("6. Esci");

            // acquisire l'input dell'utente
            //Console.Write("\nScelta: ");
            //string scelta = Console.ReadLine();
            //string scelta = acquisire mediante il metodo LeggiInteri della classe InputManager
            string scelta = InputManager.LeggiIntero("Scelta", 1, 6).ToString();
            //pulisco la console
            Console.Clear();

            // switch-case per gestire le scelte dell'utente che usa scelta come variabile di controllo
            switch (scelta)
            {
                case "1":
                    Console.WriteLine("\nProdotti:");
                    //string scelta = Console.ReadLine();
                    //string scelta = acquisita mediante il metodo LeggiInteri della classe InputManager
                    //string scelta = InputManager.LeggiIntero();

                    // Visualizzare i prodotti con il metodo OttieniProdotti della classe ProdottoAdvancedManager (manager)
                    manager.StampaProdottiIncolonnati();
                    break;
                case "2":
                    // Console.Write("ID: ");
                    // int id = int.Parse(Console.ReadLine());
                    //Console.Write("Nome: ");
                    //string nome = Console.ReadLine();
                    //acquisisco il nome mediante il metodo LeggiStringa della classe InputManager
                    string nome = InputManager.LeggiStringa("\nNome: ");
                    //Console.Write("Prezzo: ");
                    //decimal prezzo = decimal.Parse(Console.ReadLine());
                    //acquisisco il prezzo mediante il metodo LeggiDecimale della classe InputManager
                    decimal prezzo = InputManager.LeggiDecimale("\nPrezzo: ");
                    //Console.Write("Giacenza: ");
                    //int giacenza = int.Parse(Console.ReadLine());
                    //acuqisisco giacenza mediante il metodo LeggiIntero della classe InputManager
                    int giacenza = InputManager.LeggiIntero("\nGiacenza: ");
                    manager.AggiungiProdotto(new ProdottoAdvanced { NomeProdotto = nome, PrezzoProdotto = prezzo, GiacenzaProdotto = giacenza });
                    break;
                case "3":
                    Console.Write("ID: ");
                    int idProdotto = InputManager.LeggiIntero("\nID");
                    ProdottoAdvanced prodottoTrovato = manager.TrovaProdotto(idProdotto);
                    if (prodottoTrovato != null)
                    {
                        Console.WriteLine($"\nProdotto trovato per ID {idProdotto}: {prodottoTrovato.NomeProdotto}");
                    }
                    else
                    {
                        Console.WriteLine($"\nProdotto non trovato per ID {idProdotto}");
                    }
                    break;
                case "4":
                    Console.Write("ID: ");
                    int idProdottoDaAggiornare = InputManager.LeggiIntero("\nID:");
                    Console.Write("Nome: ");
                    string nomeNuovo = InputManager.LeggiStringa("\nNome: ");
                    Console.Write("Prezzo: ");
                    decimal prezzoNuovo = InputManager.LeggiDecimale("\nPrezzo: ");
                    Console.Write("Giacenza: ");
                    int giacenzaNuova = InputManager.LeggiIntero("\nGiacenza:");
                    manager.AggiornaProdotto(idProdottoDaAggiornare, new ProdottoAdvanced { NomeProdotto = nomeNuovo, PrezzoProdotto = prezzoNuovo, GiacenzaProdotto = giacenzaNuova });
                    break;
                case "5":
                    int idProdottoDaEliminare = InputManager.LeggiIntero("\nID");
                    //Console.Write("ID: ");
                    //int idProdottoDaEliminare = int.Parse(Console.ReadLine());

                    manager.EliminaProdotto(idProdottoDaEliminare);
                    break;
                case "6":
                    repository.SalvaProdotti(manager.OttieniProdotti());
                    continua = false; // imposto la variabile continua a false per uscire dal ciclo while
                    break;
                default:
                    Console.WriteLine("Scelta non valida. Riprovare.");
                    break;
                
            }
        }
    }
}

public class ProdottoAdvanced

{
    private int id;
    public int Id
    {
        get { return id; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Il valore dell'ID deve essere maggiore di zero.");
            }
            id = value;
        }
    }

    private string nomeProdotto;
    public string NomeProdotto
    {
        get { return nomeProdotto; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Il nome del prodotto non può essere vuoto.");
            }
            nomeProdotto = value;
        }
    }

    private decimal prezzoProdotto;
    public decimal PrezzoProdotto
    {
        get { return prezzoProdotto; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Il prezzo deve essere maggiore di zero.");
            }
            prezzoProdotto = value;
        }
    }

    private int giacenzaProdotto;
    public int GiacenzaProdotto
    {
        get { return giacenzaProdotto; }
        set { giacenzaProdotto = value; }
    }
}

public class ProdottoAdvancedManager

{

    //lista di prodotti di tipo ProddottoAdvanced per memorizzare i prodotti
    private List<ProdottoAdvanced> prodotti;
    private ProdottoRepository repository; // prodotti e private perche non voglio che venga modificato dall'esterno
    private int prossimoId;

    public ProdottoAdvancedManager(List<ProdottoAdvanced> Prodotti)
    {
        prodotti = Prodotti;
        repository = new ProdottoRepository(); // inizializzo la lista di prodotti nel costruttore pubblco in modo che sia accessibile all'esterno
        prossimoId = 1;
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Id >= prossimoId)
            {
                prossimoId = prodotto.Id + 1;
            }
        }
    }

    // metodo per aggiungere un prodotto alla lista
    public void AggiungiProdotto(ProdottoAdvanced prodotto)
    { //assegna automaticamente un ID univoco
        prodotto.Id = prossimoId;
        //incrementa il prossimo ID per il prossim prodotto
        prossimoId++;
        prodotti.Add(prodotto);
        Console.WriteLine($"Prodotto aggiunto con ID: {prodotto.Id}");
    }

    // metodo per visualizzare la lista di prodotti
    public List<ProdottoAdvanced> OttieniProdotti()
    {
        return prodotti;
    }
    //ongi campo utilizza il formato {campo,-largezza} dove:
    //campo è il valore da stampare
    //-larghezza specifica la larghezza del campo; il il segno - allinea il testo a sinistra.
    //{"Nome". -20}significa che il nome del prodotto avrà una largezza fissa di 20 caratteri, allineato a sinisitra
    //Formato dei numeri:
    // Per i prezzi, viene usato il formato 0.00 per mostrare sempre due cifre decimali
    // Linea Console.WriteLine(new string ('-', 50)); stampa una linea divisoria lung 50 caratteri per migliorare la leggibilità
    public void StampaProdottiIncolonnati()
    {


        // Intestazioni con larghezza fissa
        Console.WriteLine(
            $"{"ID",-5} {"Nome",-20} {"Prezzo",-10} {"Giacenza",-10}"
        );
        Console.WriteLine(new string('-', 50)); // Linea separatrice

        // Stampa ogni prodotto con larghezza fissa
        foreach (var prodotto in prodotti)
        {
            Console.WriteLine(
                $"{prodotto.Id,-5} {prodotto.NomeProdotto,-20} {prodotto.PrezzoProdotto,-10:0.00} {prodotto.GiacenzaProdotto,-10}"
            );
        }
    }

    // metodo per cercare un prodotto
    public ProdottoAdvanced TrovaProdotto(int id)
    {
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Id == id)
            {
                return prodotto;
            }
        }
        return null;
    }

    // metodo per mpdificare un prodotto esistente
    public void AggiornaProdotto(int id, ProdottoAdvanced nuovoProdotto)
    {
        var prodotto = TrovaProdotto(id);
        if (prodotto != null)
        {
            prodotto.NomeProdotto = nuovoProdotto.NomeProdotto;
            prodotto.PrezzoProdotto = nuovoProdotto.PrezzoProdotto;
            prodotto.GiacenzaProdotto = nuovoProdotto.GiacenzaProdotto;
        }
    }

    // metodo per eliminare un prodotto
    public void EliminaProdotto(int id)
    {
        var prodotto = TrovaProdotto(id);
        if (prodotto != null)
        {
            prodotti.Remove(prodotto);
            //elimina il file json corrispondente al  prodotto
            string filePath = Path.Combine("Prodotti", $"{id}.json");
            File.Delete(filePath);
            Console.WriteLine($"Prodotto eliminato: {filePath}");
        }
    }
    public void SalvaProdotti()
    {
        repository.SalvaProdotti(prodotti);
    }
}



public class ProdottoRepository
{

    private readonly string folderPath = "Prodotti"; //crea per il file json
    public void SalvaProdotti(List<ProdottoAdvanced> prodotti)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var prodotto in prodotti)
        {
            string filePath = Path.Combine(folderPath, $"{prodotto.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(prodotto, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine($"Prodotto salvato in {filePath}: \n");
        }
    }

    public List<ProdottoAdvanced> CaricaProdotti()
    {

        List<ProdottoAdvanced> prodotti = new List<ProdottoAdvanced>();
        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(file);
                ProdottoAdvanced prodotto = JsonConvert.DeserializeObject<ProdottoAdvanced>(readJsonData);
                prodotti.Add(prodotto);
            }
        }
        return prodotti;

    }
}

//classe gesstion einput che può esssere integrata per semplificare e standardizzare l'acquisizione degli input dell'utente.
//questa classe aiuta a gestire i casi di errore e fornisce metodi per acquisire input di diversi tipi

public static class InputManager
{
    //minvalue e maxvalue sono i metodi di int che rappresentano il valore minimo ed il valore massimo di un intero
    // il metodo LeggiIntero accetta un messaggio da visualizzare
    public static int LeggiIntero(string messaggio, int min = int.MinValue, int max = int.MaxValue)
    {
        int valore; //Vriabile per memorizzare il valore intero acquisito
        while (true)
        {
            Console.Write($"{messaggio}"); //messaggio e la variabile di input che dovrò passare al metodo
            string input = Console.ReadLine();//acquisire l'input dell'utente come stringa
            // try parse per convertire la stringa  in un intero e controllare se l'input è vaòidp
            if (int.TryParse(input, out valore) && valore >= min && valore <= max) // devo verifiare se il valore e tra min e max e se è un intero
            {
                return valore; // restituire il valore intero se è valido
            }
            else
            {
                Console.WriteLine($"Inserire un valore intero compreso tra {min} e {max}"); // messaggio di errore
            }


        }
    }

    public static decimal LeggiDecimale(string messaggio, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        decimal valore; //variabile per memorizzare il valore decimale acquisito
        while (true)
        {
            Console.Write($"{messaggio}");
            string input = Console.ReadLine();

            //sostituisco la virgola con il punto per gestire i decimali
            if (input.Contains(",")) //se l'input contiene la virgola e non contiene il punto
            {
                input = input.Replace(",", ","); //sostituire la virgola con il punto
            }

            // try parse per convertire la stringa in un decimale e controllare se l'input è valido
            if (decimal.TryParse(input, out valore) && valore >= min && valore <= max)
            {
                return valore;
            }
            Console.WriteLine($"errore: inserire un numero decimale comprso tra {min} e {max}");
        }
    }

    public static string LeggiStringa(string messaggio, bool obbligatorio = true)
    {
        while (true)
        {
            Console.Write($"{messaggio}"); // messaggio e la variabile di input che devo passare al metodo
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) || !obbligatorio) // se l'input non è vuoro o non è obbligtaorio
            {
                return input; // restituire il valore della stringa
            }
            Console.WriteLine($"errore: il valore non può essere vuot");
        }
    }

    public static bool LeggiConferma(string messaggio)
    {
        while (true)
        {
            Console.Write($"{messaggio} (s/n): ");
            string input = Console.ReadLine().ToLower();
            if (input == "s" || input == "si")
            {
                return true;
            }
            if (input == "n" || input == "no")
            {
                return false;
            }
            Console.WriteLine("errore: rispondere con 's' o 'n' ");
        }
    }
}
