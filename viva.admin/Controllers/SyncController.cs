using viva.admin.Models;
using viva.admin.Models.Sync;
using Data.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace viva.admin.Controllers
{
    public class SyncController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArtikliService _artikliService;

        private readonly ISyncService _syncService;

        public SyncController(ILogger<HomeController> logger, IArtikliService artikliService, ISyncService syncService)
        {
            _logger = logger;
            _artikliService = artikliService;
            _syncService = syncService;
        }
        public async Task<IActionResult> Index()
        {
            //await _syncService.SyncSlika();
            //await _syncService.SavaCoop_Sync();
            //await _syncService.PROMOSOLUTIONS_Update();
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(SyncViewModel vm)
        //{
        //    var syncGrupe = await _artikliService.SyncGrupeArtikala();
        //    var test = await _artikliService.SyncArtikala(vm.Sifra);
        //    return View(vm);
        //}

    
        public async Task<JsonResult> SyncArtikala_Promosolution(string sifra)
        {
            try
            {
                await _syncService.SyncGrupeArtikala();
                await _syncService.PROMOSOLUTIONS_Sync_Brand();
                await _syncService.PROMOSOLUTIONS_SyncArtikala(sifra);
                await _syncService.PROMOSOLUTIONS_Sync_Color();
                return Json("OK");
            }
           catch(Exception ex)
            {
                return Json("NOT OK");
            }
           
        }
        public async Task<JsonResult> SyncArtikala_Lacuna(string sifra)
        {
            try
            {
                await _syncService.Lacuna_SyncArtikala("LACUNA");
                return Json("OK");
            }
            catch (Exception ex)
            {
                return Json("NOT OK");
            }

        }
        

        public async Task<JsonResult> SyncStanja(string sifra)
        {
            try
            {
                await _syncService.SyncStanja(sifra);
        
                return Json("OK");
            }
            catch (Exception ex)
            {
                return Json("NOT OK");
            }

        }


        public async Task<IActionResult> Cenovnik()
        {
            SyncCenovnikViewModel vm = new SyncCenovnikViewModel();
            vm.TipCenovnika = "12";
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Cenovnik(SyncCenovnikViewModel vm)
        {
            var tipovi = await _syncService.SyncTipoveCenovnika();
            var cenovnik = await _syncService.SyncCenovnika(vm.TipCenovnika);
            return View(vm);
        }
    }
}
