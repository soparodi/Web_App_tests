using System.Net;
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

        while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
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
            stream.Write(buffer, 0, buffer.Length); // Invia il messaggio al client
        }
    }

    public static void Main(string[] args) // Metodo Main che viene eseguito all'avvio del programma
    {
        Server server = new Server(); // Crea un'istanza della classe Server
        server.StartServer(3000); // Avvia il server sulla porta 3000
    }

    private List<TcpClient> clients = new List<TcpClient>(); // Lista dei client connessi
}