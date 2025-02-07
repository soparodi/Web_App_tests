## Verifica delle competenze

## Obiettivo

- Esercizio mirato alla verifica delle competenze riguardanti:

- Tipi di dati, operatori, cicli
- Strutture di dati 
- Funzioni

## Esercitazione

- Creare una rubrica con i campi:
- nome
- cognome
- indirizzo
- numero di telefono

- Creare una funzione che permetta di aggiungere un contatto alla rubrica
- Creare una funzione che permetta di visualizzare tutti i contatti della rubrica
- Creare una funzione che permetta di cercare un contatto per nome
- Creare una funzione che permetta di modificare un contatto per nome
- Creare una funzione che permetta di cancellare un contatto per nome

Il programma deve chiedere all'utente quale operazione vuole fare e, in base alla scelta, eseguire la funzione corrispondente.

I dati devono essere inseriti in una struttura di dati appropriata.

## Durata

- 2 ore

```csharp
 Dictionary<List<string>,int> rubrica = new Dictionary<List<string>, int>
{
    { new List<string> { "Anna","Pinto", "Via Leopardi" }, 3312195246 },
    { new List<string> {"Lisa","Giunta", "Via Dante" }, 3315346667 },
    { new List<string> { "Gianni","Conte", "Via Foscolo" }, 3263456789 },
    { new List<string> { "Paolo","Sartori","Via Macchiavelli" }, 3312294276 }
};
AggiungiContatto(rubrica);

MostraContatti(rubrica);

CercaContatto(rubrica);


 void AggiungiContatto(Dictionary <List<string>,int> rubrica)
{
Console.WriteLine("Inserisci il nome del contatto da aggiungere:");
string nome= Console.ReadLine();

Console.WriteLine("Inserisci il cognome del contatto da aggiungere:");
string cognome= Console.ReadLine();

Console.WriteLine("Inserisci l'indirizzo del contatto da aggiungere:");
string indirizzo= Console.ReadLine();

Console.WriteLine("Inserisci il numero telefonico del contatto da aggiungere:");
int numero;
 while (!int.TryParse(Console.ReadLine(), out numero))
 {
    Console.WriteLine("Inserisci un numero valido.");
 }

List<string> informazioniContatto = new List<string> { nome, cognome, indirizzo };

rubrica.Add(informazioniContatto, numero);
}

void MostraContatti(Dictionary<List<string>,int>rubrica)
{
   foreach (var contatto in rubrica)
   {
    Console.WriteLine($"Nome:{contatto.Key[0]}");
    Console.WriteLine($"Cognome:{contatto.Key[1]}");
    Console.WriteLine($"Indirizzo:{contatto.Key[2]}");
    Console.WriteLine($"Numero telefonico: {contatto.Value}");
   }
}
void CercaContatto(Dictionary<List<string>,int>rubrica)
{
    Console.WriteLine("Cerca un contatto specifico, scrivi il nome:");
    string cercoContatto= Console.ReadLine();
    if (rubrica.ContainsKey(cercoContatto))
    {
        Console.WriteLine($"{cercoContatto} è presente in rubrica");
    }
}
```
