using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Server
{
    private TcpListener listener;
    private List<TcpClient> clients = new List<TcpClient>(); // Mantiene una lista dei client connessi
    private object clientLock = new object(); // Oggetto per la sincronizzazione

    public void StartServer(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Server avviato sulla porta {port}...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            lock (clientLock)
            {
                clients.Add(client); // Aggiunge il client alla lista in modo sicuro
            }
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }

    private void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        try
        {
            int byteCount;
            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
                Console.WriteLine($"Messaggio ricevuto: {message}");
                Broadcast(message, client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore con il client: {ex.Message}");
        }
        finally
        {
            lock (clientLock)
            {
                clients.Remove(client); // Rimuove il client dalla lista quando si disconnette
            }
            client.Close();
        }
    }

    private void Broadcast(string message, TcpClient sender)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(message);

        lock (clientLock)
        {
            foreach (TcpClient client in clients)
            {
                if (client != sender) // Non inviamo il messaggio al mittente
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch
                    {
                        // Se il client non è più valido, lo rimuoviamo
                        clients.Remove(client);
                    }
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        Server server = new Server();
        server.StartServer(3000);
    }
}