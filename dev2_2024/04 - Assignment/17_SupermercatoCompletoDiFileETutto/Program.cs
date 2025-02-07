
/*using Newtonsoft.Json; //libreria per gestire il file JSON

string filePath = "catalogo.json"; //percorso del file JSON
string scontriniFilePath = "scontrini.json";
int prossimoIdProdotto = 1;

//creo due liste di dizionari per il catalogo e il carrello
//i dizionari contengono le informazioni dei prodotti
//i dizionari sono composti da coppie chiave-valore
//la chiave è una stringa, il valore è un oggetto generico
//il valore è un oggetto generico per poter contenere valori di diversi tipi in questo caso stringhe,decimali, interi
var catalogo = new List<Dictionary<string, object>>(); //lista di  dizionari per il catalogo
var carrello = new List<Dictionary<string, object>>(); //lista di dizionari per il carrello

Console.WriteLine("Benvenuto al supermercato JSON");
string indentificazione = SelezioneIdentificazione();



if (indentificazione == "o")
{
    MenuOperatore(catalogo, filePath);
}
else if (indentificazione == "c")
{
    MenuCliente(catalogo, carrello, scontriniFilePath);
}


 static string SelezioneIdentificazione()
{


    Console.WriteLine("\nSei un operatore o un cliente? o/c:");
    string scelta = Console.ReadLine();

    if (scelta == "o")
    {
        return "o";

    }
    else
    {
        return "c";

    }


}

static void MenuOperatore(List<Dictionary<string, object>> catalogo, string filePath)
{
    while (true)
    {
        Console.WriteLine("\n--- Menu Operatore ---");
        Console.WriteLine("1. Aggiungi un prodotto al catalogo");
        Console.WriteLine("2. Salva il catalogo in JSON");
        Console.WriteLine("3. Carica il catalogo da JSON");
        Console.WriteLine("4. Mostra il catalogo");
        Console.WriteLine("5. Esci");

        string scelta = Console.ReadLine();

        switch (scelta)
        {
            case "1":
                catalogo.Add(CreaProdotto());
                Console.WriteLine("Prodotto aggiunto con successo!");
                break;
            case "2":
                SalvaInJson(catalogo, filePath);
                Console.WriteLine("Catalogo salvato con successo!");
                break;
            case "3":
                catalogo = CaricaDaJson(filePath);
                Console.WriteLine("Catalogo caricato con successo!");
                break;
            case "4":
                MostraCatalogo(catalogo);
                break;
            case "5":
                Console.WriteLine("Uscita dal menu addetto.");
                return;
            default:
                Console.WriteLine("Opzione non valida, riprova.");
                break;
        }
    }
}

static void MenuCliente(List<Dictionary<string, object>> catalogo, List<Dictionary<string, object>> carrello, string scontriniFilePath)
{
    while (true)
    {
        Console.WriteLine("\n--- Menu Cliente ---");
        Console.WriteLine("1. Mostra il catalogo");
        Console.WriteLine("2. Aggiungi prodotti al carrello tramite ID");
        Console.WriteLine("3. Rimuovi prodotti dal carrello tramite ID");
        Console.WriteLine("4. Visualizza carrello");
        Console.WriteLine("5. Stampa scontrino");
        Console.WriteLine("6. Esci");

        string scelta = Console.ReadLine();

        switch (scelta)
        {
            case "1":
                MostraCatalogo(catalogo);
                break;
            case "2":
                AggiungiAlCarrelloConID(catalogo, carrello);
                break;
            case "3":
                RimuoviDalCarrelloConID(carrello);
                break;
            case "4":
                VisualizzaCarrello(carrello);
                break;
            case "5":
                VisualizzaCarrello(carrello);
                SalvaScontrino(carrello, scontriniFilePath);
                carrello.Clear(); // Svuota il carrello dopo la stampa
                break;
            case "6":
                Console.WriteLine("Uscita dal menu cliente.");
                return;
            default:
                Console.WriteLine("Opzione non valida, riprova.");
                break;
        }
    }
}

static Dictionary<string, object> CreaProdotto(int prossimoIdProdotto)
{
    var prodotto = new Dictionary<string, object>();
    prodotto["ID"] = prossimoIdProdotto++;
    Console.Write("Inserisci il nome del prodotto: ");
    prodotto["Nome"] = Console.ReadLine();

    Console.Write("Inserisci il prezzo del prodotto: ");
    prodotto["Prezzo"] = decimal.Parse(Console.ReadLine());

    Console.Write("Inserisci la quantità disponibile: ");
    prodotto["Quantità"] = int.Parse(Console.ReadLine());

    return prodotto;
}

static void SalvaInJson(List<Dictionary<string, object>> catalogo, string filePath)
{
    string json = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
    File.WriteAllText(filePath, json);
}

static List<Dictionary<string, object>> CaricaDaJson(string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.WriteLine("Nessun file JSON trovato.");
        return new List<Dictionary<string, object>>();
    }
    string json = File.ReadAllText(filePath);
    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
}

static void MostraCatalogo(List<Dictionary<string, object>> catalogo)
{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto.");
        return;
    }
    Console.WriteLine("\n--- Catalogo Prodotti ---");
    foreach (var prodotto in catalogo)
    {
        Console.WriteLine($"ID: {prodotto["ID"]} - {prodotto["Nome"]} - Prezzo: €{prodotto["Prezzo"]} - Quantità: {prodotto["Quantità"]}");
    }
}

static void AggiungiAlCarrelloConID(List<Dictionary<string, object>> catalogo, List<Dictionary<string, object>> carrello)
{
    Console.Write("Inserisci l'ID del prodotto da acquistare: ");
    int id = int.Parse(Console.ReadLine());

    Dictionary<string, object> prodotto = catalogo.FirstOrDefault(p => (int)p["ID"] == id);

    if (prodotto == null)
    {
        Console.WriteLine("Prodotto non trovato.");
        return;
    }

    Console.Write("Inserisci la quantità da acquistare: ");
    int quantità = int.Parse(Console.ReadLine());

    if (quantità > Convert.ToInt32(prodotto["Quantità"]))
    {
        Console.WriteLine("Quantità non disponibile.");
        return;
    }

    prodotto["Quantità"] = Convert.ToInt32(prodotto["Quantità"]) - quantità;
    carrello.Add(new Dictionary<string, object>
        {
            { "ID", prodotto["ID"] },
            { "Nome", prodotto["Nome"] },
            { "Prezzo", prodotto["Prezzo"] },
            { "Quantità", quantità }
        });

    Console.WriteLine($"{quantità} x {prodotto["Nome"]} aggiunti al carrello.");
}

static void RimuoviDalCarrelloConID(List<Dictionary<string, object>> carrello)
{
    Console.Write("Inserisci l'ID del prodotto da rimuovere: ");
    int id = int.Parse(Console.ReadLine());

    var prodottoDaRimuovere = carrello.FirstOrDefault(p => (int)p["ID"] == id);

    if (prodottoDaRimuovere == null)
    {
        Console.WriteLine("Prodotto non trovato nel carrello.");
        return;
    }

    carrello.Remove(prodottoDaRimuovere);
    Console.WriteLine("Prodotto rimosso dal carrello.");
}

static void VisualizzaCarrello(List<Dictionary<string, object>> carrello)
{
    if (carrello.Count == 0)
    {
        Console.WriteLine("Il carrello è vuoto.");
        return;
    }

    Console.WriteLine("\n--- Carrello ---");
    decimal totale = 0;
    foreach (var prodotto in carrello)
    {
        decimal costo = decimal.Parse(prodotto["Prezzo"].ToString()) * int.Parse(prodotto["Quantità"].ToString());
        Console.WriteLine($"{prodotto["Quantità"]} x {prodotto["Nome"]} - €{costo}");
        totale += costo;
    }
    Console.WriteLine($"\nTotale: €{totale}");
}

static void SalvaScontrino(List<Dictionary<string, object>> carrello, string filePath)
{
    var scontrino = new
    {
        Data = DateTime.Now,
        Prodotti = carrello,
        Totale = carrello.Sum(p => decimal.Parse(p["Prezzo"].ToString()) * int.Parse(p["Quantità"].ToString()))
    };

    string json = JsonConvert.SerializeObject(scontrino, Formatting.Indented);
    File.WriteAllText(filePath, json);
    Console.WriteLine("Scontrino salvato con successo.");
}


while (true)
{
    Console.WriteLine("Sei un cliente o un operatore? c/o");
     indentificazione = Console.ReadLine();
    if (indentificazione == "o")
    {
        Console.WriteLine("\nScegli un'operazione:");
        Console.WriteLine("0. Aggiungi un prodotto al catalogo");
        Console.WriteLine("1. Salva il catalogo in JSON");
        Console.WriteLine("2. Carica il catalogo d JSON");
        Console.WriteLine("3. Mostra il catalogo");
        Console.WriteLine("4. Inserisci Id del prodotto:");
        Console.WriteLine("5. Aggiungi prodotti al carrello");
        Console.WriteLine("6. Visualizza carrello e stampa scontrino");
        Console.WriteLine("7. Esci");
    }
    string scelta = Console.ReadLine(); //legge l'input dell'utente

    switch (scelta)
    {
        case "0":
            var prodotto = CreaProdotto(prossimoIdProdotto); //crea un nuovo prodotto
            catalogo.Add(prodotto); //aggiunge il prodotto al catalogo, lo faccio direttamente senza salvarlo in una variabile perchè non serve
            Console.WriteLine($"Prodotto {prodotto["Nome"]} aggiunto al catalogo"); //conferma l'aggiunta
            break;

        case "1":
            SalvaInJson(catalogo, filePath);//salva il catalogo in JSON
            Console.WriteLine("Catalogo salvato in Json");//conferma il salvataggio
            break;

        case "2":
            catalogo = CaricaDaJson(filePath); // carica il catalogo da JSON
            Console.WriteLine("Catalogo caricato da Json");//conferma il caricamento
            break;

        case "3":
            inserimentoId = InserisciId()

        case "4":
            MostraCatalogo(catalogo); //mostra il catalogo
            break;

        case "5":
            AggiungiAlCarrello(catalogo, carrello); //aggiunge prodotti al carrello
            break;

        case "6":
            VisualizzaCarrello(carrello); //visualizza il carrello e stampa lo scontrino
            break;

        case "7":
            Console.WriteLine("Grazie da Supermercato JSON"); //messaggio di uscita
            return;

        default:
            Console.WriteLine("Opzione non valida, riprova"); //messaggio di errore
            break;
    }
}

// la funzione CreaProdotto permette di creare un nuovo prodotto
//non accetta parametri
//restituisce un dizionario con le informazioni del prodotto
//la funzione è accessibile a tutto il programma in quanto statica
//la funzione statica permette di accedere alle variabili globali del programma senza doverle passare come parametri
static Dictionary<string, object> CreaProdotto()
{
    var prodotto = new Dictionary<string, object>(); //crea un nuovo dizionario per il prodotto che in questo caso
    // è un oggetto generico che ha come chiave una stringa (Nome, Prezzo, Quantità) e come valore un oggetto generico che può essere di diversi tipi (stringa, decimale, intero)

    Console.Write("Inerisci il nome del prodotto:");
    // devo mettere prodotto ["Nome"] perchè il dizionario è di tipo Dictionary<string, object> e non posso usare l'operatore .
    // per accedere ai campi quindi devo usare le parentesi quadre [] ed inserire la chiave
    prodotto["Nome"] = Console.ReadLine();

    Console.Write("Inserisci il prezzo del prodotto:");
    //converte la stringa in decimale e la mette nel dizionario come valore decimale quindi devo usare [] per accedere al campo Prezzo
    prodotto["Prezzo"] = decimal.Parse(Console.ReadLine());

    Console.Write("Inserisci la quantità disponibilie:");
    prodotto["Quantità"] = int.Parse(Console.ReadLine());

    return prodotto; // restituisce il dizionario con le informazioni del prodotto
}

//la funzione SalvaInJson permette di salvare una lista di dizionari in un file Json
//accetta come parametri la lista di dizionari da salvare e il percorso del file Json
// la funzione non restituisce nulla
static void SalvaInJson(List<Dictionary<string, object>> catalogo, string filePath)
{
    string json = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
    File.WriteAllText(filePath, json);
}
//la funzione carica da Json permette di caricare una lista di dizionari da un file JSON
//accetta come parametro il percorso del file JSON
//restituisce la lista di dizionari caricata o una lista vuota se il file non esiste
static List<Dictionary<string, object>> CaricaDaJson(string filePath)
{
    if (!File.Exists(filePath)) // se il file non esiste
    {
        Console.WriteLine("Nessun file Json trovato");
        return new List<Dictionary<string, object>>(); //crea una lista vuota e la restituisce
    }
    string json = File.ReadAllText(filePath);
    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
}
//la funzione MostraCatalogo permette di cisualizzare il catalogo dei prodotti
//accetta come parametro la lista di dizionari del catalogo
//la funzione non restituisce nulla cioè non restituisce nessun valore da essere usato fuori dalla funzione
static void MostraCatalogo(List<Dictionary<string, object>> catalogo)
{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto");
        return;
    }
    Console.WriteLine("\n---Catalogo Prodotti---");
    foreach (var prodotto in catalogo)
    {
        Console.WriteLine($"{prodotto["Nome"]} - Prezzo: € {prodotto["Prezzo"]} - Quantità: {prodotto["Quantità"]}");
    }
}
//la funzione AggiungiAlCarrello permette di aggiungere prodotti al carrello 
//accetta come parametri la  lista di dizionari del catalogo e la lista di dizionari del carrello
//la funzione non restituisce nulla
static void AggiungiAlCarrello(List<Dictionary<string, object>> catalogo, List<Dictionary<string, object>> carrello)
{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto, carica prima i prodotti");
        return;//esco datta funzione se il catalogo è vuoto
    }
    Console.WriteLine("Inserisci il nome del prodotto da acquistare:");
    string nome = Console.ReadLine();


    Dictionary<string, object> prodotto = null; //inizializzo il prodotto a null in modo da poter controllare se è stato trovato
    foreach (var p in catalogo)
    {
        if (p["Nome"].ToString().Equals(nome, StringComparison.OrdinalIgnoreCase))
        //uso []per accedere al campo Nome del prodotto perchè il dizionario è di tipo Dictionary<dtring, object>
        //quindi non posso usare l'operatore . per accedere ai campi
        // StringComparison.OrdinalIgnoreCase serve per ignorare le maiuscole e minuscole
        //Equals restituisce true se le due stringhe sono uguali in quanto non possiamo usare l'operatore == per confrontare stringhe
        {
            prodotto = p; // assegno il prodotto trovato alla variabile prodotto in modo da poterlo usare fuori dal ciclo
            break; //esco dal ciclo dato che ho trovato il prodotto
        }
    }

    if (prodotto == null)
    {
        Console.WriteLine("Prodotto non trovato");
        return; //esco dalla funzione se il prodotto non è stato trovato
    }
    Console.Write("Inserisci la quantità da acquistare:");
    int quantità = int.Parse(Console.ReadLine());

    if (quantità > int.Parse(prodotto["Quantità"].ToString())) // controllo se la quantità richiesta è dispoibile nel catalogo
    {
        Console.WriteLine("Quantità non disponibile");
        return;// esco dalla funzione se la quantità non è disponibile
    }
    prodotto["Quantità"] = int.Parse(prodotto["Quantità"].ToString()) - quantità; // aggiorno la quantità disponibile nel catalogo

    carrello.Add(new Dictionary<string, object> // aggiungo la quantità al carrello con la quantità scelta
    {
        {"Nome", prodotto["Nome"]},
        {"Prezzo", prodotto["Prezzo"]},
        {"Quantità", quantità} // aggiungo lla quantità al carrello, non metto ["Quantità"] perchè la quantità nel carrello è quella che l'utente ha scelto
    });

    Console.WriteLine($"{quantità} x {prodotto["Nome"]} aggiunti al carrello");
}
//la funzione VisualizzaCarrello permette di visualizzare il carrello e stampare lo scontrino
//accetta come parametro la lista di dizionari del carrello
//la funzione non restituisce nulla
static void VisualizzaCarrello(List<Dictionary<string, object>> carrello)
{
    if (carrello.Count == 0)
    {
        Console.WriteLine("Il carrello è vuoto");
        return;
    }

    Console.WriteLine("\n--- Carrello ---");

    decimal totale = 0; //inizializzo il totale a 0 per calcolare il costo totale del carrello, 
                        //                   se non lo inizializzo il valore di default è 0 quindi posso anche non scriverlo
    foreach (var prodotto in carrello)
    {
        decimal costo = decimal.Parse(prodotto["Prezzo"].ToString()) * int.Parse(prodotto["Quantità"].ToString()); // calcolo il costo del prodotto
        Console.WriteLine($"{prodotto["Quantità"]} x {prodotto["Nome"]} - €{costo}");
        totale += costo; //aggiorno il totale con il costo del prodotto acquistato
    }
    Console.WriteLine($"\ntotale: €{totale}");
}*/

