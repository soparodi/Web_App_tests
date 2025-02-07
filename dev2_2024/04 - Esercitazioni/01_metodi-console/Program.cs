// questo e un commento a riga singola

/*
questo e un commento a riga multipla
*/

// il metodo Console.Writeline stampa a video il testo passato come argomento
Console.WriteLine("Hello World!");

Console.WriteLine("Hello World!"); // questo e un commento a fine riga

// il metodo Console.ReadLine legge una riga di testo da tastiera
Console.WriteLine("Inserisci il tuo nome:");
string nome = Console.ReadLine(); // legge una riga di testo da tastiera e la assegna alla variabile nome
// stampo il nome concatenato con una stringa
Console.WriteLine("Ciao " + nome + "!"); // con il segno + posso concatenare stringhe con variabili
// stampo il nome concatenato con una stringa utilizzando l interpolazione
Console.WriteLine($"Ciao {nome}!"); // con l'interpolazione posso concatenare stringhe con variabili
Console.WriteLine("Inserisci il tuo cognome:");
string cognome = Console.ReadLine();
Console.WriteLine($"Ciao {nome} {cognome}!"); // con l'interpolazione posso concatenare piu variabili in modo semplificato
Console.WriteLine($"Ciao {nome.ToUpper()}!"); // il metodo ToUpper() trasforma una stringa in maiuscolo
Console.WriteLine($"Ciao {nome.ToLower()}!"); // il metodo ToLower() trasforma una stringa in minuscolo
// chiedo all utente di inserire l eta
Console.WriteLine("Inserisci la tua eta:");
// leggo l eta da tastiera e la assegno alla variabile eta
string eta = Console.ReadLine();
// stampo l eta
Console.WriteLine($"Hai {eta} anni");
// stampo nome cognome ed eta concatenati
Console.WriteLine($"Ciao {nome} {cognome} hai {eta} anni");
// dichiaro una variabile intera etaInt
int etaInt = 47; // inizializzo la variabile etaInt con il valore 47
string etaStr = etaInt.ToString();
// concateno etaInt con una stringa
Console.WriteLine($"Hai {etaInt} anni");

/*
i metodi di console permettono di gestire l output o l input (il dialogo con l utente) attraverso la console
WriteLine() stampa a video una stringa
ReadLine() legge una stringa da tastiera

ho utilizzato due variabili di tipo string per memorizzare il nome e il cognome dell utente
ho utilizzato una variabile di tipo int per memorizzare l eta dell utente

il metodo ToUpper() trasforma una stringa in maiuscolo
il metodo ToLower() trasforma una stringa in minuscolo

ho provato ad utilizzare la variabile intera direttamente ma mi ha dato errore perche doveva essere convertita in stringa
ho utilizzato il metodo ToString() per convertire un intero in una stringa e l ho interpolata
*/