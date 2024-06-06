
using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace viva.webstore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IModelService _modelService;
        private readonly IArtikliService _artikleService;

    

        public  List<Data.Models.Model> Artikli_Novi { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IModelService modelService, IArtikliService artikleService)
        {
            _logger = logger;
            _modelService = modelService;
            _artikleService = artikleService;
        }

        public async Task OnGet()
        {
            var modeli_novi = await _modelService.SviModeliNovi();
            Artikli_Novi= modeli_novi;
        }
    }
}
