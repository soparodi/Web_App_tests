﻿// I TIPI DI DATI SEMPLICI

// variabili di tipo intero
int eta = 10; // dichiarazione e inizializzazione di una variabile di tipo intero

int eta2; // dichiarazione di una variabile di tipo intero
eta2 = 20; // inizializzazione di una variabile di tipo intero

// variabili di tipo decimale
double altezza = 1.70; // dichiarazione e inizializzazione di una variabile di tipo decimale

// variabili di tipo carattere
char iniziale = 'M'; // dichiarazione e inizializzazione di una variabile di tipo carattere

// variabili di tipo stringa
string nome = "Nome1"; // dichiarazione e inizializzazione di una variabile di tipo stringa

// variabili di tipo booleano
bool maggiorenne = true; // dichiarazione e inizializzazione di una variabile di tipo booleano

// variabile var
// var e una parola chiave che permette di dichiarare una variabile senza specificare il tipo
// il tipo viene dedotto dal valore assegnato
// var pero necessita di essere inizializzata al momento della dichiarazione
var cognome = "Rossi"; // dichiarazione e inizializzazione di una variabile var

// variabili di tipo dynamic
// dynamic e una parola chiave che permette di dichiarare una variabile il cui tipo viene determinato a runtime
// il tipo di una variabile dynamic puo cambiare durante l esecuzione del programma
dynamic altezza2 = 1.70; // dichiarazione e inizializzazione di una variabile di tipo dynamic

// quindi la differenza tra var e dynamic e che
// var determina il tipo a compile time (la variabile non puo cambiare tipo e deve essere inizializzata al momento della dichiarazione)
// mentre dynamic determina il tipo a runtime (la variabile puo cambiare tipo e puo non essere inizializzata al momento della dichiarazione)

// variabili di tipo data
DateTime dataNascita = new DateTime(2000, 1, 1); // dichiarazione e inizializzazione di una variabile di tipo data

// esempio di utilizzo di una variabile attraverso i metodi di console
// utilizzando l interpolazione di stringhe
Console.WriteLine($"La variabile eta contiene il valore {eta}"); // output: La variabile eta contiene il valore 10
Console.WriteLine($"La variabile altezza contiene il valore {altezza}"); // output: La variabile altezza contiene il valore 1.7

// ricevo l input dall utente e lo salvo in una variabile
Console.WriteLine("Inserisci il tuo nome: ");
string nomeUtente = Console.ReadLine();
Console.WriteLine($"Ciao {nomeUtente}!"); // output: Ciao Nome1!

// I TIPI DI DATI COMPLESSI (o strutture di dati)
// un insieme di dati dello stesso tipo

// ARRAY
int[] numeri = new int[5]; // dichiarazione e inizializzazione di un array di interi di 5 elementi
                            // new e una parola chiave che serve per creare un nuovo oggetto (costruttore)
// inserimento di valori nell array
numeri[0] = 10; // inserisco il numero 10 nella prima posizione dell array
numeri[1] = 20;
numeri[2] = 30;
numeri[3] = 40;
numeri[4] = 50;

// inserimento di valori multipli nell array
int[] numeri2 = new int[] { 10, 20, 30, 40, 50 }; // dichiarazione e inizializzazione di un array di interi di 5 elementi

// la caratteristica principale degli array e che sono di dimensione fissa

// LISTA
List<int> numeri3 = new List<int>(); // dichiarazione di una lista di interi
                                        // le liste sono di dimensione variabile

// inserimento di valori nella lista
numeri3.Add(10); // inserico il valore nella lista usando il metodo Add
numeri3.Add(20);
numeri3.Add(30);
numeri3.Add(40);
numeri3.Add(50);

// inserimento di valori multipli nella lista
List<int> numeri4 = new List<int> { 10, 20, 30, 40, 50 }; // dichiarazione e inizializzazione di una lista di interi

// sia le liste che gli array sono collezioni di dati che possono essere di Int, Double, Char, String, ecc.
// esempio di lista di stringhe
List<string> nomi = new List<string> { "Nome1", "Nome2", "Nome3" }; // dichiarazione e inizializzazione di una lista di stringhe

// DIZIONARIO
Dictionary<string, int> voti = new Dictionary<string, int>(); // dichiarazione di un dizionario con chiave di tipo stringa e valore di tipo intero
// in questo caso string e la chiave e int il valore

// BEST PRACTICES PER LA DICHIARAZIONE DI VARIABILI
// dichiarare le variabili con nomi significativi
// dichiarare le variabili con la notazione CamelCase o PascalCase
// esempio camel case etaStudente
// esempio pascal case EtaStudente

// METODI DI STRINGA
// i tipi di dato stringa hanno dei metodi che permettono di eseguire delle operazioni su di essi o di ottenere informazioni su di essi

// lenght
// prende la lunghezza di una stringa
string nome2 = "Nome1";
Console.WriteLine(nome2.Length); // output: 5

