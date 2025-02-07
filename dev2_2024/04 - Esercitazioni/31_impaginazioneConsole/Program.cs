using System.Globalization;

var prodotti = new List<Prodotto> //noi non lista ma utilizziamo il repository
{
    new Prodotto { Id = 1, Nome = "Prodotto1", Prezzo = 1.5m },
    new Prodotto { Id = 2, Nome = "Prodotto2", Prezzo = 2.0m },
    new Prodotto { Id = 3, Nome = "Prodotto3", Prezzo = 3.0m },
    new Prodotto { Id = 4, Nome = "Prodotto4", Prezzo = 4.0m },
    new Prodotto { Id = 5, Nome = "Prodotto5", Prezzo = 5.0m },
    new Prodotto { Id = 6, Nome = "Prodotto6", Prezzo = 6.0m },
    new Prodotto { Id = 7, Nome = "Prodotto7", Prezzo = 7.0m },
    new Prodotto { Id = 8, Nome = "Prodotto8", Prezzo = 8.0m },
    new Prodotto { Id = 9, Nome = "Prodotto9", Prezzo = 9.0m },
    new Prodotto { Id = 10, Nome = "Prodotto10", Prezzo = 10.0m }
};

int articoliPerPagina = 3; // numero articoli per pagina

MostraProdottiPaginati(prodotti,articoliPerPagina);

static void MostraProdottiPaginati(List<Prodotto> prodotti, int articoliPerPagina)
{
    int totaleProdotti = prodotti.Count;
    // calcolo del totatle delle pagine con tottale prodotti diviso gli articoli in igni pagina
    int totalePagine = (int)Math.Ceiling((decimal)totaleProdotti / articoliPerPagina);
    //definisco la pagina corrente
    int paginaCorrente = 1; //pagina iniziale

    while (true)
    {
        Console.Clear();
        Console.WriteLine ($"Pagina {paginaCorrente} di {totalePagine}");
        Console.WriteLine (new string ('-', 38)); // linea divisoria
        //var prodottiPagina = prodotti.Skip ((paginaCorrente -1) * articoliPerPagina).Take (articoliPerPagina); // soluzione in un'unica riga
        
        var prodottiPagina = prodotti // lista di prodotti in modo da poter fare la paginazione
        // paginazione
        // skip salta i primi n elementi
        // take prendi i primi n elementi (in questo caso 3)
        .Skip((paginaCorrente -1) * articoliPerPagina) //salta i prodotti delle pagine precedenti
        .Take(articoliPerPagina);

        foreach (var prodotto in prodottiPagina)
        {
            Console.WriteLine ($"ID: {prodotto.Id} - Nome: {prodotto.Nome} - Prezzo: {prodotto.Prezzo}");
        }

        Console.WriteLine (new string ('-', 38)); // linea divisoria
        // stampo i pulsanti di navigazone 
        Console.WriteLine ("Navigazione: [N] Prossima pagina | [P] Pagina precedente | [E] Esci"); // ("Navigazione: [<] pagina precedente | [>] Prossima pagina |  | [E] Esci")
        // uso il readkey in modo da non dover premere invio
        var input = Console.ReadKey(true).Key;
        if ( input == ConsoleKey.RightArrow && paginaCorrente < totalePagine) // RightArrow o N per andare avanti
        // if ( input == ConsoleKey.N && paginaCorrente < totalePagine)
        {
            paginaCorrente++; //va avanti avendo come limite il totale delle pagine
        }
        else if (input == ConsoleKey.LeftArrow && paginaCorrente > 1) // LeftArrow o P per andare indietro
        // else if (input == ConsoleKey.P && paginaCorrente > 1)
        {
            paginaCorrente--; //va indietro avendo come limite la prima pagina 
        }
        else if (input == ConsoleKey.Escape) // escape o E per uscire
        // else if (input == ConsoleKey.E) // escape o E per uscire
        {
            break;
        }
    }
}

    
public class Prodotto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Prezzo { get; set; }
}