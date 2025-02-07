# SORTEGGIO PARTECIPANTI

## Versione 1

## Obiettivo

- Scrivere un programma che permette di sorteggiare i partecipanti del corso da una lista di nomi. 

- I nomi vengono gestiti senza un inserimento da parte dell'utente, cioè inizializzati nel programma.

- Il programma estrae un partecipante singolo alla volta e lo stampa a video.

```csharp


List<string>listaPartecipanti= new List<string>
{
    "Andrea", "Anita", "Diego", "Felipe", "Giorgio", "Ivan", "Sofia", "Tamer"
    };
    foreach (var nome in listaPartecipanti)
    {
        Console.WriteLine(nome);
    }
 Random random = new Random();

 int nomeCasuale= random.Next(listaPartecipanti.Count);
  Console.WriteLine("E' stato sorteggiato " + listaPartecipanti[nomeCasuale] );
```


## Comandi di versionamento

```bash

git add --all
git commit -m "versione12"
git push -u origin main

```

## Versione 2

## Obiettivo

- Scrivere un programma che permette di sorteggiare più volte i partecipanti del corso da una lista di nomi.

- Il programma deve chiedere all'utente se vuole estrarre un altro partecipante.

- I nomi dei partecipanti estratti vengono rimossi dalla lista.


```csharp

Random random = new Random();//metodo random per sorteggiare i nomi casualmente
//creo la lista di nomi dei partecipanti
List<string> listaPartecipanti = new List<string> { "Andrea", "Anita", "Diego", "Felipe", "Giorgio", "Ivan", "Sofia", "Tamer" };
int nomeCasuale; // mi sono creata un indice
string nomeEstratto; //aggiungo una variabile stringa
int conteggioP = listaPartecipanti.Count;
string risposta;


do
{
    nomeCasuale = random.Next(conteggioP);

    foreach (var nome in listaPartecipanti) // utilizzo il foreach per stamparli tutti
    {
        Console.WriteLine(nome);
    }

    nomeEstratto = listaPartecipanti[nomeCasuale];

    Console.WriteLine($"E' stato sorteggiato {nomeEstratto}"); // ho inserito l'indice nel Console.WriteLine

    listaPartecipanti.RemoveAt(nomeCasuale);

    conteggioP--;

    if (conteggioP == 0)
    {
        Console.WriteLine("Non ci sono più nomi da estrarre");
        break;
    }


    Console.WriteLine("Vuoi estrarre un altro nome? s/n");
    risposta= Console.ReadLine().ToLower();

      while (risposta != "s" && risposta != "n")
    {
        Console.WriteLine("Risposta non valida. Vuoi estrarre di nuovo? (s/n)");
        risposta = Console.ReadLine().ToLower();
        Console.Clear();
    }
}
while (risposta =="s");

```

## Comandi di versionamento

```bash

git add --all
git commit -m "versione 2"
git push -u origin main

```

## Versione 3

## Obiettivo

- Scrivere un programma che permette di sorteggiare i partecipanti del corso da una lista di nomi dividendoli in gruppi.

- Il programma deve chiedere all'utente il numero di squadre

- Se il numero dei partecipanti non è divisibile per il numero di squadre , i partecipanti rimanenti vengono assegnati ad un gruppo in modo casuale.


