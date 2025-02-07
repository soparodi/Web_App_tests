
using Newtonsoft.Json;

public class CarrelloManager
{
    private int prossimoId;
    private List<Prodotto> prodotti;
    private List<Prodotto> catalogo;
    private ProdottoRepository repositoryCatalogo;
    private CarrelloRepository repositoryCarrello;
    private ClienteRepository repositoryCliente;
    private List<Prodotto> listaCarrello;
    private int quantita;


    public CarrelloManager(List<Prodotto> listaProdotti)
    {
        prodotti = listaProdotti;
        repositoryCatalogo = new ProdottoRepository();
        repositoryCarrello = new CarrelloRepository();
        repositoryCliente = new ClienteRepository();

        prossimoId = 1;
        quantita = 0;


        if (listaProdotti != null)
        {
            prodotti = listaProdotti;
        }
        else
        {
            prodotti = new List<Prodotto>();
        }
        prossimoId = 1;
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Id >= prossimoId) // se l'ID del prodotto è maggiore o uguale al prossimoId
            {
                prossimoId = prodotto.Id + 1; // assegna al prossimoId il valore successivo
            }
        }

    }
    public void AggiungiProdotto(string prodottoCarrello, List<Prodotto> carrello, string Cliente)
    {
        catalogo = repositoryCatalogo.CaricaProdotti();

    }

    public void VisualizzaCarrello(List<Prodotto> listaCarrello)
    {
        Console.WriteLine(
       $"{"Nome",-20} {"Prezzo",-10} {"Quantita",-10}"
     );
        Console.WriteLine(new string('-', 50)); // Linea separatrice

        // Stampa ogni prodotto con larghezza fissa
        foreach (var prodotti in listaCarrello)
        {
            Console.WriteLine(
                $" {prodotti.Nome,-20} {prodotti.Prezzo,-10} {prodotti.Quantita}"
            );
        }
    }
    public decimal CalcoloTotale(Cliente clienteSingolo)
    {
        decimal calcoloTotale = 0;
        //per ogni prodotto nel carrello 
        //sommare il prezzo del prodotto a calcoloTotale
        foreach (var prodotto in clienteSingolo.Carrello)
        {
            if (prodotto.Quantita == 1)
            {
                //se i prodotti da sommare sono singoli svolgo l istruzione:
                calcoloTotale += prodotto.Prezzo;
            }
            else
            {
                //altrimenti significa che bisogna tenere conto anche della quantità:
                calcoloTotale += prodotto.Prezzo * prodotto.Quantita;
            }
        }
        return calcoloTotale;
    }
}
