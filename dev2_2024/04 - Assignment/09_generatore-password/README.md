# Generatore password

## Obiettivo

Programma in c# che genera una password sicura basata sui seguenti criteri:

- La lunghezza della password deve essere composta tra 8 e 20 caratteri (scelta dall'utente)
- La password deve contenere almeno:
    - 1 lettera maiuscola
    - 1 lettera minuscola
    - 1 numero
    - 1 carattere speciale (es: @, #, !, ecc.).

- La password generata non deve contenere spazi.

## Suggerimenti

- Usa la classe Random per generare caratteri casuali.
- Puoi creare gruppi di caratteri (lettere maiuscole, minuscole, numeri e caratteri speciali) e selezionare casualmente un carattere da ciascun gruppo.

## Versione 1

```csharp
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
```
## Comandi di versionamento

```bash
git add --all
git commit -m "versione1"
git push -u origin main
```