```csharp

Random random = new Random();//metodo random per sorteggiare i nomi casualmente
//creo la lista di nomi dei partecipanti
List<string> listaPartecipanti = new List<string> { "Andrea", "Anita", "Diego", "Felipe", "Giorgio", "Ivan", "Sofia", "Tamer", "Giacomo" };
int conteggioP = listaPartecipanti.Count; // ho assegnato alla variabile conteggioP il numero dei partecipanti che sono all'interno della lista
string risposta;
int numeroSquadre;//dichiaro una variabile di tipo int che conterrà il numero di squadre che l'utente desidera formare 
int pPSquadra;//dichiaro una variabile di tipo int che rappresenta il numero di partecipanti per ciascuna squadra
int partecipantiRimanenti;// variabile usata per calcolare quanti partecipanti rimangono se il numero di partecipanti non è divisibile per il numero di squadre


do // creo il ciclo do while per far si che l'utente possa ripetere l'operazione 
{
    Console.WriteLine("Quante squadre desideri formare?");
    numeroSquadre = int.Parse(Console.ReadLine()); // converto la risposta in string dell'utente in int con l'utilizzo di Parse perchè se l'utente inserisce un testo che non è un numero, il metodo genererà un'eccezione 

    Console.WriteLine("Quanti partecipanti per squadra devono esserci?");
    pPSquadra = int.Parse(Console.ReadLine());
    partecipantiRimanenti = conteggioP % numeroSquadre;//calcolo quanti partecipanti rimarranno dopo essere stati assegnati tra le squadre utilizzando il modulo % quindi il resto.
    int pPSquadre;
    pPSquadre = conteggioP / numeroSquadre; // divido ilnumero dei partecipanti per il numero di squadre

    // Aggiungo una lista vuota per ogni squadra
    List<List<string>> listaSquadre = new List<List<string>>();

    //USO FOR PERCHE'IL NUMERO DI VOLTE IN CUI DEVE RIPETERSI IL CICLO NON E' INFINITO MA DEFINITO DALL'UTENTE
    for (int i = 0; i < numeroSquadre; i++) // itero nell ' indice del numeroSquadre. quando il ciclo for inizia, i vale 0. Ogni volta che il ciclo ripete il codice all'interno del blocco, il valore di i aumenta di 1.
    {
        listaSquadre.Add(new List<string>());
    }
    for (int i = 0; i < listaPartecipanti.Count; i++)
    {
        int indiceCasuale = random.Next(i, listaPartecipanti.Count); // Ottengo un indice casuale
        string nomeCasuale = listaPartecipanti[indiceCasuale];//memorizzo un elemento casuale della lista listaPartecipanti in una variabile nomeCasuale. Accedo all'elemento della lista che si trova nella posizione indiceCasuale, viene copiato nella variabile nomeCasuale
        listaPartecipanti[i] = nomeCasuale; //nomeCasuale ora contiene un valore che corrisponde al nome di uno dei partecipanti scelti casualmente dalla lista
        listaPartecipanti[indiceCasuale] = listaPartecipanti[i]; //sostituisco l'elemento della lista che si trova nella posizione indiceCasuale con l'elemento che si trova nella posizione i della lista.

        /*IN PRATICA Salvo il nome che si trova nella posizione indiceCasuale in una variabile temporanea (nomeCasuale).
        Sposto il nome che si trova alla posizione i nella posizione indiceCasuale e metto il nome che avevo salvato temporaneamente (nomeCasuale) nella posizione i. Quindi è uno scambio */


    }

    // Distribuisco i partecipanti nelle squadre.
    int partecipanteIndex = 0;  /*uso index re ssegnare correttamente i partecipanti dalla lista listaPartecipanti a ciascuna squadra.
                                 l'indice è una posizione numerica che rappresenta   la posizione dell' elemento all'interno della lista. 
                                 ogni elemento ha una posizione che parte da 0.*/
    for (int i = 0; i < numeroSquadre; i++) // primo ciclo for, che itera sul numero di squadre da creare
    {
        for (int j = 0; j < pPSquadre; j++) // j è il numero di partecipanti che assegno a ciascuna squadra.
        {
            if (partecipanteIndex < listaPartecipanti.Count) // verifico che l'indice partecipanteIndex non superi il numero totale di partecipanti disponibili nella lista    (listaPartecipanti.Count).

            {
                listaSquadre[i].Add(listaPartecipanti[partecipanteIndex]);//aggiungendo il partecipante corrente (indicato da partecipanteIndex) alla squadra i nella lista listaSquadre. Sto assegnando un partecipante alla squadra corrente
                partecipanteIndex++;// Ogni volta che un partecipante viene assegnato a una squadra, partecipanteIndex aumenta, così da non assegnare lo stesso partecipante più di una volta. Sto incrementando l'indice di 1
            }
        }
    }

    // Gestire i partecipanti rimanenti (se ce ne sono)
    if (partecipantiRimanenti > 0) // verifico se ci sono partecipanti rimanenti da distribuire nelle squadre confrontando la variabile partecipantiRimanenti (che è il numero di partecipanti che non sono riusciti ad essere distribuiti equamente tra le squadre) con 0.
    {
        // Distribuire i partecipanti rimanenti nelle squadre in modo casuale
        for (int i = 0; i < partecipantiRimanenti; i++) // inizio un ciclo che verrà eseguito tante volte quanto è il numero di partecipanti rimanenti.
        {
            listaSquadre[i].Add(listaPartecipanti[partecipanteIndex]); // ggiungi il partecipante rimanente alla squadra i (dove i è l'indice della squadra), utilizzando l'indice casuale partecipanteIndex.

            partecipanteIndex++;
        }
    }

    // Mostra i gruppi creati
    for (int i = 0; i < listaSquadre.Count; i++)
    {
        Console.WriteLine($"Squadra {i + 1}:"); //Stampo il nome della squadra. Poiché i inizia da 0, la numerazione della squadra parte da 1 (usiamo i + 1 per visualizzare "Squadra 1" invece di "Squadra 0").
        foreach (var nome in listaSquadre[i])/*il ciclo foreach itera su tutti i partecipanti all'interno della squadra i 
        (ogni squadra è una lista di stringhe, quindi ogni elemento di listaSquadre[i] è il nome di un partecipante)*/
        {
            Console.WriteLine(nome);//Stampo il nome di ogni partecipante della squadra corrente.
        }
    }

    // Chiedo all'utente se vuole riprovare

    Console.WriteLine("\nVuoi riprovare? s/n");
    risposta = Console.ReadLine().ToLower();

    while (risposta != "s" && risposta != "n")
    {
        Console.WriteLine("Risposta non valida. Vuoi provare a formare nuove squadre? (s/n)");
        risposta = Console.ReadLine().ToLower();
        Console.Clear();
    }

} while (risposta == "s");

```

