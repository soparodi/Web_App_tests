
using Newtonsoft.Json;

public class CategoriaRepository
{
    private string folderPath = "Data/Categoria"; //crea per il file json
    public void SalvaCategoria(List<Categoria> categoria)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var categorie in categoria)
        {
            string filePath = Path.Combine(folderPath, $"{categorie.Id}.json"); //percorso del file JSON
            string jsonData = JsonConvert.SerializeObject(categorie, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
            //Console.WriteLine($"Prodotto salvato in {filePath}: \n");
        }
    }

    public List<Categoria> CaricaCategoria()
    {
        List<Categoria> categorias = new List<Categoria>();
        if (Directory.Exists(folderPath))
        {
            
            foreach (var categoria in Directory.GetFiles(folderPath, "*.json"))
            {
                string readJsonData = File.ReadAllText(categoria);
                Categoria categorie = JsonConvert.DeserializeObject<Categoria>(readJsonData);
                categorias.Add(categorie);
            }
        }
        return categorias;
    }
}