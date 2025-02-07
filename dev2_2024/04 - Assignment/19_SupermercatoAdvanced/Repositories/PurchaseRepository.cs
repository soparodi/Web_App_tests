
using Newtonsoft.Json;

public class PurchaseRepository
{

    private static readonly string folderPath = "Data/Purchase"; //crea per il file json
    public void SalvaPurchase(List<Purchase> purchases)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var purchase in purchases)
        {
            string filePath = Path.Combine(folderPath, $"{purchase.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(purchase, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            //Console.WriteLine($"Purchase salvato in {filePath}: \n");
        }
    }

    public List<Purchase> CaricaPurchase()
    {
        List<Purchase> purchases = new List<Purchase>();
        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(file);
                Purchase purchase = JsonConvert.DeserializeObject<Purchase>(readJsonData);
                purchases.Add(purchase);
            }
        }
        return purchases;
    } 
}