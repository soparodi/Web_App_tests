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
                stream.Write(buffer, 0, buffer.Length);

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