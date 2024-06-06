using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using viva.admin.Models.Files;
using viva.admin.Models.Grupe;

namespace viva.admin.Controllers
{
    public class GrupeArtikalasController : Controller
    {
        private readonly IArtikliService _artikliService;
        private IHostingEnvironment Environment;
        public IConfiguration Configuration { get; set; }

        public GrupeArtikalasController(IArtikliService artikliService = null, IConfiguration configuration = null, IHostingEnvironment environment = null)
        {

            _artikliService = artikliService;
            Configuration = configuration;
            Environment = environment;
        }

        // GET: GrupeArtikalas
        public async Task<IActionResult> Index()
        {
            return View(await _artikliService.GrupeArtikala());
        }

        // GET: GrupeArtikalas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupeArtikala = await _artikliService.GrupeArtikala(id.Value);
            if (grupeArtikala == null)
            {
                return NotFound();
            }

            return View(grupeArtikala);
        }
        [HttpGet]
        public async Task<PartialViewResult> Vrati_Podgrupe(int g_id)
        {
            var grupa = await _artikliService.GrupeArtikalaById(g_id);
            var podgrupe_from_db = await _artikliService.Pod_Grupe(g_id);
            var podgrupe = new List<Grupa>();
            foreach (var record in podgrupe_from_db)
            {
                podgrupe.Add(new Grupa() { Id = record.Id, Naziv = record.Naziv });
            }
            var vm = new Pod_Grupe_VM()
            {
                Grupa_Id = g_id,
                Grupa_Naziv = grupa.Naziv,
                Pod_Grupe = podgrupe
            };
            return PartialView("_Pod_grupe_table",vm);
        }
        [HttpGet]
        public async Task<JsonResult> UbaciPodGrupu(string naziv, int g_id)
        {
            var grupa = await _artikliService.GrupeArtikalaById(g_id);
            var podgrupa = await _artikliService.Pod_Grupe_By_Naziv(g_id,naziv);
            if(podgrupa == null)
            {
                var vm = new GrupeArtikala()
                {
                    ParentId = g_id,
                    Naziv = naziv,
                    SourceId = null,
                    Aktivan = true,
                    Naslovna = false,
                    ParentSourceId = g_id.ToString(),
                    AdminList = true,
                    Source = null
                };
                await _artikliService.Grupa_Add(vm);
                return Json(vm);
            }
            else
            {
                return Json(null);
            }
        }
        // GET: GrupeArtikalas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrupeArtikalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CalculusId,Naziv,WebshopNaziv")] GrupeArtikala grupeArtikala)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(grupeArtikala);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(grupeArtikala);
        //}

        // GET: GrupeArtikalas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupeArtikala = await _artikliService.GrupeArtikala(id.Value);
            if (grupeArtikala == null)
            {
                return NotFound();
            }
            return View(grupeArtikala);
        }

        // POST: GrupeArtikalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CalculusId,Naziv,WebshopNaziv,Aktivan,Naslovna,Order")] GrupeArtikala grupeArtikala)
        {
            if (id != grupeArtikala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _artikliService.GrupeUpdate(grupeArtikala);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //    if (!GrupeArtikalaExists(grupeArtikala.Id))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                }
                return View(grupeArtikala);
            }
            return View(grupeArtikala);
        }

        [HttpPost]
        public async Task<IActionResult> SnimiGlavnuSlikuGrupa(List<IFormFile> files, int Id)
        {


            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {


                var fileFolder = Configuration.GetValue<string>("ImageFolder");
                //string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(contentPath, fileFolder);
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(path);
                if (!basePathExists) Directory.CreateDirectory(path);
                //var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(path, fileName + extension);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new FileOnFileSystemModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,

                        FilePath = filePath
                    };
                    await _artikliService.SnimiGlavnuSlikuGrupa(Id, fileName + extension);
                    //context.FilesOnFileSystem.Add(fileModel);
                    //context.SaveChanges();
                }
            }



            //if (file.Length > 0)
            //{
            //    var filePath = Path.GetTempFileName();

            //    using (var stream = System.IO.File.Create(filePath))
            //    {
            //        await file.CopyToAsync(stream);
            //    }
            //}

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Edit", new { id = Id });
        }

        public async Task<ActionResult> ObrisiSliku(int id, [Bind("Id,Slika")] GrupeArtikala grupeArtikala)
        {

            if (id != grupeArtikala.Id)
            {
                return NotFound();
            }
            var grupa = await _artikliService.GrupeArtikala(id);
            if (ModelState.IsValid)
            {
                try
                {
                    grupa.Slika = null;

                    await _artikliService.GrupeUpdate(grupa);

                    var fileFolder = Configuration.GetValue<string>("ImageFolder");
                    //string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(contentPath, fileFolder);
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                    bool basePathExists = System.IO.Directory.Exists(path);
                    if (!basePathExists) Directory.CreateDirectory(path);
                    //var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString();

                    var filePath = Path.Combine(path, grupeArtikala.Slika);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);

                        //context.FilesOnFileSystem.Add(fileModel);
                        //context.SaveChanges();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    //    if (!GrupeArtikalaExists(grupeArtikala.Id))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                }
                return View("Edit", grupa);
            }
            return View("Edit", grupa);
        }
        public ActionResult SomeImage(string imageName)
        {
            var frontLink = Configuration.GetValue<string>("FrontLink");
            var fileFolder = Configuration.GetValue<string>("ImageFolder");
            string contentPath = this.Environment.ContentRootPath;
            var path = Path.Combine(contentPath, fileFolder);
            path = Path.GetFullPath(path + "\\" + imageName);
            //if (!path.StartsWith(root))
            //{
            //    // Ensure that we are serving file only inside the root folder
            //    // and block requests outside like "../web.config"
            //    //throw new Exception(403, "Forbidden");
            //}

            return PhysicalFile(path, "image/jpeg");
        }

        public async Task<JsonResult> AktivirajGrupu(bool artikli, int gId)
        {
            var result = await _artikliService.AktivirajGrupu(artikli, gId);
            return Json(result);
        }
        public async Task<JsonResult> DeaktivirajGrupu(bool artikli, int gId)
        {
            var result = await _artikliService.DeaktivirajGrupu(artikli, gId);
            return Json(result);
        }
        // GET: GrupeArtikalas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grupeArtikala = await _context.GrupeArtikala
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (grupeArtikala == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grupeArtikala);
        //}

        // POST: GrupeArtikalas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var grupeArtikala = await _context.GrupeArtikala.FindAsync(id);
        //    _context.GrupeArtikala.Remove(grupeArtikala);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GrupeArtikalaExists(int id)
        //{
        //    return _context.GrupeArtikala.Any(e => e.Id == id);
        //}
    }
}