//COMMENTO TUTTI I PASSAGGI 

using Newtonsoft.Json;
string filePath = "catalogo.json";
//string fileScontrinoPath = "scontrino.json";
bool continua = true;
// Ho definito qui lo scontrino perchè mi dava problemi nalla funzione salvascontrinosufile del menu lavoratore, in questo modo entrambi i menu riescano a vedere la variabile
//List<Dictionary<string, object>> scontrino = new List<Dictionary<string, object>>();
List<Dictionary<string, object>> catalogo = new List<Dictionary<string, object>>();
List<Dictionary<string, object>> carrello = new List<Dictionary<string, object>>();
Console.WriteLine("\n--- Benvenuto al Supermercato Json ---");



catalogo = CaricaCatalogoDalFile(filePath); // abbiamo inserito qui perchè il cliente non visualizzava il catalogo

while (continua)
{
    Console.WriteLine("\nIdentificati:");
    Console.WriteLine("1. Dipendente");
    Console.WriteLine("2. Cliente");
    Console.WriteLine("3. Magazziniere");
    Console.WriteLine("4. Amministratore");
    string selezione = Console.ReadLine();


    if (selezione == "1")
    {
        Console.WriteLine("\n--- Menu Dipendente ---");
        Console.WriteLine("1. Visualizza il catalogo");
        Console.WriteLine("2. Aggiungi un prodotto al catalogo tramite ID");
        Console.WriteLine("3. Modifica il prezzo di un prodotto");
        Console.WriteLine("4. Rimuovi un prodotto dal catalogo");
        Console.WriteLine("5. Salva il catalogo sul file");
        Console.WriteLine("6. Carica il catalogo dal file");
        //Console.WriteLine("7. Salva lo scontrino sul file");
        //Console.WriteLine("8. Visualizza gli scontrini");
        Console.WriteLine("0. ESCI");
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
                RimuoviDalCatalogo();
                break;

            case "5":
                SalvaCatalogoSuFile(catalogo, filePath);

                break;

            case "6":
                catalogo = CaricaCatalogoDalFile(filePath); //  la funzione restituisce una lista di dizionari. Non funzionava perchè il dato del return non aveva una destinazione. In questo modo quindi gli abbiamo assegnato una variabile (o "destinazione")
                break;
            /*
                        case "7":
                            //SalvaScontrinoSuFile(scontrino, fileScontrinoPath);
                            break;

                        case "8":
                            //VisualizzaScontrini();
                            break;
            */
            case "0":
                Console.WriteLine("Grazie per il tuo lavoro!");
                continua = false; //abbiamo aggiunto un bool perchè non usciva mai dalle scelte
                break;
        }
    }
    if (selezione == "2")
    {
        Console.WriteLine("Scegli un'operazione:");
        Console.WriteLine("1. Visualizza il catalogo");
        Console.WriteLine("2. Aggiungi prodotto al carrello");
        Console.WriteLine("3. Elimina un prodotto dal carrello");
        Console.WriteLine("4. Visualizza il carrello");
        //Console.WriteLine("5. Vai al pagamento ");
        Console.WriteLine("0 ESCI");
        string sceltaCliente = Console.ReadLine();
        switch (sceltaCliente)
        {
            case "1":
                VisualizzaCatalogo(catalogo);
                break;

            case "2":
                AggiungiAlCarrello(catalogo, carrello);
                break;

            case "3":
                RimuoviProdottoDaCarrello();
                break;

            case "4":
                VisualizzaCarrello(carrello);
                break;

            /*case "5":
                VaiAlPagamento(carrello, fileScontrinoPath, scontrino);
                break; */

            case "0":
                Console.WriteLine("Grazie, a presto!");
                continua = false;
                break;

            default:
                Console.WriteLine("Inserisci una scelta valida");
                break;

        }
    }
    if (selezione == "3")
    {
        Console.WriteLine("\n--- Menu magazziniere ---");
        Console.WriteLine("1. Visualizza prodotti");
        Console.WriteLine("2. Aggiungi prodotti");
        Console.WriteLine("3. Aggiorna prodotti");
        Console.WriteLine("4. Elimina prodotti");
    }
    /*inserisco lo switch con le varie funzioni
    /
    /
    /
    /
    /
    */
    if (selezione == "4")
    {
        Console.WriteLine("\n--- Menu Amministratore ---");
        Console.WriteLine("1. Visualizza dipendenti tramite ID");
        Console.WriteLine("2. Imposta ruolo dei dipendenti tramite ID");
    }
    /*Inserisco lo switch con le funzioni
    /
    /
    /
    /
    /
    */

}



