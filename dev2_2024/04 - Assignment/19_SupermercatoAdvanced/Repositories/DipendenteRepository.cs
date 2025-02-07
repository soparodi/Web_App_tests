
using Newtonsoft.Json;

public class DipendenteRepository
{

    private string folderPath = "Data/Dipendente"; //crea per il file json
    public void SalvaDipendente(List<Dipendente> dipendenti)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var dipendente in dipendenti)
        {
            string filePath = Path.Combine(folderPath, $"{dipendente.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(dipendente, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            //Console.WriteLine($"Dipendente salvato in {filePath}: \n");
        }
    }

    public List<Dipendente> CaricaDipendenti()
    {

        List<Dipendente> dipendenti = new List<Dipendente>();
        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(file);
                Dipendente dipendente = JsonConvert.DeserializeObject<Dipendente>(readJsonData);
                dipendenti.Add(dipendente);
            }
        }
        return dipendenti;

    }
  
}