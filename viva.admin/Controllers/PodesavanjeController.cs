using viva.admin.Models.Files;
using viva.admin.Models.Podesavanja;
using AutoMapper;
using Data.Models;
using Data.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace viva.admin.Controllers
{
    public class PodesavanjeController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IWSCalls _apiService;
        private readonly IMapper _mapper;
        private IHostingEnvironment Environment;
        public IConfiguration Configuration { get; set; }

        public PodesavanjeController(ISettingsService settingsService, IMapper mapper, IWSCalls apiService, IConfiguration configuration, IHostingEnvironment environment)
        {
            _settingsService = settingsService;
            _mapper = mapper;
            _apiService = apiService;
            Configuration = configuration;
            Environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var orgJed = await _apiService.PodaciOrgJed();
            List<Magacin> OrgJed = new List<Magacin>();
            foreach (var org in orgJed.Table)
            {
                OrgJed.Add(new Magacin {Id=Convert.ToInt32(org.ID),Sifra=org.Sifra,Naziv=org.Zvanicninaziv });
            }

            var vm = new PodesavanjaViewModel();
            vm.OrgJed= OrgJed;
            var magacini = await _settingsService.AktivniMagacin();
            List<Magacin> MagaciniList = new List<Magacin>();
            foreach (var record in magacini)
            {
                MagaciniList.Add(new Magacin {Id=record.Id,Sifra = record.Magacin});
            }
            vm.Magacini = MagaciniList;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(PodesavanjaViewModel vm)
        {
            var orgJed = await _apiService.PodaciOrgJed();
            List<Magacin> OrgJed = new List<Magacin>();
            foreach (var org in orgJed.Table)
            {
                OrgJed.Add(new Magacin { Id = Convert.ToInt32(org.ID), Sifra = org.Sifra, Naziv = org.Zvanicninaziv });
            }
            vm.OrgJed = OrgJed;
            _settingsService.SnimiMagacin(vm.OrgJedSifra);

            var magacini = await _settingsService.AktivniMagacin();
            List<Magacin> MagaciniList = new List<Magacin>();
            foreach (var record in magacini)
            {
                MagaciniList.Add(new Magacin { Id = record.Id, Sifra = record.Magacin });
            }
            vm.Magacini = MagaciniList;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> IzbaciMagacin(int Id)
        {
            _settingsService.IzbaciMagacin(Id);
            var orgJed = await _apiService.PodaciOrgJed();
            List<Magacin> OrgJed = new List<Magacin>();
            foreach (var org in orgJed.Table)
            {
                OrgJed.Add(new Magacin { Id = Convert.ToInt32(org.ID), Sifra = org.Sifra, Naziv = org.Zvanicninaziv });
            }
            
            var vm = new PodesavanjaViewModel();
            vm.OrgJed = OrgJed;
            var magacini = await _settingsService.AktivniMagacin();
            List<Magacin> MagaciniList = new List<Magacin>();
            foreach (var record in magacini)
            {
                MagaciniList.Add(new Magacin { Id = record.Id, Sifra = record.Magacin });
            }
            vm.Magacini = MagaciniList;
            return View("Index",vm);
        }


        public async Task<IActionResult> Meni()
        {
            var meni = await _settingsService.PrikaziMeni();
            var vm = _mapper.Map<List<Models.Podesavanja.Meni>>(meni);
            return View(vm);
        }

        public async Task<IActionResult> Slider () {
            SliderViewModel vm = new SliderViewModel();
            var sliders = await _settingsService.GetSliders();
            //vm.Sliders = sliders;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Slider(IFormFile file,string Text,string Link, string Order)
        {
            SliderViewModel vm = new SliderViewModel();
            var fileFolder = Configuration.GetValue<string>("SliderFolder");
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
                
            }
            var nSlider = new Slider();
            nSlider.Url = Link;
            nSlider.Text = Text;
            nSlider.Slika = fileName+"/" + extension;
            nSlider.Order = Convert.ToInt16(Order);
            await _settingsService.InsertSlider(nSlider);

            var sliders = await _settingsService.GetSliders();
            //vm.Sliders = sliders;
            return View(vm);
        }
    }
}