// isnullorwhitespace
// verifica se una stringa e nulla o vuota
string nome3 = "Nome1";
Console.WriteLine(string.IsNullOrWhiteSpace(nome3)); // output: False

// tolower
// converte una stringa in minuscolo
string nome4 = "Nome1";
Console.WriteLine(nome4.ToLower()); // output: nome1

// toupper
// converte una stringa in maiuscolo
string nome5 = "Nome1";
Console.WriteLine(nome5.ToUpper()); // output: NOME1

// trim
// rimuove gli spazi bianchi all inizio e alla fine di una stringa
string nome6 = " Nome1 ";
Console.WriteLine(nome6.Trim()); // output: Nome1

// split
// divide una stringa in base a un separatore
string nomi2 = "Nome1,Nome2,Nome3";
string[] nomi3 = nomi2.Split(',');
foreach (string nome in nomi3)
{
    Console.WriteLine(nome);
}
// output:
// Nome1
// Nome2
// Nome3

// replace
// sostituisce una sottostringa con un altra sottostringa
string nome7 = "Nome1";
Console.WriteLine(nome7.Replace("Nome1", "Nome2")); // output: Nome2

// substring
// restituisce una sottostringa usando un indice iniziale e una lunghezza
string nome8 = "Nome1";
Console.WriteLine(nome8.Substring(0, 3)); // output: Nom

// contains
// verifica se una stringa contiene una sottostringa
string nome9 = "Nome1";
Console.WriteLine(nome9.Contains("Nom")); // output: True

// indexof
// restituisce l indice della prima occorrenza di una sottostringa
// se non trova la sottostringa restituisce -1
// se trova piu occorrenze restituisce l indice della prima occorrenza
string nome10 = "Nome1";
Console.WriteLine(nome10.IndexOf("Nome1")); // output: 0

// lastindexof
// restituisce l indice dell ultima occorrenza di una sottostringa
// se non trova la sottostringa restituisce -1
// parte dalla fine della stringa in questo caso la "o" si trova in posizione 3
string nome11 = "Nome1";
Console.WriteLine(nome11.LastIndexOf("o")); // output: 3

// startswith
// verifica se una stringa inizia con una sottostringa
string nome12 = "Nome1";
Console.WriteLine(nome12.StartsWith("N")); // output: True

// endswith
// verifica se una stringa finisce con una sottostringa
string nome13 = "Nome1";
Console.WriteLine(nome13.EndsWith("1")); // output: True

// tostring
// converte un tipo di dato in una stringa
// funziona con int double char ed altri tipi di dato
int eta3 = 10;
Console.WriteLine(eta3.ToString()); // output: 10

// parse
// converte una stringa in un tipo di dato
// se la conversione non e riuscita viene generata un eccezione di tipo FormatException ed il programma si blocca
string eta4 = "10";
Console.WriteLine(int.Parse(eta4)); // output: 10

// tryparse
// converte una stringa in un tipo di dato e restituisce un valore booleano che indica se la conversione e riuscita
// se la conversione e riuscita il valore convertito viene salvato in una variabile passata per riferimento
// se la conversione non e riuscita il valore convertito e 0
string eta5 = "10";
int eta6;
if (int.TryParse(eta5, out eta6))
{
    Console.WriteLine(eta6); // output: 10
}
else
{
    Console.WriteLine("Conversione non riuscita");
}

// convert
// converte un tipo di dato in un altro tipo di dato
// se la conversione non e riuscita viene generata un eccezione di tipo InvalidCastException ed il programma si blocca
string eta7 = "10";
Console.WriteLine(Convert.ToInt32(eta7)); // output: 10

// concatenazione di stringhe
string nome14 = "Nome1";
string cognome2 = "Rossi";
Console.WriteLine(nome14 + " " + cognome2); // output: Nome1 Rossi

// concatenazione con interpolazione
string nome15 = "Nome1";
string cognome3 = "Rossi";
Console.WriteLine($"{nome15} {cognome3}"); // output: Nome1 Rossi

// concatenazione con string.format
string nome16 = "Nome1";
string cognome4 = "Rossi";
Console.WriteLine(string.Format("{0} {1}", nome16, cognome4)); // output: Nome1 Rossi

// METODI DI CONVERSIONE
// convertire un tipo di dato in un altro tipo di dato

// conversione implicita
// la conversione implicita e possibile solo se non c e perdita di dati ad esempio da int a double ma non da double a int
int eta8 = 10;
double altezza3 = eta8; // conversione implicita da int a double

// conversione esplicita (cast)
double altezza4 = 1.70;
int eta9 = (int)altezza4; // conversione esplicita da double a int

// conversione con metodi
int eta10 = 10;
string eta11 = eta10.ToString(); // conversione da int a stringa

// posso stampare il tipo della variabile con GetType()
Console.WriteLine($"Il tipo della variabile eta e {eta10.GetType()}"); // output: Il tipo della variabile eta e System.Int32
Console.WriteLine($"Il tipo della variabile eta e {eta11.GetType()}"); // output: Il tipo della variabile eta e System.String