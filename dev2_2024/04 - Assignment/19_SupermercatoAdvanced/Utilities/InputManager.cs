
public static class InputManager
{
    //minvalue e maxvalue sono i metodi di int che rappresentano il valore minimo ed il valore massimo di un intero
    // il metodo LeggiIntero accetta un messaggio da visualizzare
    public static int LeggiIntero(string messaggio, int min = int.MinValue, int max = int.MaxValue)
    {
        int valore; //Vriabile per memorizzare il valore intero acquisito
        while (true)
        {
            Console.Write($"{messaggio}"); //messaggio e la variabile di input che dovrò passare al metodo
            string input = Console.ReadLine();//acquisire l'input dell'utente come stringa
            // try parse per convertire la stringa  in un intero e controllare se l'input è valido
            if (int.TryParse(input, out valore) && valore >= min && valore <= max) // devo verifiare se il valore e tra min e max e se è un intero
            {
                return valore; // restituire il valore intero se è valido
            }
            else
                Console.WriteLine($"Inserire un valore intero compreso tra {min} e {max}"); // messaggio di errore
        }
    }

    public static decimal LeggiDecimale(string messaggio, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        decimal valore; //variabile per memorizzare il valore decimale acquisito
        while (true)
        {
            Console.Write($"{messaggio}");
            string input = Console.ReadLine();

            //sostituisco la virgola con il punto per gestire i decimali
            if (input.Contains(".")) //se l'input contiene la virgola e non contiene il punto
            {
                input = input.Replace(",", "."); //sostituire la virgola con il punto
            }

            // try parse per convertire la stringa in un decimale e controllare se l'input è valido
            if (decimal.TryParse(input, out valore) && valore >= min && valore <= max)
            {
                return valore;
            }
            else
            {
                Console.WriteLine($"errore: inserire un numero decimale comprso tra {min} e {max}");
            }
        }
    }

    public static string LeggiStringa(string messaggio, bool obbligatorio = true)
    {
        string valore;
        while (true)
        {
            Console.Write($"{messaggio}"); // messaggio e la variabile di input che devo passare al metodo
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) || !obbligatorio) // se l'input non è vuoro o non è obbligtaorio
            {
                return input; // restituire il valore della stringa
            }
            Console.WriteLine($"errore: il valore non può essere vuot");
        }
    }

    public static bool LeggiConferma(string messaggio)
    {
        while (true)
        {
            Console.Write($"{messaggio} (s/n): ");
            string input = Console.ReadLine().ToLower();
            if (input == "s" || input == "si")
            {
                return true;
            }
            else if (input == "n" || input == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("errore: rispondere con 's' o 'n' ");
            }
        }
    }
    public static double LeggiDouble(string messaggio, double min = double.MinValue, double max = double.MaxValue)
    {
        double valore; //variabile per memorizzare il valore decimale acquisito
        while (true)
        {
            Console.Write($"{messaggio}");
            string input = Console.ReadLine();
            if (input.Contains(","))
            {
                input = input.Replace(",", ",");

                // try parse per convertire la stringa in un decimale e controllare se l'input è valido
                if (double.TryParse(input, out valore) && valore >= min && valore <= max)
                {
                    return valore;
                }
                else
                {
                    Console.WriteLine($"errore: inserire un numero double compreso tra {min} e {max}");

                }
            }
        }
    }
}