
public class Cliente
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public List<Prodotto> Carrello { get; set; }
    public List<List<Prodotto>> StoricoAcquisti { get; set; }
    public int PercentualeSconto { get; set; }
    public decimal Credito { get; set; }
}
