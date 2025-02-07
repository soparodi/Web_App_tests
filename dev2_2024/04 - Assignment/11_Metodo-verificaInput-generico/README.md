# Verifica Input generico

## Obiettivo

Programma in c# che contiene una serie di metodi generici per la verifica degli input.

- un metodo per la verifica di un numero intero
- un metodo per la verifica di numero decimale
- un metodo per la verifica di che un numero sia compreso in un intervallo
- un metodo per la verifica di di una stringa non vuota
- un metodo per la verifica di un char non vuoto
```csharp
// passo a VerificaIntero il messaggio da stampare a video
// restituisce l'intero inserito dall'utente
int numero = VerificaIntero("Inserisci un numero intero: ");
Console.WriteLine($"Hai inserito il numero {numero}");

// passo a VerificaDecimale il messaggio da stampare a video
// restituisce il decimale inserito dall'utente
double numeroDecimale = VerificaDecimale("Inserisci un numero decimale: ");
Console.WriteLine($"Hai inserito il numero {numeroDecimale}");

// passo a VerificaCompreso il messaggio da stampare a video
// restituisce il decimale inserito dall'utente
double numeroCompreso = VerificaCompreso("Inserisci un numero compreso tra 1 e 10: ", 1, 10);
Console.WriteLine($"Hai inserito il numero {numeroCompreso}");

// passo a VerificaStringa il messaggio da stampare a video
// restituisce la stringa inserita dall'utente
string stringa = VerificaStringa("Inserisci una stringa: ");
Console.WriteLine($"Hai inserito la stringa {stringa}");

// passo a VerificaChar il messaggio da stampare a video
// restituisce il carattere inserito dall'utente
char carattere = VerificaChar("Inserisci un carattere: ");
Console.WriteLine($"Hai inserito il carattere {carattere}");

// funzioni di verifica input

// verifica intero con messaggio personalizzato
// prende in input una stringa da stampare a video
// restituisce l'intero inserito
static int VerificaIntero(string messaggio)
{
    int numero; // dichiaro la variabile numero
    bool successo; // dichiaro la variabile successo
    // do while che cicla finchè non si inserisce un numero intero
    do
    {
        Console.Write(messaggio); // stampo il messaggio cioe "Inserisci un numero intero: " che gli passo
        successo = int.TryParse(Console.ReadLine(), out numero); // TryParse tenta di convertire la stringa in un numero intero
    // se non ci riesce restituisce false e continua a ciclare
    } while (!successo);
    return numero; // restituisco la variabile numero
}

// verifica decimale con messaggio personalizzato
// prende in input una stringa da stampare a video
// restituisce il decimale inserito
static double VerificaDecimale(string messaggio)
{
    double numero;
    bool successo;
    do
    {
        Console.Write(messaggio);
        successo = double.TryParse(Console.ReadLine(), out numero);
    } while (!successo);

    return numero;
}

// verifica decimale compreso tra min e max con messaggio personalizzato
// prende in input una stringa da stampare a video, il valore min e il valore max
// restituisce il decimale inserito
static double VerificaCompreso(string messaggio, double min, double max)
{
    double numero;
    bool successo;
    do
    {
        Console.Write(messaggio);
        successo = double.TryParse(Console.ReadLine(), out numero);
    } while (!successo || numero < min || numero > max);

    return numero;
}

// verifica stringa con messaggio personalizzato
// prende in input una stringa da stampare a video
// restituisce la stringa inserita
static string VerificaStringa(string messaggio)
{
    string stringa;
    do
    {
        Console.Write(messaggio);
        stringa = Console.ReadLine();
    } while (string.IsNullOrEmpty(stringa));

    return stringa;
}

// verifica carattere con messaggio personalizzato
// prende in input un carattere da stampare a video
// restituisce il carattere inserito
static char VerificaChar(string messaggio)
{
    char carattere;
    do
    {
        Console.Write(messaggio);
        carattere = Console.ReadKey().KeyChar;
    } while (char.IsWhiteSpace(carattere));

    return carattere;
}
```