#region Lavoratore
static void VisualizzaCatalogo(List<Dictionary<string, object>> catalogo)
{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto.");
        return;
    }

    foreach (var prodotto in catalogo)
        Console.WriteLine($"ID: {prodotto["ID"]}, Nome: {prodotto["Nome"]}, Prezzo: €{prodotto["Prezzo"]}, Quantita: x{prodotto["Quantita"]}");
}
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

    var prodotto = new Dictionary<string, object>
   {
        {"ID" , id},
        {"Nome", nomeProdotto},
        {"Prezzo",prezzoProdotto},
        {"Quantita", quantita},
        {"QuantitaDisponibile",quantita}
   };
    catalogo.Add(prodotto);
    Console.WriteLine("Il prodotto è stato aggiunto al catalogo.");
}
static void ModificaPrezzo(List<Dictionary<string, object>> catalogo)
{

    Console.WriteLine("Inserisci ID del prodotto da modificare:");
    int idDaModificare = int.Parse(Console.ReadLine());
    bool trovato = false;

    foreach (var elemento in catalogo) // ciclo foreach perchè ricerco per ogni elemento nel catalogo il suo id
    {
        if (elemento.ContainsValue(idDaModificare)) // se il valore dell'elemento contiene l id da modifcare allora:
        {
            Console.WriteLine("Prodotto trovato! Inserire nuovo prezzo:");
            decimal prezzo = decimal.Parse(Console.ReadLine()); // 3 EURO
            elemento["Prezzo"] = prezzo; // ID 123 = 3 EURO // nella chiave "prezzo" di quell'elemento specifico, vado a sovrascrivere il nuovo prezzo

            trovato = true;
        }
    }
    if (!trovato)
    {
        Console.WriteLine("Prodotto non trovato...");
    }
}

