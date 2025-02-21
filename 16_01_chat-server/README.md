# I comandi di rete
## Il comando `netstat`

```csharp

using System.Net.Sockets; // using che da le funzionalità per la comunicazione in rete
using System.Text; // using che dà le funzionalità per la gestione delle stringhe che vengono inviate e ricevute

public class Client
{
    // metodo per stabilire la connessione con il server
    void StartClient(string serverIP, int port) // gli argomenti sono l'indirizzo IP del server e la porta su cui il server è in ascolto
    {
        using (var client = new TcpClient(serverIP, port)) // TcpClient è la classe che rappresenta un client TCP cioè un client che si connette ad un server tramite il protocollo TCP
        using (var stream = client.GetStream()) // GetStream restituisce un oggetto NetworkStream che rappresenta il flusso di dati tra il client e il server
        {
            Console.WriteLine("Connesso al server.");
            string messageToSend = Console.ReadLine();

            while (!string.IsNullOrEmpty(messageToSend))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(messageToSend); // Converte la stringa in un array di byte
                stream.Write(buffer, 0, buffer.Lenght);

                messageToSend = Console.ReadLine(); // Legge il messaggio da inviare
            }
        }
    }

    public static void Main(string[] args)
    {
        Client client = new Client(); // Crea un'istanza della classe Client in modo da poter chiamare il metodo StartClient
        Console.WriteLine("Inserisci l'IP del server:"); // Chiede all'utente di inserire l'IP del server
        string serverIP = Console.ReadLine(); // Legge l'IP del server inserito dall'utente
        client.StartClient(serverIP, 3000); // Avvia il client con l'IP del server e la porta 3000
    }
}

```

# 02_Server

```csharp

using System.Net.Sockets; // using che dà le funzionalità per la comunicazione in rete
using System.Text; // using che dà le funzionalità per la gestione delle stringhe che vengono inviate e ricevute
using System.Threading; // using che dà le funzionalità per la gestione del thread, cioè dei flussi di esecuzione separati

public class Server
{
    private TcpListener listener; // Oggetto che rappresenta un server TCP
    public void StartServer(int port)
    {
        listener = new TcpListener(IPAddress.Any, port); // IPAddress.Any indica che il server accetta connessioni su tutte le interfacce di rete
        listener.Start(); // Avvia il server

        while(true)
        {
            TcpClient client = listener.AcceptTcpClient(); // Accetta una connessione da un client e restituisce un oggetto TcpClient che rappresenta il client connesso
            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient)); // Il delegato è new ParameterizedThreadStart, new è il costruttore del delegato che ha come argomento HandleCliente, crea un nuovo thread (= canale di trasmissione) per gestire il client connesso - metodo che come argomento un metodo che ha come argomento un metodo (matrioska)
            clientThread.Start(client); // Avvia il thread per gestire il client connesso in questo caso thread
        }
    }

    private void HandleClient(object obj) // accetta solo argomenti di tipo object
    {
        TcpClient client = (TcpClient)obj; // Converte l'oggetto, passato come argomento, in un oggetto TcpClient
        NetworkStream stream = client.GetStream(); // Ottiene il flusso di dati tra il client e il server networkstream
        byte[] buffer = new byte[1024]; // gestisce un messaggio di 1024 caratteri - 1 byte = 8 bit

        int byteCount;

        while ((byteCount = stream.Read(buffer, 0, buffer.Lenght)) != 0)
        {
            string message = Encoding.ASCII.GetString(buffer, 0, byteCount); // Converte i byte ricevuti in una stringa
            Console.WriteLine($"Messaggio ricevuto: {message}");
            Broadcast(message); // reinviamo il messaggio a tutti i client connessi
        }
        client.Close();
    }

    // Il metodo Broadcast si occupa di inviare il messaggio a tutti i client connessi
    private void Broadcast(string message)
    {
        foreach (TcpClient client in clients) // Per ogni client connesso
        {
            NetworkStream stream = client.GetStream(); // Ottiene il flusso di dati tra il client e il server
            byte[] buffer = Encoding.ASCII.GetBytes(message); // Converte la stringa in un array di byte
            stream.Write(buffer, 0, buffer.Lenght); // Invia il messaggio al client
        }
    }

    public static void Main(string[] args) // Metodo Main che viene eseguito all'avvio del programma
    {
        Server server = new Server(); // Crea un'istanza della classe Server
        server.StartServer(3000); // Avvia il server sulla porta 3000
    }
}

```