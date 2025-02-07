//MATH

//La libreria Math in c# è una collezione di metodi statici che forniscono funzionalità matematiche di base, coma calcoli su numeri, 
//trigonometria, logaritmi ecosì via

//abs
//Il metodo Math.Abs() restituisce il valore assoluto di un numero. Il valore assoluto di un numero è il numero stesso senza il segno meno.

int number1 = -10;
int absNumber = Math.Abs(number1);
Console.WriteLine(absNumber);

//arrotondamenti
//celiling
//Il metodo Math.Ceiling() restituisce il piu piccolo intero maggiore o uguae a un numero decimale.
double number2 = 10.1;
double ceilingNumber = Math.Ceiling(number2);
Console.WriteLine(ceilingNumber);


//floor
//Il metdodo Math.Floor() restituisce il più grande intero minore o uguale a un numero decimle.
double number3 = 10.9;
double floorNumber = Math.Floor(number3);
Console.WriteLine(floorNumber);

//round
//il metodo Math.Round() arrotonda un numero decimale al numero intero piu vicino.
double number4 = 10.5000;
double intNumber = Math.Round(number4);
double roundNumber = Math.Round(number4, 2);// posso utilizzare un secondo parametro per specificare il numero di cifre decimali
Console.WriteLine(intNumber);
Console.WriteLine(roundNumber);

//midpointrounding
double[] numeri = {3.5, 3.4, 5.5};
for (int i = 0; i< numeri.Length; i++)
{
    double arrotondaPerDifetto = Math.Round(numeri[i], MidpointRounding.ToEven);
    double arrotondaPerEccesso = Math.Round(numeri[i], MidpointRounding.AwayFromZero);
    Console.WriteLine($"Arrotonda per difetto : {arrotondaPerDifetto}");
    Console.WriteLine($"Arrotonda per eccesso: {arrotondaPerEccesso}");
}

//max
//Il metodo Math.Max() restituisce il numero più grande tra i due numeri.
int number5 = 10;
int number6 = 20;
int maxNumber = Math.Max(number5, number6);
Console.WriteLine($"Il numero più grande è {maxNumber}");

//min
//Il metodo Math.Min() restituisce il numero più piccolo tra due numeri.
int number7 = 10;
int number8 = 20;
int minNumber = Math.Min(number7, number8);
Console.WriteLine($"Il numero minore è {minNumber}");

//average
//Il metodo Math.Average() 

//pow
//Il metodo Math.Pow() restituisce il valore di un numero elevato alla potenza di un altro numero
double number9 = 2;
double number10 = 3;
double powNumber = Math.Pow(number9, number10);
Console.WriteLine(powNumber);
//devo usare un double per il risultato, altrimenti ottengo un errore perchè il risultato potrebbe essere un numero decimale

//sqrt
//Il metodo Math.Sqrt() restituisce la radice qyadrata di un numero.
double number11 = 17;
double sqrtNumber = Math.Sqrt(number11);
Console.WriteLine(sqrtNumber);

//dvrem
//Il metodo Math.Dvrem() restituisce il quoziente e il resto di una divisione intera
int dividendo = 10;
int divisore = 3;
int quoziente = Math.DivRem(dividendo, divisore, out int resto);
Console.WriteLine($"");

//cos
//Il metodo Math.Cos() restituisce il coseno di un angolo specificato in radianti
double angle =45;
double cosNumber = Math.Cos(angle);
Console.WriteLine(cosNumber);

//sin
//Il metodo Math.Sin() restituisce il seno di un alngolo specificato in radianti.
double angle2 = 45;
double sinNumber = Math.Sin(angle2);
Console.WriteLine(sinNumber);

//tan
//Il metodo Math.Tan() restituisce la tangente di un angolo specificato in radianti.
double angle3 = 45;
double tanNumber = Math.Tan(angle3);    
Console.WriteLine(tanNumber);

// PI uso di costanti
double raggio = 5;
double area = 2 *Math.PI * Math.Pow(raggio, 2);
double circonferenza = 2 * Math.PI * raggio;
Console.WriteLine($"Area: {area}");
Console.WriteLine($"Circonferenza : {circonferenza}");


//ESERCIZI

//Arrotonda un array di numeri decimali alla seconda cifra decimale usando Math.Round
double[] number = {3.14159, 2.71828, 1.61803};

double[] arrotondati = new double[numeri.Length];
for (int i = 0;i< numeri.Length;i++)
{
    arrotondati[i] = Math.Round(numeri[i],2);
    Console.WriteLine($"Numero arrotondato:{numeri[i]}");
}
//TROVA IL VALORE MASSIMO E MINIMO IN UN ARRAY DI INTERI USANDO Math.Max() e Math.Min()
int[] numeri2 = {5, 9,1,3,4};
int min = numeri2 [0]; //inizializza il minimo al primo elemento dell'array in modo che possa essere confontato
int max = numeri2 [0];// inizializza il massimo al primo elemento dell'array in modo che possa essere confrontato
for (int i = 1; i< numeri2.Length; i++) // inizia dal secondo elemento dell'array
{
    min= Math.Min(min, numeri2[i]);//trova il minimo tra il minimo attuale e l'elemento corrente
    max= Math.Max(max, numeri2[i]);//trova il massimo tra il massimo attuale e l'elemento corrente
}
Console.WriteLine($"Il valore minimo è: {min}");
Console.WriteLine($"Il valore massimo è: {max}");

//ARROTONDA PER ECCESSO E PER DIFETTO UN ARRAY DI NUMERI DECIMALI USANDO math.Floor
double[] numeri3 = {3.354, 2.432, 2.544};
for (int i = 0; i < numeri3.Length; i++)
{
    double perEccesso = Math.Ceiling(numeri3[i]); // Arrotonda per eccesso
    double perDifetto = Math.Floor(numeri3[i]); // Arrotonda per difetto
    Console.WriteLine($"Arrotondato per eccesso: {perEccesso}");
    Console.WriteLine($"Arrotondato per difetto: {perDifetto}");
}