void RimuoviDalCatalogo() // inserisci funzione visualizzacatalogo
{
    VisualizzaCatalogo(catalogo);
    int idProdottoDaEliminare = int.Parse(Console.ReadLine());
    Console.WriteLine("Inserisci l'ID del prodotto da rimuovere:");
    bool trovato = false;
    foreach (var prodotto in catalogo)
    {
        // Recupero l'ID del prodotto dal dizionario
        int id = (int)prodotto["ID"];   //potevo convertire in to.int32 DA FARSELO SPIEGARE


        if (id == idProdottoDaEliminare)
        {
            catalogo.Remove(prodotto);  // Rimuoviamo il prodotto dalla lista
            Console.WriteLine("Prodotto rimosso con successo!");
            trovato = true;
            break;  // Uscita dal ciclo una volta trovato e rimosso il prodotto
        }
    }

    if (!trovato)
    {
        Console.WriteLine("Prodotto non trovato!");
    }
}
static void SalvaCatalogoSuFile(List<Dictionary<string, object>> catalogo, string filePath)
{
    string json = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
    File.WriteAllText(filePath, json);
    Console.WriteLine("Catalogo salvato sul file");
}
static List<Dictionary<string, object>> CaricaCatalogoDalFile(string filePath)
{
    if (!File.Exists(filePath)) // se il file non esiste
    {
        Console.WriteLine("Nessun file Json trovato");
        return new List<Dictionary<string, object>>(); //crea una lista vuota e la restituisce al catalogo
    }
    // viene ignorato se si verifica la condizione sopra
    string json = File.ReadAllText(filePath);
    Console.WriteLine("Catalogo caricatu sul file");
    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
}

