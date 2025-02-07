using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
public class DettaglioProdottoModel : PageModel
{
    private readonly ILogger<DettaglioProdottoModel> _logger;
    public DettaglioProdottoModel(ILogger<DettaglioProdottoModel> logger)
    {
        _logger = logger;
        var cultureInfo = CultureInfo.CurrentCulture;
    }
    public Prodotto Prodotto { get; set; }
    public void OnGet(Prodotto prodotto) //sarebbe più giusto mettere i vari argomenti qui mentre 
                                         //intero solo se vogliamo prendere più campi (che mi servono)
    {
        Prodotto = prodotto  ; // var non e necessario in quanto il tipo e gia specificato nel metodo
    } 
}