
using Newtonsoft.Json;
public class CategoriaManager // gestiscono i CRUD 

{
    private int prossimoId;

    private List<Categoria> categoria1;
    private int Id;


    public CategoriaManager(List<Categoria> categoria)
    {

        categoria1 = categoria;
        prossimoId = 1;

        if (categoria != null)
        {
            categoria1 = categoria;
        }
        else
        {
            categoria1 = new List<Categoria>();
        }
        foreach (var categorias in categoria1)
        {
            if (categorias.Id >= prossimoId) // se l'ID della categoria è maggiore o uguale al prossimoId
            {
                prossimoId = categorias.Id + 1; // assegna al prossimoId il valore successivo
            }
        }
    }



    // metodo per aggiungere una categoria alla lista
    public void AggiungiCategoria( Categoria category)
    { //assegna automaticamente un ID univoco
        category.Id = prossimoId;
        //incrementa il prossimo ID per il prossim prodotto
        prossimoId++;
        categoria1.Add(category);

    }

    // metodo per visualizzare la lista di prodotti
    public List<Categoria> OttieniCategoria()
    {
        return categoria1;
    }
    //ogni campo utilizza il formato {campo,-largezza} dove:
    //campo è il valore da stampare
    //-larghezza specifica la larghezza del campo; il il segno - allinea il testo a sinistra.
    //{"Nome". -20}significa che il nome del prodotto avrà una largezza fissa di 20 caratteri, allineato a sinisitra
    //Formato dei numeri:
    // Per i prezzi, viene usato il formato 0.00 per mostrare sempre due cifre decimali
    // Linea Console.WriteLine(new string ('-', 50)); stampa una linea divisoria lung 50 caratteri per migliorare la leggibilità

    /*public void StampaProdottiIncolonnati()
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
                $"{prodotto.Id,-5} {prodotto.Nome,-20} {prodotto.Prezzo,-10} {prodotto.Giacenza,-10}"
            );
        }
    }*/
    /*public void StampaCategoria()
    {
        // Intestazioni con larghezza fissa
        Console.WriteLine(
            $" {"Nome",-20} {"Prezzo",-10} {"Categoria", -10}"
        );
        Console.WriteLine(new string('-', 50)); // Linea separatrice

        // Stampa ogni prodotto con larghezza fissa
        foreach (var categorias in categoria1)
        {
            Console.WriteLine(
                $"{prodotto.Nome,-20} {prodotto.Prezzo,-10} {prodotto.Categoria,-10}"
            );
        }
    }*/

    // metodo per cercare una categoria
    public Categoria TrovaCategoria(int id)
    {
        foreach (var categorias in categoria1)
        {
            if (categorias.Id == id)
            {
                return categorias;
            }
        }
        return null;
    }

    // metodo per modificare una categoria esistente
    public void AggiornaCategoria(int id, Categoria nuovaCategoria)
    {
        var categoria = TrovaCategoria(id);
        if (categoria != null)
        {
            categoria.NomeCategoria = nuovaCategoria.NomeCategoria; // il nuovo nome della categoria prende il posto del vecchio nome
        }
    }


    // metodo per eliminare un prodotto
    public void EliminaCategoria(int id)
    {
        var categoria = TrovaCategoria(id);
        if (categoria != null)
        {
            categoria1.Remove(categoria);
            //elimina il file json corrispondente alla categoria
            string filePath = Path.Combine("Data/Categoria", $"{id}.json");
            File.Delete(filePath);
            Console.WriteLine($"Categoria eliminata: {filePath}");
        }
    }
}