//deserializzazione= sto dando i pezzetti di codice a c# per farglieli comprendere
//serializzazione= sto dando l'indicazione di COME vanno scritti sul json

/*static void SalvaScontrinoSuFile(List<Dictionary<string, object>> scontrino, string fileScontrinoPath)
{
    if (scontrino == null || scontrino.Count == 0) //chiesto a chat gpt
    {
        Console.WriteLine("Scontrino vuoto. Aggiungi prodotti prima di salvarlo.");
        return;
    }
    string json = JsonConvert.SerializeObject(scontrino, Formatting.Indented);
    File.WriteAllText(fileScontrinoPath, json);
    Console.WriteLine("Scontrino salvato sul file.");
}*/
/*        //modificare entità da catalogo a scontrino
static void VisualizzaScontrini()

{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto.");
        return;
    }

    foreach (var prodotto in )
        Console.WriteLine($"ID: {prodotto["ID"]}, Nome: {prodotto["Nome"]}, Prezzo: €{prodotto["Prezzo"]}, Quantita: x{prodotto["Quantita"]}");
}
*/
#endregion

#region Cliente
static void AggiungiAlCarrello(List<Dictionary<string, object>> catalogo, List<Dictionary<string, object>> carrello)
{
    if (catalogo.Count == 0)
    {
        Console.WriteLine("Il catalogo è vuoto, carica prima i prodotti");
        return;//esco datta funzione se il catalogo è vuoto
    }
    Console.WriteLine("Inserisci il nome del prodotto da acquistare:");
    string nome = Console.ReadLine();


    Dictionary<string, object> prodotto = null; //inizializzo il prodotto a null in modo da poter controllare se è stato trovato
    foreach (var p in catalogo)
    {
        if (p["Nome"].ToString().Equals(nome, StringComparison.OrdinalIgnoreCase))

        {
            prodotto = p; // assegno il prodotto trovato alla variabile prodotto in modo da poterlo usare fuori dal ciclo
            break; //esco dal ciclo dato che ho trovato il prodotto
        }
    }

    if (prodotto == null)
    {
        Console.WriteLine("Prodotto non trovato");
        return; //esco dalla funzione se il prodotto non è stato trovato
    }
    Console.Write("Inserisci la quantità da acquistare:");
    int quantità = int.Parse(Console.ReadLine());

    if (quantità > int.Parse(prodotto["Quantita"].ToString())) // controllo se la quantità richiesta è dispoibile nel catalogo
    {
        Console.WriteLine("Quantità non disponibile");
        return;// esco dalla funzione se la quantità non è disponibile
    }
    prodotto["Quantita"] = int.Parse(prodotto["Quantita"].ToString()) - quantità; // aggiorno la quantità disponibile nel catalogo

    carrello.Add(new Dictionary<string, object> // aggiungo la quantità al carrello con la quantità scelta
    {
        {"Nome", prodotto["Nome"]},
        {"Prezzo", prodotto["Prezzo"]},
        {"Quantita", quantità}, // aggiungo lla quantità al carrello, non metto ["Quantità"] perchè la quantità nel carrello è quella che l'utente ha scelto
        {"ID", prodotto["ID"]}
    });

    Console.WriteLine($"{quantità} x {prodotto["Nome"]} aggiunti al carrello");
}
void RimuoviProdottoDaCarrello()
{

    VisualizzaCarrello(carrello);
    Console.WriteLine("Inserisci l'ID del prodotto da rimuovere:");
    // int idProdottoDaEliminare = int.Parse(Console.ReadLine()); 
    string idProdottoDaEliminare = Console.ReadLine();
    bool trovato = false;
    foreach (var prodotto in carrello)
    {
        // Recupero l'ID del prodotto dal dizionario
        //int id = (int)prodotto["ID"];   //potevo convertire in to.int32 DA FARSELO SPIEGARE
        string id = prodotto["ID"].ToString();


        if (id == idProdottoDaEliminare)
        {
            carrello.Remove(prodotto);  // Rimuoviamo il prodotto dalla lista
            Console.WriteLine("Prodotto rimosso con successo!");
            trovato = true;
            break;  // Uscita dal ciclo una volta trovato e rimosso il prodotto
        }
    }

    if (!trovato)
    {
        Console.WriteLine("Prodotto non trovato!");
    }

}
static void VisualizzaCarrello(List<Dictionary<string, object>> carrello)
{
    if (carrello.Count == 0)
    {
        Console.WriteLine("Il carrello è vuoto");
        return;
    }

    Console.WriteLine("\n--- Carrello ---");

    decimal totale = 0; //inizializzo il totale a 0 per calcolare il costo totale del carrello, 
                        //                   se non lo inizializzo il valore di default è 0 quindi posso anche non scriverlo
    foreach (var prodotto in carrello)
    {
        decimal costo = decimal.Parse(prodotto["Prezzo"].ToString()) * int.Parse(prodotto["Quantita"].ToString()); // calcolo il costo del prodotto
        Console.WriteLine($"{prodotto["ID"]} {prodotto["Quantita"]} x {prodotto["Nome"]} - €{costo}");
        totale += costo; //aggiorno il totale con il costo del prodotto acquistato
    }
    Console.WriteLine($"\ntotale: €{totale}");
}
/*static void VaiAlPagamento(List<Dictionary<string, object>> carrello, string fileScontrinoPath, List<Dictionary<string, object>> scontrino)
{
    if (carrello.Count == 0)
    {
        Console.WriteLine("Il carrello è vuoto. Aggiungi prodotti prima di procedere al pagamento.");
        return;
    }

    decimal totale = 0;
    foreach (var prodotto in carrello)
    {
        totale += (decimal)prodotto["Prezzo"] * (int)prodotto["Quantita"]; // Calcolo totale
    }

    // Mostra il totale e la data dello scontrino
    Console.WriteLine("Totale da pagare: €" + totale);
    Console.WriteLine("Premi INVIO per confermare il pagamento...");
    Console.ReadLine();
}*/

