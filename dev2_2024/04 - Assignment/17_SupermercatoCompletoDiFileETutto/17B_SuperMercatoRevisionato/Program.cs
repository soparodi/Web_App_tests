// using è la direttiva per importare una libreria
// newtonsoft.json è la libreria che permette di serializzare e deserializzare i file json
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

// filepath è la variabile stringa che identifica la posizione del catalogo
string filePath = "catalogo.json";

// string cartella = "cartella di prova";
// string nuovoPercorso = Path.Combine(cartella, filePath);

// continua permette di uscire dal programma ma potrei migliorarlo in modo da uscire dal menu cliente o lavoratore ritornando alla scelta dell'utente
// MIGLIORAMENTO: IMPLEMENTARE LA LOGICA PER PASSARE DA UN MENU ALL'ALTRO
bool continua = true;
bool termina = false;
// una lista di dizionari string object che rapprestenta il catalogo dei prodotti
List<Dictionary<string, object>> catalogo = new List<Dictionary<string, object>>();
List<Dictionary<string, object>> carrello = new List<Dictionary<string, object>>();
Console.WriteLine("--- Benvenuto al Supermercato Json ---");
Console.WriteLine("Digita '1' se sei un nostro dipendente, '2' se sei un cliente ");

// selezione è la variabile che viene acquisita dall'utente e serve per selezionare il tipo di utenza
// MIGLIORAMENTO: DARE DEI NOMI SIGNIFICATI ALLE VARIABILI, IN QUESTO CASO 'selezionaUtente'
string selezione = Console.ReadLine();

// rendo disponibile il catalogo a tutta l'applicazione utilizzando la funzione CaricaCatalogoDalFile
// se non lo faccio quando sono sul menu del cliente non ho nessun prodotto nel catalogo
// MIGLIORAMENTO: RICORDARSI DI TOGLIERE DAL MENU OPERATORE LA VOCE Console.WriteLine("6. Carica il catalogo dal file");

catalogo = CaricaCatalogoDalFile(filePath);

// inizio del ciclo while di scelta delle operazioni di ogni utente
// LOGICA DA IMPLEMENTARE CON LA VARIABILE continua 
// IN PRATICA DOBBIAMO CREARE UNA VARIABILE DI CONTROLLO TIPO 'continua' PER OGNI MENU
// continuaOperatore e continuaCliente saranno i nomi delle due nuove variabili
while (!termina)
{


    while (continua)
    {
        // menu dipendente
        if (selezione == "1")
        {
            Console.WriteLine("Seleziona un'operazione da effettuare:");
            Console.WriteLine("1. Visualizza il catalogo");
            Console.WriteLine("2. Aggiungi un prodotto al catalogo tramite ID");
            Console.WriteLine("3. Modifica il prezzo di un prodotto");
            Console.WriteLine("4. Rimuovi un prodotto dal catalogo");
            Console.WriteLine("5. Salva il catalogo sul file");
            Console.WriteLine("6. Carica il catalogo dal file");
            Console.WriteLine("0. ESCI");

            // sceltaOperazione identifica la scelta effettuata dall'utente all'interno del menu
            // MIGLIORAMENTO: DARE UN NOME SIGNIFICATIVO COME 'sceltaOperazioneDipendente'
            string sceltaOperazione = Console.ReadLine();

            switch (sceltaOperazione)
            {
                case "1":
                    VisualizzaCatalogo(catalogo);
                    break;

                case "2":
                    AggiungiAlCatalogo(catalogo);
                    break;

                case "3":
                    ModificaPrezzo(catalogo);
                    break;

                case "4":
                    RimuoviDalCatalogo(catalogo);
                    break;

                case "5":
                    SalvaCatalogoSuFile(catalogo, filePath);

                    break;

                // QUESTA E' L'OPZIONE DA ELIMINARE PERCHE' L'ABBIAMO RESA DISPONIBILE A TUTTA L'APPLICAZIONE ALLA RIGA 25
                case "6":
                    catalogo = CaricaCatalogoDalFile(filePath);
                    break;

                case "0":
                    Console.WriteLine("Grazie per il tuo lavoro!");
                    continua = false;
                    break;
            }
        }
        else
        {
            // menu cliente 
            Console.WriteLine("Scegli un'operazione:");
            Console.WriteLine("1. Visualizza il catalogo");
            Console.WriteLine("2. Aggiungi prodotto al carrello");
            Console.WriteLine("3. Elimina un prodotto dal carrello");
            Console.WriteLine("4. Visualizza il carrello");
            Console.WriteLine("0 ESCI");

            // sceltaCliente è la variabile che memorizza l'operazione effettuata dal cliente
            // MIGLIORAMENTO: DARE UN NOME SIGNIFICATIVO COME 'sceltaOperazioneCliente'
            string sceltaCliente = Console.ReadLine();

            // inizio dello switch di scelta delle operazioni del cliente
            switch (sceltaCliente)
            {
                case "1":
                    VisualizzaCatalogo(catalogo);
                    break;

                case "2":
                    AggiungiAlCarrello(catalogo, carrello);
                    break;

                case "3":
                    RimuoviProdottoDaCarrello(carrello);
                    break;

                case "4":
                    VisualizzaCarrello(carrello);
                    break;

                case "0":
                    Console.WriteLine("Grazie, a presto!");
                    continua = false;
                    break;

                default:
                    Console.WriteLine("Inserisci una scelta valida");
                    break;

            }
        }
    }
    Console.WriteLine("Vuoi uscire dal programma? s/n");
    string risposta = Console.ReadLine();
    if (risposta == "s")
    {
        termina = true;
    }
    else
    {
        termina = false;
        continua = true;
    }
}

