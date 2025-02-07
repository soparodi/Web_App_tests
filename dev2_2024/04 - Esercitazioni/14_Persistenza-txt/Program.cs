// GESTIONE FIL TXT

//LEGGERE UN CONTENUTO DA UN FILE TXT

string path = @"test.txt"; // in questo il file è nella stessa cartella del programma. La chiocciola fa riferimento ma in questo caso posso anche ometterla.//devo poter mettere un metodo
string[] lines = File.ReadAllLines(path); // legge tutte le righe del file e le mette in un array di stringhe
foreach (string line in lines)
{
    Console.WriteLine(line);   //stampa la riga
}


//LEGGERE UN CONTENUTO E STAMPARE SOLO LE RIGHE CHE INIZIANO CON UNA DETERMINATA LETTERA

//string path = @"test.txt"; 
//string[]lines = File.ReadAllLines(path);

foreach (string line in lines)
{
    if (line.StartsWith("n")) // controlla se la riga inizia con la lettera
    {
        Console.WriteLine(line);// stampa la riga
    }
}
Console.Clear();
//LEGGERE UN CONTENTUTO DA UN TEXT E STAMPARE SOLO LE RIGHE CHE INIZIANO CON UNA DETERMINATA LETTERA
//SE NESSUN NOME INCOMINCIA CON LA LETTERA SCELTA, DA UN MESSAGGIO DI ERRORE
bool errore = true;
foreach (string line in lines)
{
    if (line.StartsWith("c"))
    {
        Console.WriteLine(line);
        errore = false;
    }
}
if (errore)

{
    Console.WriteLine("NESSUNA RIGA CONTIENE QUELLO CHE CERCHI");
}
//LEGGERE UN CONTENTUTO DA UN TEXT E STAMPARE SOLO LE RIGHE CHE INIZIANO CON UNA DETERMINATA LETTERA
//SE NESSUN NOME INCOMINCIA CON LA LETTERA SCELTA, DA UN MESSAGGIO DI ERRORE
//CREARE UN TXT CON LE RIGHE CHE INIZIANO CON LA LETTERA SCELTA
Console.Clear();

string path2 = @"testOutput.txt";
File.Create(path2).Close();// crea il file e lo chiude subito
foreach (string line in lines)
{
    if (line.StartsWith("c"))
    {
        errore = false;
        File.AppendAllText(path2, line + "\n");
    }
}
if (errore) // se il booleano è vero allora stampa il messaggio che ho inserito
{
    Console.WriteLine("NESSUNA RIGA CONTIENE QUELLO CHE CERCHI");
}

//AGGIUNGERE UNA RIGA DI TESTO IN UNA POSIZIONE SPECIFICA
//stampo la lunghezza dell'array
Console.WriteLine(lines.Length);

lines[lines.Length - 2] += "Indirizzo"; // aggiunge indirizzo alla penultima riga
File.WriteAllLines(path2, lines);// scrive tutte le righe nel file

//AGGIUNGERE UNA RIGA DI TESTO IN UNA POSIZIONE SPECIFICA USANDO L'ACCENTO CIRCONFLESSO
lines[^2] += "numero di telefono2";
File.WriteAllLines(path2, lines);

//SOVRASCRIVERE UNA RIGA DI TESTO IN UNA POSIZIONE SPECIFICA
lines[lines.Length - 2] = "NUMERO DI TELEFONO"; // aggiunge indirizzo alla penultima riga
File.WriteAllLines(path2, lines);//scrive tutte le righe nel file

//StreamWriter
//esempio
void ScriviTentativiSuFile(Dictionary<string, List<int> tentativiUtenti, string nomeUtente)
{
    using (StreamWriter sw = new StreamWriter($"{nomeUtente}.txt"))
    {
        foreach (var tentativoUtente in tentativiUtenti)
        {
            if (tentativoUtente.Key == nomeUtente)
            {
                sw.WriteLine($"{tentativoUtente.Key}: {string.Join(",", tentativoUtente.Value)}");
            }
        }
    }
}