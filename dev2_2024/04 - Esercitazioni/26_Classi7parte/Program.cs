﻿
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
            Console.Write("\nScelta: ");
            string scelta = Console.ReadLine();

            // switch-case per gestire le scelte dell'utente che usa scelta come variabile di controllo
            switch (scelta)
            {
                case "1":
                    Console.WriteLine("\nProdotti:");

                    // Visualizzare i prodotti con il metodo OttieniProdotti della classe ProdottoAdvancedManager (manager)
                    foreach (var prodotto in manager.OttieniProdotti())
                    {
                        Console.WriteLine($"ID: {prodotto.Id}, Nome: {prodotto.NomeProdotto}, Prezzo: {prodotto.PrezzoProdotto}, Giacenza: {prodotto.GiacenzaProdotto}");
                    }
                    break;
                case "2":
                    // Console.Write("ID: ");
                    // int id = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Prezzo: ");
                    decimal prezzo = decimal.Parse(Console.ReadLine());
                    Console.Write("Giacenza: ");
                    int giacenza = int.Parse(Console.ReadLine());
                    manager.AggiungiProdotto(new ProdottoAdvanced {NomeProdotto = nome, PrezzoProdotto = prezzo, GiacenzaProdotto = giacenza });
                    break;
                case "3":
                    Console.Write("ID: ");
                    int idProdotto = int.Parse(Console.ReadLine());
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
                    int idProdottoDaAggiornare = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    string nomeNuovo = Console.ReadLine();
                    Console.Write("Prezzo: ");
                    decimal prezzoNuovo = decimal.Parse(Console.ReadLine());
                    Console.Write("Giacenza: ");
                    int giacenzaNuova = int.Parse(Console.ReadLine());
                    manager.AggiornaProdotto(idProdottoDaAggiornare, new ProdottoAdvanced {NomeProdotto = nomeNuovo, PrezzoProdotto = prezzoNuovo, GiacenzaProdotto = giacenzaNuova });
                    break;
                case "5":
                    Console.Write("ID: ");
                    int idProdottoDaEliminare = int.Parse(Console.ReadLine());
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
    private int prossimoId;
     private List<ProdottoAdvanced> prodotti; 
    private ProdottoRepository repository; // prodotti e private perche non voglio che venga modificato dall'esterno

    public ProdottoAdvancedManager(List<ProdottoAdvanced> Prodotti)
    {
        prodotti = Prodotti;
        repository = new ProdottoRepository(); // inizializzo la lista di prodotti nel costruttore pubblco in modo che sia accessibile all'esterno
        prossimoId =1;
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

}

public class ProdottoRepository
{
 
    private readonly string folderPath  = "Prodotti"; //crea per il file json
      public void SalvaProdotti(List<ProdottoAdvanced> prodotti)
    {
        if (! Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var prodotto in prodotti)
        {
            string filePath = Path.Combine(folderPath,$"{prodotto.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(prodotto, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine($"Prodotto salvato in {filePath}: \n");
        }
    }

    public List<ProdottoAdvanced> CaricaProdotti()
    {
        
            List<ProdottoAdvanced> prodotti = new  List<ProdottoAdvanced> ();
            if (Directory.Exists(folderPath))
            {
                foreach(var file in Directory.GetFiles(folderPath,"*.json"))
                {
                    string readJsonData= File.ReadAllText(file);
                    ProdottoAdvanced prodotto = JsonConvert.DeserializeObject<ProdottoAdvanced>(readJsonData) ;
                    prodotti.Add(prodotto);
                }
            }
            return prodotti;

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