// INIZIO OPERAZIONE CRUD DEL DIPENDENTE ( Create. Read. Update. Delete )

// AggiungiAlCatalogo accetta come parametro la lista dei dizionari catalogo
// non restituisce nulla quindi è una funziona 'void'
static void AggiungiAlCatalogo(List<Dictionary<string, object>> catalogo)
{
    Console.WriteLine("Inserisci il codice ID:");
    int id = int.Parse(Console.ReadLine());

    Console.WriteLine("Inserisci il nome del prodotto:");
    string nomeProdotto = Console.ReadLine();

    Console.WriteLine("Inserisci il prezzo del prodotto:");
    decimal prezzoProdotto = decimal.Parse(Console.ReadLine());

    Console.WriteLine("Inserisci la quantita da aggiungere del prodotto:");
    int quantita = int.Parse(Console.ReadLine());

    // sto creando un nuovo dizionario per il prodotto che avrà le seguenti proprietà:
    var prodotto = new Dictionary<string, object>
   {
        //MIGLIORAMENTO: NOME SIGNIFICATIVO PER QUESTA VARIABILE 'idProdotto'
        {"ID" , id},
        {"Nome", nomeProdotto},
        {"Prezzo",prezzoProdotto},
        //Quantita indica la quantità del prodotto che il dipendente può aggiungere
        // MIGLIORAMENTO: NOME SIGNIFICATIVO PER QUESTA VARIABILE 'quantitaProdotto'
        {"Quantita", quantita},
        // MIGLIORAMENTO: NOME SIGNIFICATIVO PER QUESTA VARIABILE COME 'giacenzaProdotto'
        {"QuantitaDisponibile",quantita}
   };
    catalogo.Add(prodotto);
    Console.WriteLine("Il prodotto è stato aggiunto al catalogo.");
}

List<Dictionary<string, object>> CaricaCatalogoDalFile(string catalogoFile)
{
    string filePathD = File.ReadAllText(catalogoFile);

    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(filePathD);
    // deserializzazione:
    // 1 - leggo i caratteri del file json (string stringaDeserializzata = File.ReadAllText(fileDaLeggere))
    //      e salvo in una variabile stringa
    // 2 - deserializzo, dicendo in che formato voglio che sia acquisito
    //      il contenuto del file json (JsonConvert.DeserializeObject<qualunque tipo di dato>(stringa per lettura))


    // per deserializzare dobbiamo prima leggere il file json
    // e acquisire il dato in una stringa 
}

