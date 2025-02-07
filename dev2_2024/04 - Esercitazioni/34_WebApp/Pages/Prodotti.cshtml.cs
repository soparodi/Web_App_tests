using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _34_WebApp.Pages;

public class ProdottiModel : PageModel
{
    private readonly ILogger<ProdottiModel> _logger;

    public ProdottiModel(ILogger<ProdottiModel> logger)
    {
        _logger = logger;
        //_logger.LogInformation("Prodotti");
        //_logger.LogDebug("Messaggio di log");
        //_logger.LogWarning("Messaggio di log");
        //_logger.LogError("Messaggio di log");
        _logger.LogCritical(parametro);
        _logger.LogInformation("messaggio di log con {0}")
    }
    string parametro = "errore 404";
    public void OnGet()
    {
    ViewData ["messaggio"] - $"messagggio di log con {parametro} ";
    }
}

