using Newtonsoft.Json;
//comando per installare il pacchetto using
//dotnet add package Newtonsoft.Json
class Program
{
    static void Main(string[] args)
    {
        List<ProdottoAdvanced> prodotti = new List<ProdottoAdvanced>
        {
            new ProdottoAdvanced { Id = 1, NomeProdotto = "Prodotto A" , PrezzoProdotto = 10.50m, GiacenzaProdotto = 100},
            new ProdottoAdvanced { Id = 2, NomeProdotto= "Prodotto B" , PrezzoProdotto = 20.75m, GiacenzaProdotto = 50}
        };
        string filePath = "prodotti.json";
        string jsonData = JsonConvert.SerializeObject(prodotti, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);

        Console.WriteLine($" Dati serializzati e salvati in {filePath}:\n{jsonData}\n");
        string readJson = File.ReadAllText(filePath);
        List<ProdottoAdvanced> prodottiDeserializzati = JsonConvert.DeserializeObject<List<ProdottoAdvanced>>(readJson);

        Console.WriteLine($"Dati deserializzati:");
        foreach (var prodotto in prodottiDeserializzati)
        {
            Console.WriteLine($"ID: {prodotto.Id}, Nome: {prodotto.NomeProdotto}, Prezzo:{prodotto.PrezzoProdotto}, Giacenza: {prodotto.GiacenzaProdotto}");

        }
        try
        {
            ProdottoAdvanced prodotto = new ProdottoAdvanced();
            prodotto.Id = 0;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        try 
        {
            ProdottoAdvanced prodotto = new ProdottoAdvanced();
            prodotto.NomeProdotto = "";       
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
        }

    }

    public class ProdottoAdvanced
    {
        // List<ProdottoAdvanced> prodotti = new List<ProdottoAdvanced>
        // {   //10.5m la m sta per decimal (tipo di dato) e indica che il valore è un decimale (numero con la virgola)
        //     new ProdottoAdvanced { id = 1, NomeProdotto = "Prodotto A" , PrezzoProdotto = 10.50m, GiacenzaProdotto = 100},
        //     new ProdottoAdvanced { id = 2, NomeProdotto= "Prodotto B" , PrezzoProdotto = 20.75, GiacenzaProdotto = 50}
        // };


        private int id;

        public int Id
        {
            get { return id; }
            // set { id = value; }

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
}