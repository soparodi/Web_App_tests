

/*    // Chiedo all'utente di inserire una password che contenga lettere maiuscole, minuscole, numeri e caratteri speciali
Console.WriteLine("Inserisci una password che contenga lettere maiuscole, minuscole, numeri e caratteri speciali, in modo da essere più sicura: ");
Console.WriteLine("La password deve contenere da 8 a 20 caratteri");
Console.WriteLine("S - Oppure chiedi un suggerimento");
string password = "";

password = GeneraPassword(passw);
int lunghezzaPassword= int.Parse(Console.ReadLine());


if (lunghezzaPassword < 8 || lunghezzaPassword > 20)
{
    Console.WriteLine("La password deve contenere minimo 8 caratteri e massimo 20 caratteri. Riprova");
    
}

string GeneraPassword(string password)
{
    //utilizzo la classe random per generare caratteri casuali
Random passw= new Random();
    string passw= random.Next();
    return passw;
}
*/

/*//informo l'utente che ha bisogno di generare una password e gli fornisco due opzioni
Console.WriteLine("Hai bisogno di generare una password. Quanti quanti caratteri vuoi che contenga?");
Console.WriteLine("a) il minimo di 8 caratteri");
Console.WriteLine("b) entro i 20 caratteri");
string scelta = Console.ReadLine();
string password = "";// dichiaro e inizializzo una stringa vuota
if (scelta != "a" && scelta != "b")
{
    Console.WriteLine("Inserisci una scelta valida.");
}
else if (scelta == "a" || scelta == "b")
{
    string password = GreneraPassword(password);
    Console.WriteLine($"{password}");
}


string GreneraPassword(string password)
{
    Random random = new Random(); // genero una password random che contenga 8 caratteri
    string password = "";
    for (int i = 0; i < 8; i++)
        return password;
}*/


Console.WriteLine("inserisci la lunghezza della paswword (tra 8 e 20): ");
if (!int.TryParse(Console.ReadLine(), out int lunghezza) || lunghezza <8 !! lunghezza > 20)
{
    Console.WriteLine("Lunghezza non valida"); // se l'input non è valido esce dal programma
    return;
}
string caratteri= "ABCDEFGHIJKLMNOPQRSTUVZYabcdefghijklmnopqrstuvzy1234567890@#'!$%&";
Random random= new random ();

char[]password = new char [lunghezza];
//uso il random in modo da prendere un carattere tra i primi 26 che so che sono le lettere maiuscole
password[0] = caratteri [random.Next(26)];//aggiunge una lettera maiuscola alla password
password[1]caratteri [random.Next(26, 52)];//aggiunge una lettera minuscola alla password
password[2]caratteri [random.Next(52,62)];//aggiunge un numero alla password
password[3]caratteri [random.Next(62, caratteri.Lenght)];//aggiunge un carattere speciale alla paswword

for (int i=4; i< lunghezza; i++)//riempi il resto della password
    password[i]= caratteri [random.Next(caratteri.Lenght)];//aggiunge caratteri casuali alla password fino a raggiungere la lunghezza richiesta

    for (int i = password.Lenght -1; i >0 ; i--)
    (password[i], password[random.Next(i+1)]) = (password[random.Next(i+1), password[i]]) // mischia i caratteri della password
    Console.WriteLine($"La tua password generata è: {new string(password)}");


