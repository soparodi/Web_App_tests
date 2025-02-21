using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Client
{
    private TcpClient client;
    private NetworkStream stream;
    private object lockObject = new object(); // Lock per la sincronizzazione

    public void StartClient(string serverIP, int port)
    {
        client = new TcpClient(serverIP, port);
        stream = client.GetStream();

        Console.WriteLine("Connesso al server.");

        // Avvia un thread separato per ricevere i messaggi dal server
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();

        SendMessages();
    }

    private void SendMessages()
    {
        string messageToSend;

        while (true)
        {
            messageToSend = Console.ReadLine(); // Legge l'input dell'utente

            if (string.IsNullOrEmpty(messageToSend)) // Se l'utente non inserisce nulla, chiude la connessione
            {
                CloseConnection();
                break;
            }

            byte[] buffer = Encoding.ASCII.GetBytes(messageToSend);

            lock (lockObject) // Evita scritture simultanee
            {
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }

    private void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int byteCount;

        while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
            Console.WriteLine($"Messaggio ricevuto dal server: {message}");
        }

        CloseConnection(); // Se il server si disconnette, chiude la connessione
    }

    private void CloseConnection()
    {
        if (client != null)
        {
            client.Close();
            Console.WriteLine("Connessione chiusa.");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Inserisci l'IP del server:");
        string serverIP = Console.ReadLine();

        Client client = new Client();
        client.StartClient(serverIP, 3000);
    }
}