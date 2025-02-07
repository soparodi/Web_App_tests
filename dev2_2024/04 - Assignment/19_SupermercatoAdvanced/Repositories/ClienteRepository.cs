
using Newtonsoft.Json;

public class ClienteRepository
{

    private string folderPath = "Data/Cliente"; //crea per il file json
    public void SalvaClienti(List<Cliente> clienti)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var cliente in clienti)
        {
            string filePath = Path.Combine(folderPath, $"{cliente.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(cliente, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            //Console.WriteLine($"Categoria salvato in {filePath}: \n");
        }
    }
    public void SalvaClienteSingolo(Cliente cliente)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }


        string filePath = Path.Combine(folderPath, $"{cliente.Id}.json"); //percorso del file JSON
        string jsonData = JsonConvert.SerializeObject(cliente, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
        //Console.WriteLine($"Prodotto salvato in {filePath}: \n");
    }

    public List<Cliente> CaricaClienti()
    {

        List<Cliente> clienti = new List<Cliente>();
        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(file);
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(readJsonData);
                clienti.Add(cliente);
            }
        }
        return clienti;
    }
    public Cliente CaricaClienteSingolo()//questo metodo carica un cliente singolo
    {
        if (Directory.Exists(folderPath))//legge tutto quello che c'è nel file del singolo cliente
        {
            string readJsonData = File.ReadAllText("Data/Cliente/1.json");
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(readJsonData);

            return cliente;// se la condizione è vera e lo trova
        }
        return null;//se non lo trova, ritorna l'avviso che non l'ha trovato
    }
}