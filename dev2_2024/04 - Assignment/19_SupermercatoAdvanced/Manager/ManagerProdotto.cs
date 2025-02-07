
using Newtonsoft.Json;
public class ProdottoManager // gestiscono i CRUD 

{
    private int prossimoId;
    //lista di prodotti di tipo ProddottoAdvanced per 

    private List<Prodotto> prodotti;
    private ProdottoRepository repository; // prodotti e private perche non voglio che venga modificato dall'esterno


    public ProdottoManager(List<Prodotto> listaProdotti)
    {


        if (listaProdotti != null)
        {
            prodotti = listaProdotti;
        }
        else
        {
            prodotti = new List<Prodotto>();
        }
        repository = new ProdottoRepository();
        prossimoId = 1;
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Id >= prossimoId) // se l'ID del prodotto è maggiore o uguale al prossimoId
            {
                prossimoId = prodotto.Id + 1; // assegna al prossimoId il valore successivo
            }
        }

    }



// metodo per aggiungere un prodotto alla lista
public void AggiungiProdotto(Prodotto prodotto)
{ //assegna automaticamente un ID univoco
    prodotto.Id = prossimoId;
    //incrementa il prossimo ID per il prossim prodotto
    prossimoId++;
    prodotti.Add(prodotto);
    Console.WriteLine($"Prodotto aggiunto con ID: {prodotto.Id}");
}

// metodo per visualizzare la lista di prodotti
public List<Prodotto> OttieniProdotti()
{
    return prodotti;
}
//ogni campo utilizza il formato {campo,-largezza} dove:
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
            $"{prodotto.Id,-5} {prodotto.Nome,-20} {prodotto.Prezzo,-10} {prodotto.Giacenza,-10}"
        );
    }
}
public void StampaProdottiCliente()
{
    // Intestazioni con larghezza fissa
    Console.WriteLine(
        $" {"Nome",-20} {"Prezzo",-10} {"Categoria", -10}"
    );
    Console.WriteLine(new string('-', 50)); // Linea separatrice

    // Stampa ogni prodotto con larghezza fissa
    foreach (var prodotto in prodotti)
    {
        Console.WriteLine(
            $"{prodotto.Nome,-20} {prodotto.Prezzo,-10} {prodotto.Categoria,-10}"
        );
    }
}

// metodo per cercare un prodotto
public Prodotto TrovaProdotto(int id)
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

// metodo per modificare un prodotto esistente
public void AggiornaProdotto(int id, Prodotto nuovoProdotto)
{
    var prodotto = TrovaProdotto(id);
    if (prodotto != null)
    {
        prodotto.Nome = nuovoProdotto.Nome;
        prodotto.Prezzo = nuovoProdotto.Prezzo;
        prodotto.Giacenza = nuovoProdotto.Giacenza;
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
        string filePath = Path.Combine("Data/Prodotto", $"{id}.json");
        File.Delete(filePath);
        Console.WriteLine($"Prodotto eliminato: {filePath}");
    }
}

}



