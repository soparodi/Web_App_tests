using System.Collections.Generic;

public class Fornitore
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Prodotto> Prodotti { get; set; } = new List<Prodotto>(); // lista dei prodotti a cui è associato il fornitore
}