## Comandi di versionamento

```bash
git add --all
git commit -m "versione 3"
git push -u origin main

```


## Versione 3.1

```csharp

// creo la lista dei partecipanti
List<string> partecipanti = new List<string> { "Partecipante 1", "Partecipante 2", "Partecipante 3", "Partecipante 4", "Partecipante 5", "Partecipante 6", "Partecipante 7", "Partecipante 8", "Partecipante 9", "Partecipante 10" };

// creo un oggetto Random per generare numeri casuali
Random random = new Random();

// chiedo all'utente il numero di squadre
Console.WriteLine("Inserisci il numero di squadre:");
int numeroSquadre = int.Parse(Console.ReadLine());

// creo un array di liste di stringhe per le squadre
List<string>[] squadre = new List<string>[numeroSquadre];

// per ogni squadra creo una lista vuota
for (int i = 0; i < numeroSquadre; i++)
{
    squadre[i] = new List<string>();
}

// calcolo quanti partecipanti ci sono in ogni squadra
int partecipantiPerSquadra = partecipanti.Count / numeroSquadre;

// se il numero di partecipanti non è divisibile per il numero di squadre, aggiungo un partecipante in piu ad una squadra

// calcolo quanti partecipanti ci sono in piu
int partecipantiInPiu = partecipanti.Count % numeroSquadre;

// per ogni squadra
for (int i = 0; i < numeroSquadre; i++)
{
    // per ogni partecipante della squadra

    for (int j = 0; j < partecipantiPerSquadra; j++)
    {
        // genero un numero casuale tra 0 e il numero di partecipanti rimasti
        int index = random.Next(partecipanti.Count);
        // aggiungo il partecipante alla squadra
        squadre[i].Add(partecipanti[index]);
        // rimuovo il partecipante dalla lista dei partecipanti
        partecipanti.RemoveAt(index);
    }

    // se ci sono partecipanti in piu, aggiungo un partecipante in piu alla squadra corrente
    if (partecipantiInPiu > 0)
    {
        // genero un numero casuale tra 0 e il numero di partecipanti rimasti
        int index = random.Next(partecipanti.Count);
        // aggiungo il partecipante alla squadra
        squadre[i].Add(partecipanti[index]);
        // rimuovo il partecipante dalla lista dei partecipanti
        partecipanti.RemoveAt(index);
        // decremento il numero di partecipanti in piu
        partecipantiInPiu--;
    }

    // stampo i partecipanti della squadra
    Console.WriteLine($"Squadra {i + 1}:");
    foreach (string partecipante in squadre[i])
    {
        Console.WriteLine(partecipante);
    }
    Console.WriteLine();
}
```

