# Inserimento dati

## Obiettivo
 Programma in c# che calcola statistiche sui punteggi inseriti dall'utente

 - L'utente deve poter inserire una serie di punteggi (numeri interi).
 - Il programma deve terminare l'inserimento dei punteggi quando l'utente inserisce un numero negativo.

 Dopo l'inserimento, il programma deve calcolare e mostrare:

 - Il numero totale di punteggi inseriti.
 - Il punteggio più alto.
 - Il punteggio più basso.
 - La media dei punteggi.

 Se l'utente inserisce solo un numero negativo, il programma deve stampare un messaggio di errore e terminare.


## Suggerimenti 
- Usa una lista per memorizzare i punteggi inseriti dall'utente
- Controlla come vengono gestiti gli input
- Usa i metodi math

## Versione 1
```csharp
int punteggio = 0;
List<int> listaPunteggi = new List<int>();
bool controllo = true;
try
{

    Console.WriteLine("Ciao, inserisci un punteggio: ");

    while (controllo)
    {
        punteggio = int.Parse(Console.ReadLine());

        if (punteggio >= 0)
        {
            listaPunteggi.Add(punteggio);

        }
        else
        {
            controllo=false;
        }
    }
}

catch (Exception e)
{
    Console.WriteLine($"Errore {e.Message}");
}

if (punteggio == 0)
{
    Console.WriteLine("Non hai inserito alcun punteggio.");
}

else

{
    int punteggioTotale = listaPunteggi.Count;
    int punteggioMax = listaPunteggi.Max();
    int punteggioMin = listaPunteggi.Min();
    double media = listaPunteggi.Average();

    Console.WriteLine($"Il punteggio maggiore è {punteggioMax} ");
    Console.WriteLine($"Il punteggio minimo è {punteggioMin}");
    Console.Write($"La media del punteggio è {media}");
    Console.WriteLine($"Hai inserito {punteggioTotale}");
}
```

