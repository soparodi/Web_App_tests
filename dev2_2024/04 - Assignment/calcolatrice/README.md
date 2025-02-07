# Versione 1

## Obiettivo

- Scrivere un programma che simuli una calcolatrice semplice.
- L utente deve poter inserire due numeri e selezionare un operatore matematico (+, -, *, /)
- Il programma deve eseguire l'operazione selezionata e stampare il risultato.
- Il programma non gestisce nessun tipo di errore o di eccezione.

```csharp
//chiedo all'utente di inserire due numeri

    Console.WriteLine("Inserisci un numero");
    double numero1 = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("inserisci il secondo numero");
    double numero2 = Convert.ToDouble(Console.ReadLine());

    //chiedo all'utente di inserire un operatore matematico
    Console.WriteLine("Inserisci un operatore:");
    Console.WriteLine("+, -, /, * ");

    string operatore = Console.ReadLine();
    double risultato = 0;

    switch (operatore)
    {
        case "+":
            risultato = numero1 + numero2;
            break;
        case "-":
            risultato = numero1 - numero2;
            break;
        case "/":
            risultato = numero1 / numero2;
            break;

        case "*":
            risultato = numero1 * numero2;
            break;

    }

//stampa il risultato
Console.WriteLine($"Il risultato dell'operazione {numero1} {operatore} {numero2} è: {risultato}");

```
## Comandi di versionamento

```bash
git add --all
git commit -m "versione1"
git push -u origin main
```
# Versione 2

## Obiettivo

- Aggiungere la gestione degli errori per evitare crash del programma.
- Se l'utente inserisce un valore non numerico, il programma deve stampare un messaggio di errore dicendo di inserire un numero valido
- Se l'utente seleziona un operatore non valido, il programma deve stampare un messaggio di errore dicendo di selezionare un operatore valido
- Se l'utente tenta di dividere per zero, il programma deve stampare un messaggio di errore dicendo che la divisione per zero non è consentita
- Il programma deve usare i blocchi try-catch per gestire gli errori

```csharp
double numero1 = 0;
            double numero2 = 0;
            string operatore;
            bool inputValido = false;

            // Gestione dell'input del primo numero
            while (!inputValido)
            {
                try
                {
                    Console.WriteLine("Inserisci un numero");
                    numero1 = Convert.ToDouble(Console.ReadLine());
                    inputValido = true;  // Se il numero è valido, esco dal ciclo
                }
                catch (FormatException)
                {
                    Console.WriteLine("Errore: per favore, inserisci un numero valido.");
                }
            }

            inputValido = false; // 

            // Gestione dell'input del secondo numero
            while (!inputValido)
            {
                try
                {
                    Console.WriteLine("Inserisci il secondo numero");
                    numero2 = Convert.ToDouble(Console.ReadLine());
                    inputValido = true;  // Se il numero è valido, esco dal ciclo
                }
                catch (FormatException)
                {
                    Console.WriteLine("Errore: per favore, inserisci un numero valido.");
                }
            }

            // Chiedo all'utente di inserire un operatore matematico
            Console.WriteLine("Inserisci un operatore (+, -, *, /):");
            operatore = Console.ReadLine();

            double risultato = 0;

            try
            {
                switch (operatore)
                {
                    case "+":
                        risultato = numero1 + numero2;
                        break;
                    case "-":
                        risultato = numero1 - numero2;
                        break;
                    case "*":
                        risultato = numero1 * numero2;
                        break;
                    case "/":
                        if (numero2 == 0)
                        {
                            throw new DivideByZeroException("La divisione per zero non è consentita.");
                        }
                        risultato = numero1 / numero2;
                        break;
                    default:
                        throw new InvalidOperationException("Operatore non valido. Per favore, inserisci +, -, *, o /.");
                }

                // Stampa il risultato
                Console.WriteLine($"Il risultato dell'operazione {numero1} {operatore} {numero2} è: {risultato}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore imprevisto: {ex.Message}");
            }
```
## Comandi di versionamento

```bash

git add --all
git commit -m "versione2"
git push -u origin main
```
# Versione 2.1

## Obiettivo
- Aggiungere la gestione degli errori per evitare crash del programma.
- Se l'utente inserisce un valore non numerico, il programma deve stampare un messaggio di errore dicendo di inserire un numero valido
- Se l'utente seleziona un operatore non valido, il programma deve stampare un messaggio di errore dicendo di selezionare un operatore valido
- Se l'utente tenta di dividere per zero, il programma deve stampare un messaggio di errore dicendo che la divisione per zero non è consentita
- Il programma deve usare i blocchi try-catch per gestire gli errori

```csharp
// Chiedi all'utente di inserire due numeri
double num1 = 0;
double num2 = 0;
bool inputValido = false;
while (!inputValido)
{
    try
    {
        Console.Write("Inserisci il primo numero: ");
        num1 = Convert.ToDouble(Console.ReadLine());
        inputValido = true;
    }
    catch (FormatException)
    {
        Console.WriteLine("Inserisci un numero valido.");
    }
}
inputValido = false;
while (!inputValido)
{
    try
    {
        Console.Write("Inserisci il secondo numero: ");
        num2 = Convert.ToDouble(Console.ReadLine());
        inputValido = true;
    }
    catch (FormatException)
    {
        Console.WriteLine("Inserisci un numero valido.");
    }
}
// Chiedi all'utente di selezionare un operatore matematico
char operatore = ' ';
inputValido = false;
while (!inputValido)
{
    Console.Write("Seleziona un operatore (+, -, *, /): ");
    operatore = Console.ReadKey().KeyChar;
    Console.WriteLine();
    if (operatore == '+' || operatore == '-' || operatore == '*' || operatore == '/')
    {
        inputValido = true;
    }
    else
    {
        Console.WriteLine("Operatore non valido.");
    }
}
// Esegui l'operazione selezionata
double risultato = 0;
try
{
    switch (operatore)
    {
        case '+':
            risultato = num1 + num2;
            break;
        case '-':
            risultato = num1 - num2;
            break;
        case '*':
            risultato = num1 * num2;
            break;
        case '/':
            if (num2 == 0)
            {
                throw new DivideByZeroException();
            }
            risultato = num1 / num2;
            break;
    }
    // Stampa il risultato
    Console.WriteLine($"Il risultato dell'operazione è: {risultato}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Impossibile dividere per zero.");
}
```
# Comandi di versionamento

```bash
git add --all
git commit -m "versione 2.1"
git push -u origin main

```