## Comandi di versionamento

```bash

git add --all
git commit -m "versione 3.1 per me"
git push -u origin main

```
## Versione 4

## Obiettivo

- Scrivere un programma che permetta di sorteggiare i partecipanti del corso da una lista di nomi dividendo i partecipanti in gruppi.
- Il programma deve usare un dizionario che ha come chiavi i numeri delle squadre e come valori le liste dei partecipanti di ogni squadra.


```csharp

// creo la lista dei partecipanti
List<string> partecipanti = new List<string> { "P1", "P2", "P3", "P4", "P5", "P6", "P7", "P8", "P9", "P10" };

// creo un oggetto Random per generare numeri casuali
Random random = new Random();

// chiedo all'utente il numero di squadre
Console.WriteLine("Inserisci il numero di squadre:");
int numeroSquadre = int.Parse(Console.ReadLine());

// creo un dizionario per le squadre
Dictionary<int, List<string>> squadre = new Dictionary<int, List<string>>();

// per ogni squadra
for (int i = 0; i < numeroSquadre; i++)
{
    // aggiungo la squadra al dizionario
    squadre.Add(i + 1, new List<string>());
}

// calcolo quanti partecipanti ci sono in ogni squadra
int partecipantiPerSquadra = partecipanti.Count / numeroSquadre;

// se il numero di partecipanti non è divisibile per il numero di squadre, aggiungo un partecipante in piu ad una squadra
int partecipantiInPiu = partecipanti.Count % numeroSquadre;

// per ogni squadra
for (int i = 0; i < numeroSquadre; i++)
{
    // aggiungo i partecipanti
    for (int j = 0; j < partecipantiPerSquadra; j++)
    {
        // genero un numero casuale tra 0 e il numero di partecipanti rimasti
        int index = random.Next(partecipanti.Count);
        // aggiungo il partecipante alla squadra
        squadre[i + 1].Add(partecipanti[index]);
        // rimuovo il partecipante dalla lista dei partecipanti
        partecipanti.RemoveAt(index);
    }

    // se ci sono partecipanti in piu, aggiungo un partecipante in piu alla squadra corrente
    if (partecipantiInPiu > 0)
    {
        // genero un numero casuale tra 0 e il numero di partecipanti rimasti
        int index = random.Next(partecipanti.Count);
        // aggiungo il partecipante alla squadra
        squadre[i + 1].Add(partecipanti[index]);
        // rimuovo il partecipante dalla lista dei partecipanti
        partecipanti.RemoveAt(index);
        // decremento il numero di partecipanti in piu
        partecipantiInPiu--;
    }

    // stampo i partecipanti della squadra
    Console.WriteLine($"Squadra {i + 1}:");
    foreach (string partecipante in squadre[i + 1])
    {
        Console.WriteLine(partecipante);
    }
    Console.WriteLine();
}

```

## Comandi di versionamento


```bash
git add --all
git commit -m "versione 4"
git push -u origin main

```

## Versione 5

## Obiettivo

