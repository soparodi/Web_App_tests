
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {
        // Creare un oggetto di tipo ProdottoRepository per gestire il salvataggio e il caricamento dei dati
        ProdottoRepository repository = new ProdottoRepository();
        DipendenteRepository repositoryDip = new DipendenteRepository();
        ClienteRepository repositoryCliente = new ClienteRepository();
        CarrelloRepository repositoryCarrello = new CarrelloRepository();
        PurchaseRepository repositoryPurchase = new PurchaseRepository();
        CategoriaRepository repositoryCategoria = new CategoriaRepository();
        // Caricare i dati da file con il metodo CaricaProdotti della classe ProdottoRepository (repository)
        List<Prodotto> prodotti = repository.CaricaProdotti();
        List<Dipendente> listaDipendenti = repositoryDip.CaricaDipendenti();
        List<Cliente> listaClienti = repositoryCliente.CaricaClienti();
        List<Prodotto> prodottoCarrello = repositoryCarrello.CaricaCarrello();
        List<Purchase> purchase = repositoryPurchase.CaricaPurchase();
        List<Categoria> categoria = repositoryCategoria.CaricaCategoria();
        // Creare un oggetto di tipo ProdottoAdvancedManager per gestire i prodotti
        ProdottoManager manager = new ProdottoManager(prodotti);
        DipendenteManager managerDip = new DipendenteManager(listaDipendenti);
        ClienteManager managerCliente = new ClienteManager(listaClienti);
        CarrelloManager managerCarrello = new CarrelloManager(prodottoCarrello);
        Cliente clienteSingolo = repositoryCliente.CaricaClienteSingolo();
        ManagerPurchase managerPurchase = new ManagerPurchase(purchase);
        CategoriaManager managerCategoria = new CategoriaManager(categoria);

        // Menu interattivo per eseguire operazioni CRUD sui prodotti

        // variabile per controllare se il programma deve continuare o uscire
        bool continua = true;
        bool continuaMagazziniere = true;
        bool continuaCliente = true;
        bool continuaAmministratore = true;
        bool continuaCassiere = true;
        // il ciclo while continua finché la variabile continua è true
        while (continua)
        {
            Console.WriteLine("\n--- Benvenuto al Supermercato Json ---");

            Console.WriteLine("\nIdentificati:");
            Console.WriteLine("1. Magazziniere");
            Console.WriteLine("2. Cliente");
            Console.WriteLine("3. Cassiere");
            Console.WriteLine("4. Amministratore");

            string identificazione = InputManager.LeggiIntero("Scelta: ", 1, 4).ToString();
            //pulisco la console
            Console.Clear();

            if (identificazione == "1")
            {
                while (continuaMagazziniere)
                {
                    Console.WriteLine("\n --- Menu magazziniere ---");
                    Console.WriteLine("1. Visualizza Prodotti");
                    Console.WriteLine("2. Aggiungi Prodotto");
                    Console.WriteLine("3. Trova Prodotto per ID");
                    Console.WriteLine("4. Aggiorna Prodotto");
                    Console.WriteLine("5. Elimina Prodotto");
                    //Console.WriteLine("6. Sezione Categorie");
                    Console.WriteLine("0. Esci");
                    string sceltaDipendente = InputManager.LeggiIntero("Scelta: ", 0, 7).ToString();
                    bool continuaCategorie = false;
                    Console.Clear();
                    switch (sceltaDipendente)
                    {
                        case "1":
                            Console.WriteLine("\nProdotti: ");
                            //string scelta = Console.ReadLine();
                            //string scelta = acquisita mediante il metodo LeggiInteri della classe InputManager
                            //string scelta = InputManager.LeggiIntero();
                            // Visualizzare i prodotti con il metodo OttieniProdotti della classe ProdottoAdvancedManager (manager)
                            manager.StampaProdottiIncolonnati();
                            break;
                        case "2": //aggiungo il prodotto (e categoria)
                            //acquisisco il nome mediante il metodo LeggiStringa della classe InputManager
                            string nome = InputManager.LeggiStringa("\nNome: ");
                            //acquisisco il prezzo mediante il metodo LeggiDecimale della classe InputManager
                            decimal prezzo = InputManager.LeggiDecimale("\nPrezzo: ");
                            //acuqisisco giacenza mediante il metodo LeggiIntero della classe InputManager
                            int giacenza = InputManager.LeggiIntero("\nGiacenza: ");
                            string nomeCategoria = InputManager.LeggiStringa("\nCategoria: ");
                            manager.AggiungiProdotto(new Prodotto { Nome = nome, Prezzo = prezzo, Giacenza = giacenza, Categoria = nomeCategoria });
                            break;
                        case "3": // trovo prodotto 
                            Console.Write("ID: ");
                            int idProdotto = InputManager.LeggiIntero("\n");
                            Prodotto prodottoTrovato = manager.TrovaProdotto(idProdotto);
                            if (prodottoTrovato != null)
                            {
                                Console.WriteLine($"\nProdotto trovato per ID {idProdotto}: {prodottoTrovato.Nome}");
                            }
                            else
                            {
                                Console.WriteLine($"\nProdotto non trovato per ID {idProdotto}");
                            }
                            break;
                        case "4": // aggiorno prodotto ( e categoria )
                            int idProdottoDaAggiornare = InputManager.LeggiIntero("\nID: ");
                            string nomeNuovo = InputManager.LeggiStringa("\nNome: ");
                            decimal prezzoNuovo = InputManager.LeggiDecimale("\nPrezzo: ");
                            Console.Write("Giacenza: ");

                            int giacenzaNuova = int.Parse(Console.ReadLine());
                            manager.AggiornaProdotto(idProdottoDaAggiornare, new Prodotto { Nome = nomeNuovo, Prezzo = prezzoNuovo, Giacenza = giacenzaNuova });
                            //
                            break;
                        case "5":
                            int idProdottoDaEliminare = InputManager.LeggiIntero("\nID: ");
                            manager.EliminaProdotto(idProdottoDaEliminare);
                            break;

                            //case "6":
                            Console.WriteLine("Vuoi accedere alla sezione Categorie? s/n");
                            string accessoCategorie = Console.ReadLine().ToLower();
                            if (accessoCategorie == "s")
                            {
                                continuaCategorie = true;
                                Console.WriteLine("1. Visualizza Categorie");
                                Console.WriteLine("2. Aggiungi Categoria");
                                Console.WriteLine("3. Trova Categoria");
                                Console.WriteLine("4. Aggiorna Categoria");
                                Console.WriteLine("5. Elimina Categoria");
                                Console.WriteLine("0. Esci");
                                string sceltaOpzioneCategoria = InputManager.LeggiIntero("\nScelta: ").ToString();
                                switch (sceltaOpzioneCategoria)
                                {
                                    case "1":
                                    managerCategoria.OttieniCategoria();
                                    break;

                                    case "2":
                                    nomeCategoria = InputManager.LeggiStringa("\nNome Categoria: ");
                            //acquisisco il prezzo mediante il metodo LeggiDecimale della classe InputManager
                            
                            int Id = InputManager.LeggiIntero("\nId Categoria: ");
                            
                            managerCategoria.AggiungiCategoria(new Categoria { NomeCategoria = nomeCategoria, Id = Id });
                                    break;

                                    case "3":
                                    break;

                                    case "4":
                                    break;

                                    case "5":
                                    break;

                                    case "0":
                                    break;
                                }
                            }

                        case "0":
                            repository.SalvaProdotti(manager.OttieniProdotti());
                            Console.WriteLine("Vuoi uscire dal programma? s/n");
                            string rispostaMagazzieniere = Console.ReadLine().ToLower();
                            if (rispostaMagazzieniere == "n")
                            {
                                continuaMagazziniere = false;
                            }
                            else
                            {
                                continua = false;
                                continuaMagazziniere = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Scelta non valida. Riprovare.");
                            break;
                    }
                }
            }

            if (identificazione == "2") // cliente
            {
                string userName = InputManager.LeggiStringa("\n UserName: ");
                bool userNameTrovato = false;
                foreach (var cliente in listaClienti)
                {
                    if (userName == cliente.UserName)
                    {
                        userNameTrovato = true;
                        clienteSingolo = cliente;
                    }
                }
                if (!userNameTrovato)
                {
                    Console.WriteLine("\n Sei un nuovo cliente!");
                    string userNameCliente = InputManager.LeggiStringa("\nAggiungi il tuo UserName: ");

                    managerCliente.AggiungiCliente(new Cliente
                    {
                        UserName = userNameCliente,
                        Carrello = new List<Prodotto>(),
                        StoricoAcquisti = new List<List<Prodotto>>(),
                        PercentualeSconto = 0,
                        Credito = 100
                    });
                    clienteSingolo = managerCliente.TrovaClienteNome(userNameCliente);
                    repositoryCliente.SalvaClienteSingolo(clienteSingolo);

                }
                while (continuaCliente)
                {
                    Console.WriteLine("\n --- Menu cliente ---");
                    Console.WriteLine("Scegli un'operazione: ");
                    Console.WriteLine("1. Visualizza il catalogo");
                    Console.WriteLine("2. Aggiungi prodotto al carrello");
                    Console.WriteLine("3. Elimina un prodotto dal carrello");
                    Console.WriteLine("4. Visualizza il carrello");
                    Console.WriteLine("5. Procedi al pagamento");
                    Console.WriteLine("0. ESCI");
                    string sceltaCliente = InputManager.LeggiIntero("Scelta: ", 0, 5).ToString();
                    switch (sceltaCliente)
                    {
                        case "1":
                            Console.WriteLine("\nCatalogo: ");
                            manager.StampaProdottiCliente();
                            break;
                        case "2":
                            manager.StampaProdottiCliente();
                            string nome = InputManager.LeggiStringa("\nNome: ");
                            int quantita = InputManager.LeggiIntero("\nQuantita: ");

                            foreach (var prodotto in manager.OttieniProdotti())
                            {
                                if (prodotto.Nome == nome)
                                {
                                    prodotto.Giacenza -= quantita; //decremento la quantita del prodotto dalla giacenza del magazzino
                                    prodotto.Quantita = quantita; //salvo la nuova quantita del magazzino
                                    clienteSingolo.Carrello.Add(prodotto); // aggiungo il prodotto alla lista del carrello del singolo cliente
                                    listaClienti.Add(clienteSingolo);
                                    repositoryCliente.SalvaClienti(listaClienti);// salvo la lista dei clienti nel repositoryCliente
                                    repository.SalvaProdotti(manager.OttieniProdotti());// salvo la lista dei prodotti nel repository dei prodotti
                                }
                            }
                            break;
                        case "3": //elimina prodotto dal carrello
                            managerCarrello.VisualizzaCarrello(clienteSingolo.Carrello); //il cliente visualizza prima il suo carrello
                            string nomeProdottoDaEliminare = InputManager.LeggiStringa("\n Quale prodotto vuoi eliminare?: ");
                            bool trovato = false;// creo la variabile per il controllo dell'esistenza del prodotto in 
                                                 // modo che possa diventare true se si trova
                            var prodottoDaEliminare = new Prodotto();
                            int quantitaEliminata = 0;
                            int quantitaDaEliminare = 0;
                            foreach (var prodotto in clienteSingolo.Carrello)
                            {
                                if (nomeProdottoDaEliminare == prodotto.Nome)
                                {
                                    quantitaDaEliminare = InputManager.LeggiIntero("\n Inserisci la quantita da eliminare:", 0, prodotto.Quantita);// ho inserito il numero massimo possibile da eliminare
                                    quantitaEliminata = quantitaDaEliminare;// devo prima salvare la quantita da reinserire nella giacenza 
                                                                            //del magazzino
                                    prodottoDaEliminare = prodotto;
                                    trovato = true;
                                }
                            }
                            if (trovato)
                            {
                                if (prodottoDaEliminare.Quantita == quantitaDaEliminare)
                                {
                                    clienteSingolo.Carrello.Remove(prodottoDaEliminare);//rimuovere il prodotto dal carrello
                                }
                                else
                                {
                                    clienteSingolo.Carrello.Remove(prodottoDaEliminare); //rimuovere il prodotto dal carrello
                                    prodottoDaEliminare.Quantita -= quantitaDaEliminare; //rimuove la quantità da eliminare dalla quantità 
                                                                                         //del prodotto
                                    prodottoDaEliminare.Giacenza += quantitaDaEliminare;
                                    clienteSingolo.Carrello.Add(prodottoDaEliminare); //riaggiunge il prodotto con la nuova quantità al carrello
                                }

                                repositoryCliente.SalvaClienteSingolo(clienteSingolo);
                                foreach (var prodotto in manager.OttieniProdotti())
                                {
                                    if (prodotto.Id == prodottoDaEliminare.Id)
                                    {
                                        repository.SalvaProdotti(manager.OttieniProdotti()); //salvare sia il  repository che quello del
                                                                                             //magazzino per aggiornare i file.
                                    }
                                }
                            }

                            break;

                        case "4":
                            managerCarrello.VisualizzaCarrello(clienteSingolo.Carrello);
                            Console.WriteLine($"\nTotale: € {managerCarrello.CalcoloTotale(clienteSingolo)}");
                            break;

                        case "5": //procedi al pagamento
                            decimal salvaTotale = managerCarrello.CalcoloTotale(clienteSingolo);
                            Console.WriteLine($"\n Il totale da pagare è di € {managerCarrello.CalcoloTotale(clienteSingolo)}");
                            bool procediAlPagamento = InputManager.LeggiConferma("Vuoi procedere al pagamento?");
                            if (procediAlPagamento)
                            {
                                if (salvaTotale > clienteSingolo.Credito)
                                {
                                    Console.WriteLine("Credito non sufficiente.");
                                }
                                else
                                {
                                    clienteSingolo.Credito -= salvaTotale;
                                    DateTime dataAcquisto = DateTime.Now;
                                    string dataAcquistoStringa = dataAcquisto.ToString();
                                    Purchase nuovoPurchase = new Purchase
                                    {
                                        ProdottoPurchase = clienteSingolo.Carrello,
                                        Data = dataAcquistoStringa,
                                        Stato = true,
                                        Totale = salvaTotale,
                                        IdCliente = clienteSingolo.Id,
                                        NomeCliente = clienteSingolo.UserName
                                    };
                                    purchase.Add(nuovoPurchase);
                                    managerPurchase.AggiungiPurchase(nuovoPurchase);
                                    repositoryPurchase.SalvaPurchase(purchase);
                                    foreach (var prodotto in clienteSingolo.Carrello)
                                    {
                                        Console.WriteLine(
                                         $"{prodotto.Nome,-20} {prodotto.Prezzo,-10} {prodotto.Quantita,-10}"
                                         );
                                        Console.WriteLine(new string('-', 50)); // Linea separatrice
                                                                                //Console.WriteLine($"\n {prodotto.Nome} {prodotto.Prezzo}");

                                    }
                                    Console.WriteLine($"\n Totale € {salvaTotale.ToString("F2")}"); // mostra due cifre dopo la virgola
                                    Console.WriteLine($"Grazie e arrivederci! \n{dataAcquistoStringa} ");

                                    dataAcquistoStringa = DateTime.Now.ToString("dd/MM/yyyy");
                                    foreach (var item in clienteSingolo.Carrello)
                                    {
                                        item.Data = dataAcquistoStringa;
                                    }
                                    clienteSingolo.StoricoAcquisti.Add(clienteSingolo.Carrello);
                                    clienteSingolo.Carrello = new List<Prodotto>();
                                    repositoryCliente.SalvaClienteSingolo(clienteSingolo);
                                }
                            }
                            break;

                        case "0":
                            Console.WriteLine("Vuoi uscire dal programma? s/n");
                            string rispostaCliente = Console.ReadLine().ToLower();
                            if (rispostaCliente == "n")
                            {
                                continuaCliente = false;
                            }
                            else
                            {
                                continua = false;
                                continuaCliente = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Scelta non valida. Riprovare.");
                            break;
                    }
                }
            }
            if (identificazione == "3")
            {
                while (continuaCassiere)
                {
                    Console.WriteLine("\n--- Menu cassiere ---");
                    Console.WriteLine("1. Visualizza carrello del cliente");
                    Console.WriteLine("2. Aggiungi prodotti");
                    Console.WriteLine("3. Elimina prodotti");
                    Console.WriteLine("4. Visualizza elementi da processare");
                    Console.WriteLine("0. ESCI");
                    string sceltaCassiere = InputManager.LeggiIntero("Scelta: ", 0, 4).ToString();
                    switch (sceltaCassiere)
                    {
                        case "1": //visualizza il carrello del cliente
                            managerCarrello.VisualizzaCarrello(clienteSingolo.Carrello);
                            break;

                        case "2": //aggiunge il prodotto al carrell del cliente
                            manager.StampaProdottiCliente();
                            string nome = InputManager.LeggiStringa("\nNome: ");
                            int quantita = InputManager.LeggiIntero("\nQuantita: ");
                            foreach (var prodotto in manager.OttieniProdotti())
                            {
                                if (prodotto.Nome == nome)
                                {
                                    prodotto.Giacenza -= quantita; //decremento la quantita del prodotto dalla giacenza del magazzino
                                    prodotto.Quantita = quantita; //salvo la nuova quantita del magazzino
                                    clienteSingolo.Carrello.Add(prodotto); // aggiungo il prodotto alla lista del carrello del singolo cliente
                                    listaClienti.Add(clienteSingolo);
                                    repositoryCliente.SalvaClienti(listaClienti);// salvo la lista dei clienti nel repositoryCliente
                                    repository.SalvaProdotti(manager.OttieniProdotti());// salvo la lista dei prodotti nel repository dei prodotti
                                }
                            }
                            break;
                        case "3": //elimina prodotto dal carrello del cliente
                            managerCarrello.VisualizzaCarrello(clienteSingolo.Carrello); //il cassiere visualizza prima il carrello
                            string nomeProdottoDaEliminare = InputManager.LeggiStringa("\n Quale prodotto vuoi eliminare?: ");
                            bool trovato = false;
                            var prodottoDaEliminare = new Prodotto();
                            int quantitaEliminata = 0;
                            foreach (var prodotto in clienteSingolo.Carrello)
                            {
                                if (nomeProdottoDaEliminare == prodotto.Nome)
                                {
                                    quantitaEliminata = prodotto.Quantita;// devo prima salvare la quantita da reinserire nella giacenza 
                                                                          //del magazzino
                                    prodottoDaEliminare = prodotto;

                                    trovato = true;
                                }
                            }
                            if (trovato)
                            {
                                clienteSingolo.Carrello.Remove(prodottoDaEliminare);//rimuovere il prodotto dal carrello
                                repositoryCliente.SalvaClienteSingolo(clienteSingolo);
                                foreach (var prodotto in manager.OttieniProdotti())
                                {
                                    if (prodotto.Id == prodottoDaEliminare.Id)
                                    {
                                        prodotto.Giacenza += quantitaEliminata; //sommare la quantita alla giacenza nel magazzino
                                        repository.SalvaProdotti(manager.OttieniProdotti()); //salvare sia il  repository che quello del
                                                                                             //magazzino per aggiornare i file.
                                    }
                                }
                            }
                            break;
                        case "4":

                            break;

                        case "0":
                            Console.WriteLine("Vuoi uscire dal programma? s/n");
                            string rispostaCassiere = Console.ReadLine().ToLower();
                            if (rispostaCassiere == "n")
                            {
                                continuaCassiere = false;
                            }
                            else
                            {
                                continua = false;
                                continuaCassiere = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Scelta non valida. Riprovare.");
                            break;
                    }
                }
            }
            if (identificazione == "4")
            {
                while (continuaAmministratore)
                {
                    Console.WriteLine("\n--- Menu Amministratore ---");
                    Console.WriteLine("1. Visualizza dipendenti");
                    Console.WriteLine("2. Imposta ruolo dei dipendenti tramite ID");
                    Console.WriteLine("3. Aggiungi dipendente");
                    Console.WriteLine("4. Elimina dipendente");
                    Console.WriteLine("5. Visualizza cliente");
                    Console.WriteLine("6. Elimina cliente");
                    Console.WriteLine("0. Salva ed esci");
                    string sceltaAmministratore = InputManager.LeggiIntero("Scelta :", 0, 11).ToString();
                    switch (sceltaAmministratore)
                    {
                        case "1":// visualizza dipendenti
                            managerDip.StampaDipendentiIncolonnati();
                            break;
                        case "2": //impostare il ruolo del dipendente tramite ID
                            int idDipendenteRuolo = InputManager.LeggiIntero("ID: ", 0);
                            managerDip.ImpostaRuolo(idDipendenteRuolo);
                            break;
                        case "3": //aggiungi dipendente
                            string userNameDip = InputManager.LeggiStringa("\nAggiungi il nome del dipendente:");
                            string ruoloDip = InputManager.LeggiStringa("\nImposta il suo ruolo:");
                            managerDip.AggiungiDipendente(new Dipendente { UserName = userNameDip, Ruolo = ruoloDip });
                            break;
                        case "4"://elimina dipendente
                            int idDipendenteDaEliminare = InputManager.LeggiIntero("ID:");
                            managerDip.EliminaDipendente(idDipendenteDaEliminare);
                            break;

                        case "5":// visualizza clienti
                            managerCliente.StampaClientiIncolonnati();
                            break;
                        case "6"://elimina cliente
                            int idClienteDaEliminare = InputManager.LeggiIntero("ID:");
                            managerCliente.EliminaCliente(idClienteDaEliminare);
                            break;
                        case "0": //salva ed esci
                            repositoryDip.SalvaDipendente(listaDipendenti);
                            repositoryCliente.SalvaClienti(listaClienti);
                            Console.WriteLine("Vuoi uscire dal programma? s/n");
                            string rispostaAmministratore = Console.ReadLine().ToLower();
                            if (rispostaAmministratore == "n")
                            {
                                continuaAmministratore = false;
                            }
                            else
                            {
                                continua = false;
                                continuaAmministratore = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Scelta non valida. Riprovare.");
                            break;
                    }
                }
            }
            //Resetto il bool del while a true altrimenti non è più accessibile al menu del dipendente che mi interessa
            continuaMagazziniere = true;
            continuaCliente = true;
            continuaCassiere = true;
            continuaAmministratore = true;
        }
    }
}
