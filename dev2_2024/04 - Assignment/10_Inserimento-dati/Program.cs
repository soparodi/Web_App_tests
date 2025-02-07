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

    Console.WriteLine($"Hai inserito {punteggioTotale}");
    Console.WriteLine($"Il punteggio maggiore è {punteggioMax} ");
    Console.WriteLine($"Il punteggio minimo è {punteggioMin}");
    Console.Write($"La media del punteggio è {media}");
   
}


/*int punteggio1 = int.Parse(Console.ReadLine());
Console.WriteLine("Inserisci il secondo punteggio: ");
int punteggio2 = int.Parse(Console.ReadLine()) ;
Console.WriteLine("Inserisci il terzo punteggio: ");
int punteggio3 = int.Parse(Console.ReadLine());
}

{
   Console.WriteLine($"Inserisci un valore numerico: ");
}
int punteggiInseriti= int.Parse(Console.ReadLine());
Console.WriteLine($"Hai inserito {punteggiInseriti} punteggi");*/
