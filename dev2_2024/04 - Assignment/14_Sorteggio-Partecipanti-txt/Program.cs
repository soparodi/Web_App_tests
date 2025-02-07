List <string> partecipanti = List<string>() ; //creo un array di stringhe con i nomi dei partecipanti

List<T> List<T>()
{
    throw new NotImplementedException();
}

string partecipante = @"Partecipanti.txt" ;
Random random = new Random(); //creo un oggetto Random per generare numeri casuali
int index = random.Next(partecipanti.Count); //genero un indice casuale
partecipante = partecipanti[index]; //estraggo il partecipante
Console.WriteLine(partecipante); //stampo il partecipante
while (true)
{
    // controllo se ci sono ancora partecipanti
    if (partecipanti.Count == 0)
    {
        Console.WriteLine("Non ci sono piu partecipanti da estrarre");
        break;
    }
    // estraggo un indice casuale
    index = random.Next(partecipanti.Count);

    // estraggo il partecipante
    partecipante = partecipanti[index];

    // stampo il partecipante
    Console.WriteLine(partecipante);
}

/*List<string> partecipati = new List<string>();

Random random= new Random();

StampaPartecipanti(partecipanti);

Console.WriteLine("Quanti partecipanti vuoi sorteggiare?");
int quantitaPartecipantiSorteggio = int.Parse(Console.ReadLine());

List <string>[] partecipantiSort = new List<string>[quantitaPartecipanti];








void StampaPartecipanti(List<string> partecipanti)
{
    Console.WriteLine("Partecipanti:");
    foreach (string partecipante in partecipanti)
    {
        Console.WriteLine(partecipante);
    }
}

int PartecipantiSorteggiati(List<string> partecipanti,Random random, List <string>[]partecipantiSort)
{
   int index = random.Next(partecipanti.Count);
}*/

/*if (partecipanti.Count==0)
{
    Console.WriteLine("Non c'è nessun partecipante");
}

Console.WriteLine("Quanti partecipanti vuoi sorteggiiare?");
int partecipantiDaSorteggiare;
while(!int.TryParse(Console.ReadLine(), out partecipantiDaSorteggiare))
{
    Console.WriteLine($"Inserisci un numero valido tra 1 e {partecipanti.Count}");
}*/