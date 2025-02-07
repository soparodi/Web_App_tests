using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _36_WebAppProduct.Pages;

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

