
public class Purchase
{
    public int Id { get; set; }
    //public Cliente ClientePurchase { get; set; } // clienteSingolo <-- non serve perchè salvo nella lista prodottoPurchase
    public List<Prodotto> ProdottoPurchase { get; set; }
    //public int Quantita { get; set; } // è già inserito nel modello del prodotto
    public string Data { get; set; } // non uso DateTime perchè mi basta salvare la stringa della data di acquisto.
    public bool Stato { get; set; }
    public decimal Totale { get; set; }
    public int IdCliente { get; set; }
     public string NomeCliente { get; set; }
}
// inserire il cliente singolo nel program.cs.
// creare un oggetto purchase e inserire i campi che gli servono per essere popolato.
