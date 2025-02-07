## Calcolatrice con funzioni

# Versione 1

# Obiettivo

- implementare le funzioni nella versione semplificata della calcolatrice.

```csharp
// chiedi all'utente di inserire due numeri
double num1 = ChiediNumero();
double num2 = ChiediNumero();

// chiedi all'utente di selezionare un operatore matematico
char operatore = ChiediOperatore();

// esegui l'operazione selezionata
double risultato = EseguiOperazione(num1, num2, operatore);

// visualizza il risultato
Console.WriteLine($"Il risultato è: {risultato}");

```
# Codice completo

```csharp
// chiedi all utente di inserire due numeri
double num1 = ChiediNumero();
double num2 = ChiediNumero();

// chiedi all utente di selezionare un operatore matematico
char operatore = ChiediOperatore();

// esegui l operazione selezionata
double risultato = EseguiOperazione(num1, num2, operatore);

// visualizza il risultato
Console.WriteLine($"Il risultato è: {risultato}");

double ChiediNumero()
{
    double num = 0;
    bool inputValido = false;
    while (!inputValido)
    {
        try
        {
            Console.Write("Inserisci un numero: ");
            num = Convert.ToDouble(Console.ReadLine());
            inputValido = true;
        }
        catch (FormatException)
        {
            Console.WriteLine("Inserisci un numero valido.");
        }
    }
    return num;
}

char ChiediOperatore()
{
    char operatore = ' ';
    bool inputValido = false;
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
    return operatore;
}

double EseguiOperazione(double num1, double num2, char operatore)
{
    double risultato = 0;
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
            risultato = num1 / num2;
            break;
    }
    return risultato;
}
// output atteso
```

# Comandi di versionamento

```bash
git add --all
git commit -m "Implementata la versione 1 della calcolatrice con funzioni"
git push -u origin main
```

## Versione 1.2

```csharp

double numero1 = ChiediNumero("Inserisci il primo numero");
string operazione = ChiediOperazione();
double numero2 = ChiediNumero("Inserisci il secondo numero");
double risultato = 0;

if (numero2 == 0 && operazione == "/")
{
    Console.WriteLine("Mi dispiace, non puoi dividere per 0");
}

switch (operazione)
{
    case "+":
        risultato = numero1 + numero2;
        break;

    case "-":
        risultato = numero2 - numero1;
        break;

    case "*":
        risultato = numero1 * numero2;
        break;

    case "/":
        risultato = numero1 / numero2;
        break;

}
StampaRisultato();

double ChiediNumero(string messaggio)
{
    Console.WriteLine(messaggio);
    return Convert.ToDouble(Console.ReadLine());
}

string ChiediOperazione()
{
    Console.WriteLine("Ora scegli un'operazione: ");
    return Console.ReadLine();
}

void StampaRisultato()
{
    Console.WriteLine($"il risultato è : {numero1} {operazione} {numero2} = {risultato}");
}

```