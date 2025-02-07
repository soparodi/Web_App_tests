
using Newtonsoft.Json;

public class CarrelloRepository
{
    private string folderPath = "Data/Carrello"; //crea per il file json
    public void SalvaCarrello(List<Prodotto> carrello)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var prodotto in carrello)
        {
            string filePath = Path.Combine(folderPath, $"{prodotto.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(prodotto, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            //Console.WriteLine($"Prodotto salvato in {filePath}: \n");
        }
    }

    public List<Prodotto> CaricaCarrello()
    {
        List<Prodotto> prodotti = new List<Prodotto>();
        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(file);
                Prodotto prodotto = JsonConvert.DeserializeObject<Prodotto>(readJsonData);
                prodotti.Add(prodotto);
            }
        }
        return prodotti;
    }
}