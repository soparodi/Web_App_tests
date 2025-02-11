using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _29_WebApp_sqlite2.Pages;

public class ContattiModel : PageModel
{
    private readonly ILogger<ContattiModel> _logger;

    public ContattiModel(ILogger<ContattiModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

