
using Newtonsoft.Json;

public class ProdottoRepository
{

    private static readonly string folderPath = "Data/Prodotto"; //crea per il file json
    public void SalvaProdotti(List<Prodotto> prodotti)
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
            //Console.WriteLine($"Prodotto salvato in {filePath}: \n");
        }
    }

    public List<Prodotto> CaricaProdotti()
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