/*void VisualizzaScontrini(List<Dictionary<string, object>> scontrino)
{
if (scontrino.Count == 0)
    {
        Console.WriteLine("Scontrino vuoto");
        return;
    }

    Console.WriteLine("\n--- Scontrino---");

    decimal totale = 0; //inizializzo il totale a 0 per calcolare il costo totale del carrello, 
                        //                   se non lo inizializzo il valore di default è 0 quindi posso anche non scriverlo
    foreach (var prodotto in scontrino)
    {
        decimal costo = decimal.Parse(prodotto["Prezzo"].ToString()) * int.Parse(prodotto["Quantita"].ToString()); // calcolo il costo del prodotto
        Console.WriteLine($"{prodotto["ID"]} {prodotto["Quantita"]} x {prodotto["Nome"]} - €{costo}");
        totale += costo; //aggiorno il totale con il costo del prodotto acquistato
    }
    Console.WriteLine($"\ntotale: €{totale}");
}
    // Creo lo scontrino con data e carrello
    
    /*scontrino.Add(new Dictionary<string, object>
    {
        { "Data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
        { "Prodotti", carrello },
        { "Totale", totale }
    });

    // Serializza lo scontrino e lo salva sul file
    string json = JsonConvert.SerializeObject(scontrino, Formatting.Indented);
    File.WriteAllText(fileScontrinoPath, json);
    Console.WriteLine("Pagamento effettuato con successo! Scontrino salvato.");

    // Svuota il carrello dopo il pagamento
    carrello.Clear();
}
*/













// int risultato = 0;
// risultato = potenzaallaseconda(3);


// //--------


// void potenzaallaseconda (int n)
// {
//      Console.WriteLine(n*n);
// }

#endregion