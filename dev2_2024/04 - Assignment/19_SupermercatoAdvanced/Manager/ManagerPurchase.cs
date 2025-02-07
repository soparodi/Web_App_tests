
using Newtonsoft.Json;
public class ManagerPurchase // gestiscono i CRUD 

{
    private int prossimoId;
    //lista di prodotti di tipo ProddottoAdvanced per 
   

    public ManagerPurchase(List<Purchase> listaPurchase)
    {


    /*if (listaProdotti != null)
        {
            prodotti = listaProdotti;
        }
        else
        {
            prodotti = new List<Prodotto>();
        }*/
        //repository = new ProdottoRepository();
        prossimoId = 1;
        foreach (var purchase in listaPurchase)
        {
            if (purchase.Id >= prossimoId) // se l'ID del prodotto è maggiore o uguale al prossimoId
            {
                prossimoId = purchase.Id + 1; // assegna al prossimoId il valore successivo
            }
        }

    }



// metodo per aggiungere un prodotto alla lista
public void AggiungiPurchase(Purchase purchase)
{ //assegna automaticamente un ID univoco
    purchase.Id = prossimoId;
    //incrementa il prossimo ID per il prossim purchase
    prossimoId++;
    //purchase.Add(purchase);
    //Console.WriteLine($"Purchase aggiunto con ID: {purchase.Id}");
}

/*
/

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
*/
}