void SalvaCatalogoSuFile(List<Dictionary<string, object>> catalogo, string filePath)
{
    string filePathS = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
    File.WriteAllText(filePath, filePathS);

    // serializzazione
    // 1 - creiamo una variabile stringa =  JsonConvert.SerializeObject(oggetto da serializzare, Formatting.Indented);
    //      dove tra parentesi c'è la cosa da serializzare, Formatting.Indented
    // 2 -  scrivo il file: File.WriteAllText(percorso, stringa serializzata); 

}
void VisualizzaCatalogo(List<Dictionary<string, object>> catalogo)
{
    foreach (var prodottiCatalogo in catalogo)
    {
        Console.WriteLine($"{prodottiCatalogo.Keys}, {prodottiCatalogo.Values}");
    }
}
void ModificaPrezzo(List<Dictionary<string, object>> catalogo)
{
    // chiedere all'utente di quale prodotto modificare il prezzo
    // e salvare in una variabile
    Console.WriteLine("Di quale prodotto vuoi modificare il prezzo?");
    string prodottoDaModificare = Console.ReadLine();
    // se il catalogo contiene il prodotto inserito dall'utente 
    // chiedi il nuovo prezzo da inserire

    // se non lo contiene comunica che il prodotto non è stato trovato

    foreach (var prodotto in catalogo)
    {
        if (prodotto.ContainsKey(prodottoDaModificare))
        {
            Console.WriteLine("Inserisci il nuovo prezzo:");
            decimal nuovoPrezzo = decimal.Parse(Console.ReadLine());
            // "catalogo" è una lista di dizionari
            // "prodotto" è un dizionario 
            // per scrivere dentro il dizionario dobbiamo dargli una chiave
            // siccome abbiamo controllato che il prodotto contiene la chiave (nell'if)
            // significa che abbiamo trovato il prodotto, quindi abbiamo la chiave
            // dunque per modificare il prodotto, gli diciamo
            prodotto[prodottoDaModificare] = nuovoPrezzo;
        }
        else
        {
            Console.WriteLine("Il prodotto non è stato trovato");
        }
    }
}
void RimuoviDalCatalogo(List<Dictionary<string, object>> catalogo)
{
    Console.WriteLine("Quale prodotto vuoi eliminare");
    string prodottoDaEliminare = Console.ReadLine();
    foreach (var prodotto in catalogo)
    {
        if (prodotto.ContainsKey(prodottoDaEliminare))
        {
            prodotto.Remove(prodottoDaEliminare);
        }
    }
}
// funzioni del cliente
void AggiungiAlCarrello(List<Dictionary<string, object>> catalogo, List<Dictionary<string, object>> carrello)
{
    Console.Write("Scrivi il prodotto da ggiungere:");
    string prodottoDaAggiungere = Console.ReadLine();
    foreach (var prodotto in catalogo)
    {
        // se la chiave del prodotto 
        if (prodotto.ContainsKey(prodottoDaAggiungere))
        {
            carrello.Add(prodotto);
            Console.WriteLine("Il prodotto è stato aggiunto al tuo carrello.");
        }

    }

}
void RimuoviProdottoDaCarrello(List<Dictionary<string, object>> carrello)
{
    Console.WriteLine("Quale prodotto vuoi rimuovere?");
    string prodottoDaRimuovere = Console.ReadLine();
    foreach (var prodotto in carrello)
    {
        if (prodotto.ContainsKey(prodottoDaRimuovere))
        {
            carrello.Remove(prodotto);
            Console.WriteLine("Il prodotto è stato eliminato");
        }
    }
}
void VisualizzaCarrello(List<Dictionary<string, object>> carrello)
{
    foreach (var prodotto in carrello)
        Console.WriteLine(carrello);
}



// string jsontestoD = File.ReadAllText(path);
//     return JsonConvert.DeserializeObject<List<Dictionary<string,object>>>(jsontestoD);
//  string jsonTestoS = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
//     File.WriteAllText(filePath, jsonTestoS);
// }