- Scrivere un programma che permetta di sorteggiare i partecipanti del corso da una lista di nomi dividendo i partecipanti in gruppi.
- Il programma deve stampare la lista dei partecipanti
- Il programma deve chiedere all utente di inserire o eliminare un partecipante presente nella lista iniziale o fare il sorteggio

```csharp
// creo la lista dei partecipanti
List<string> partecipanti = new List<string> { "Partecipante 1", "Partecipante 2", "Partecipante 3", "Partecipante 4", "Partecipante 5", "Partecipante 6", "Partecipante 7", "Partecipante 8", "Partecipante 9", "Partecipante 10" };

// creo un oggetto Random per generare numeri casuali
Random random = new Random();

// pulisco la console
Console.Clear();

// stampo la lista dei partecipanti
Console.WriteLine("Partecipanti:");
foreach (string partecipante in partecipanti)
{
    Console.WriteLine(partecipante);
}

// chiedo all utente se vuole inserire o eliminare un partecipante o sorteggiare i partecipanti
while (true)
{
    Console.WriteLine("Vuoi inserire un partecipante, eliminare un partecipante o sorteggiare i partecipanti? (i/e/s)");
    string risposta = Console.ReadLine();
    // pulisco la console
    Console.Clear();
    if (risposta == "i")
    {
        Console.WriteLine("Inserisci il nome del partecipante:");
        string partecipante = Console.ReadLine();
        partecipanti.Add(partecipante);
    }
    else if (risposta == "e")
    {
        Console.WriteLine("Inserisci il nome del partecipante:");
        string partecipante = Console.ReadLine();
        partecipanti.Remove(partecipante);
    }
    else if (risposta == "s")
    {
        // chiedo all'utente il numero di squadre
        Console.WriteLine("Inserisci il numero di squadre:");
        int numeroSquadre = int.Parse(Console.ReadLine());

        // creo una lista per ogni squadra
        List<string>[] squadre = new List<string>[numeroSquadre];
        for (int i = 0; i < numeroSquadre; i++)
        {
            squadre[i] = new List<string>();
        }

        // calcolo quanti partecipanti ci sono in ogni squadra
        int partecipantiPerSquadra = partecipanti.Count / numeroSquadre;

        // se il numero di partecipanti non è divisibile per il numero di squadre, aggiungo un partecipante in più ad una squadra
        int partecipantiInPiù = partecipanti.Count % numeroSquadre;

        // per ogni squadra
        for (int i = 0; i < numeroSquadre; i++)
        {
            // aggiungo i partecipanti
            for (int j = 0; j < partecipantiPerSquadra; j++)
            {
                // genero un numero casuale tra 0 e il numero di partecipanti rimasti
                int index = random.Next(partecipanti.Count);
                // aggiungo il partecipante alla squadra
                squadre[i].Add(partecipanti[index]);
                // rimuovo il partecipante dalla lista dei partecipanti
                partecipanti.RemoveAt(index);
            }

            // se ci sono partecipanti in più, aggiungo un partecipante in più alla squadra corrente
            if (partecipantiInPiù > 0)
            {
                // genero un numero casuale tra 0 e il numero di partecipanti rimasti
                int index = random.Next(partecipanti.Count);
                // aggiungo il partecipante alla squadra
                squadre[i].Add(partecipanti[index]);
                // rimuovo il partecipante dalla lista dei partecipanti
                partecipanti.RemoveAt(index);
                // decremento il numero di partecipanti in più
                partecipantiInPiù--;
            }

            // stampo i partecipanti della squadra
            Console.WriteLine($"Squadra {i + 1}:");
            foreach (string partecipante in squadre[i])
            {
                Console.WriteLine(partecipante);
            }
            Console.WriteLine();
        }
        break;
    }
    // stampo la lista dei partecipanti
    Console.WriteLine("Partecipanti:");
    foreach (string partecipante in partecipanti)
    {
        Console.WriteLine(partecipante);
    }
}
```

## Comandi di versionamento

```bash
git add --all
git commit -m "Sorteggia Partecipanti Versione 5"
git push -u origin main

```