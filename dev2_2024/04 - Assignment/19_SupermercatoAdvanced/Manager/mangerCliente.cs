

public class ClienteManager // i manager gestiscono i CRUD 

{
    private int prossimoId;
    public List<Cliente> clienti;
    public ClienteRepository repositoryCliente; 
    public Cliente nuovoCliente;
  
    
  

    public ClienteManager(List<Cliente> Clienti)
    {
        clienti = Clienti;
        repositoryCliente = new ClienteRepository(); 
        prossimoId = 1;
        foreach (var cliente in clienti)
        {
            if (cliente.Id >= prossimoId)
            {
                prossimoId = cliente.Id + 1;
            }
        }
    }

    // metodo per aggiungere un cliente alla lista
    public void AggiungiCliente(Cliente cliente)
    { //assegna automaticamente un ID univoco
        cliente.Id = prossimoId;
        //incrementa il prossimo ID per il prossim cliente
        prossimoId++;
        clienti.Add(cliente);
        Console.WriteLine($"Cliente aggiunto con ID: {cliente.Id}");
    }

    // metodo per visualizzare la lista clienti
    public List<Cliente> OttieniClienti()
    {
        return clienti;
    }
    
   public void StampaClientiIncolonnati()
    {
        // Intestazioni con larghezza fissa
        Console.WriteLine(
            $"{"ID",-5} {"UserName",-20} {"% Sconto",-20} {"Credito"}"
        );
        Console.WriteLine(new string('-', 50)); // Linea separatrice

        // Stampa ogni cliente con larghezza fissa
        foreach (var cliente in clienti)
        {
            Console.WriteLine(
            $"{cliente.Id,-5} {cliente.UserName,-20} {cliente.PercentualeSconto, -20} {cliente.Credito}"
            );
        }
    }

    public Cliente TrovaCliente(int id)
    {
        foreach (var cliente in clienti)
        {
            if (cliente.Id == id)
            {
                return cliente; 
            }
        }
        return null;
    }
     public Cliente TrovaClienteNome(string userName)
    {
        foreach (var cliente in clienti)
        {
            if (cliente.UserName == userName)
            {
                return cliente; 
            }
        }
        return null;
    }

    public void AggiornaCliente(int id, Cliente nuovoCliente)
    {
        var cliente = TrovaCliente(id);
        if (cliente != null)
        {
            
            cliente.UserName = InputManager.LeggiStringa("Inserisci nuovo UserName: ");
            cliente.StoricoAcquisti = nuovoCliente.StoricoAcquisti;
            cliente.PercentualeSconto = nuovoCliente.PercentualeSconto;
            cliente.Credito = InputManager.LeggiDecimale(" Inserisci nuovo credito: ");
        }
    }

    public void EliminaCliente(int id)
    {
        var cliente = TrovaCliente(id);
        if (cliente != null)
        {
            clienti.Remove(cliente);
            //elimina il file json corrispondente al  cliente
            string filePath = Path.Combine("Data/Cliente", $"{id}.json");
            File.Delete(filePath);
            Console.WriteLine($"Cliente eliminato: {filePath}");
        }
    }

}



   