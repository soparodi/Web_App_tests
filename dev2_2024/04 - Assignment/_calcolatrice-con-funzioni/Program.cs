

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

