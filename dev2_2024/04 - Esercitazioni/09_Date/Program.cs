
//GESTIONE DELLE DATE

DateTime birthDate = new DateTime(1990, 1, 1); //Inserisci la tua data di nascita
//DateTime è una struttura che rappresenta un istante di tempo
// il costruttore di DataTime accetta tre parametri : anno, mese , giorno
Console.WriteLine($"sei nato il { birthDate}");
DateTime today = DateTime.Today; //Oggi 

TimeSpan age = today - birthDate;// oggi- data di nascita
// TimeSpan e una struttura che rappresenta un intervallo di tempo

Console.WriteLine($"Hai {age.Days / 365 } anni"); //stampa l'età
//age.Days restituisce i numero di giorni tra due date
//age.Days / 365 restituisce il numero di anni
//posso usare age.TotalDays per avere un valore decimale
//age.hours, age.minutes, age.seconds, age.milliseconds, age.ticks
//i ticks sono i decimi di microsecondi
//age.totalhours, age.totalminutes, age.totalseconds, age.totalmilliseconds, age.totalticks

DateTime nextYear = new DateTime(today.Year +1, 1, 1);
//in questo caso ho usato il costruttore di DateTime con tre parametri che sono anno, mese, giorno
//metto 1 come mese e giorno perchè voglio i primo giorno dell'anno

Console.WriteLine($"Mancano {nextYear - today} giorni a Capodanno"); // stampa i giornimancanti a Capodanno

DateTime nextMonth = today.AddMonths(1);// prossimo mese
//AddMonths è un metodo che aggiunge un numero di mesi ad una data

Console.WriteLine($"Mancano {nextMonth - today} giorni al prossimo mese");// stampa i giorni mancanti al prossimo mese dalla data di oggi
DateTime nextWeek = today.AddDays(7); // prossima settimana

//CONVERSIONI

//Date.Time.Parse è un metodo che converte una stringa in un oggetto DataTime
DateTime date = DateTime.Parse("2025-12-31");
Console.WriteLine(date);

//DateTime.ToString è un metodo che converte un oggetto DateTime in una stringa
string dateString = date.ToString("dd/MM/yyy");
Console.WriteLine(dateString);

//DateTime.TryParse è un  metodo che converte una stringa in un oggetto DateTime
DateTime parseDate;
if (DateTime.TryParse($"duemilaventiquattro-12-31", out parseDate))
{
    Console.WriteLine(parseDate);
}
else
{
    Console.WriteLine("Errore nella conversione della data");
}
//TryParse restituisce true se la conversione è riuscita, altrimenti restituisce false
// il risultato di conversione è restituito tramite il parametro out
//questa è una introduzione alla gestione delle eccezioni

//FORMATTAZIONE
// è possibile formattare le date in diversi modi
Console.WriteLine($"Formato lungo:  {birthDate.ToLongDateString()}");
Console.WriteLine($"Formato corto:  {birthDate.ToShortDateString()} ");

Console.WriteLine($"Mese in formato testuale: {birthDate.ToString("MMM")}");
Console.WriteLine($"Mese in formato testuale : {birthDate.ToString("MM")}");

Console.WriteLine($"Formato personalizzato: {birthDate.ToString("dd-MM-yyy")}");
Console.WriteLine($"Formato personalizzato:  {birthDate.ToString("dddd, dd-MM-yyy")}");

//si può inserire una data e farci restituire il giorno della settimana 
Console.WriteLine($"Il giorno della settimana è: {birthDate.DayOfWeek}");//lo scrive in inglese
//se lo vogliamo in italiano dobbiamo fare una conversione
Console.WriteLine($"Il giorno della settimana è: {birthDate.ToString("dddd")}");
//dddd è il formato per il giorno della settimana in formato testuale

//possiamo farci restituire l'ordine numerico del giorno della settimana
Console.WriteLine($"Il giorno della settimana è: {birthDate.DayOfWeek}");

//possiamo restituire il giorno dell'anno con il metodo DayOfYear
Console.WriteLine($"Il giorno dell'anno è: {birthDate.DayOfYear}");

//OPERAZIONI CON LE DATE

//possiamo sommare o sottrarre un intervallo di tempo a una data
DateTime domani = today.AddDays(1);
//date.Time = today.AddDays(1);
Console.WriteLine($"Domani è:  {domani}" );
DateTime ieri = today.AddDays(-1);
Console.WriteLine($"Ieri era:  {ieri}");
//domani in formato stringa
Console.WriteLine($"Domani sarà: {domani.ToString("dddd") }");

//Calcolare i giorni fino al prossimo compleanno
DateTime nextBirthday = new DateTime(today.Year,1,1);
if (nextBirthday < today)
{
nextBirthday = nextBirthday.AddYears(1);
}
int daysUntilBirthday = (nextBirthday - today).Days;

//confronto fra date
DateTime date1 = DateTime.Today; //oggi
DateTime date2 = new DateTime(2024, 12, 31);// scegli una data
int result = DateTime.Compare(date1, date2 );// confronto tra date
if (result<0)
{
    Console.WriteLine("La prima data viene prima della seconda data.");
}
else if (result>0)
{
    Console.WriteLine("La seconda data viene prima della prima data.");
}
else 
{
Console.WriteLine("Le due date sono uguali.");
}

//calcolare la differenza tra due date usando il metodo Substract
DateTime start = new DateTime(2024, 12, 31);//inserisci la data di inizio
DateTime end = new DateTime(2024, 12, 31); // inserisci la data di fine
TimeSpan difference = end.Subtract(start);//calcola la differenza tra le due date
Console.WriteLine($"La differenza fra le due date è di: {difference.Days} giorni");

// calcolare la differenza tra due date usando TimeSpan
DateTime startDate = DateTime.Today;
DateTime endDate = new DateTime(2024, 12, 31);
TimeSpan differenceDate = endDate - startDate;
Console.WriteLine($"La differenza tra le due date e di {differenceDate.Days} giorni");
Console.WriteLine($"La differenza tra le due date e di {differenceDate.TotalDays} giorni");
Console.WriteLine($"La differenza tra le due date e di {differenceDate.TotalHours} ore");

// manipalzione di date usando il metodo Add
TimeSpan timeSpan = new TimeSpan(5, 3, 5, 10, 0, 0); // 5 giorni, 3 ore, 5 minuti, 10 secondi e 0 millisecondi 0 microsecondi
DateTime todayDate = DateTime.Today; // Oggi
DateTime resultDate = todayDate.Add(timeSpan); // Aggiungi l'intervallo di tempo a oggi
Console.WriteLine($"La data risultante e: {resultDate}");