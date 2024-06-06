using AutoMapper;
using Data.Models;
using Data.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using viva.admin.Enum;
using viva.admin.Models.Artikli;
using viva.admin.Models.Files;
using ShopSearchCriteriaModel = Data.Models.ShopSearchCriteriaModel;

namespace viva.admin.Controllers
{
    public class ArtikalsController : Controller
    {
        private readonly IModelService _modelService;
        private readonly IArtikliService _artikliService;
        private readonly ISyncService _syncService;
        private IHostingEnvironment Environment;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; set; }

        public ArtikalsController(IModelService modelService,IArtikliService artikliService, IHostingEnvironment _environment, IConfiguration configuration, IMapper mapper, ISyncService syncService)
        {
            _modelService = modelService;
            _artikliService = artikliService;
            Environment = _environment;
            Configuration = configuration;
            _mapper = mapper;
            _syncService = syncService;
        }



        // GET: Artikals
        public async Task<IActionResult> Index(Models.Artikli.ShopSearchCriteriaModel criteria)
        {
            var grupe = await _artikliService.GrupeArtikala();
            var brand = await _artikliService.Brand_Get();

            int pageSize = 0;
            if (criteria.RecordsByPage == null)
            {
                pageSize = 100;

            }
            else
            {
                pageSize = criteria.RecordsByPage.Value;
            }
            ArtikalListViewModel vm = new ArtikalListViewModel();
            vm.SamoArtikli = false;
            //var artikli = await _artikliService.SviArtikliAdmin(_mapper.Map<ShopSearchCriteriaModel>(criteria));
            //var pl = await Models.PaginatedList<Artikal>.CreateAsync(artikli, criteria.PageNumber ?? 1, pageSize);
            vm.Grupe = new SelectList(grupe, "Id", "Naziv");
            vm.Brendovi = new SelectList(brand, "Id", "SourceId");
            //vm.Artikli = pl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, Models.Artikli.ShopSearchCriteriaModel criteria)
        {
            int pageSize = 0;
            criteria.Brand = criteria.Brand_Id;
            if (criteria.RecordsByPage == null)
            {
                pageSize = 50;

            }
            else
            {
                pageSize = criteria.RecordsByPage.Value;
            }
            if (criteria.SamoArtikle)
            {
                var artikli = await _artikliService.SviArtikliAdmin(_mapper.Map<ShopSearchCriteriaModel>(criteria));
                var pl = await Models.PaginatedList<Data.Models.Artikal>.CreateAsync(artikli, criteria.PageNumber ?? 1, pageSize);
                var grupe = await _artikliService.GrupeArtikala();
                var brand = await _artikliService.Brand_Get();


                ArtikalListViewModel vm = new ArtikalListViewModel();

                vm.Artikli = pl;

                vm.RecordByPage = pageSize;
                vm.Sort = ((EnSort)criteria.SortMode).ToString().Replace("_", " ");
                vm.Grupe = new SelectList(grupe, "Id", "Naziv");
                vm.Brendovi = new SelectList(brand, "Id", "SourceId");
                vm.Group_Id = criteria.Group_Id;
                vm.Brand_Id = criteria.Brand_Id;
                vm.SamoArtikli = true;
                return View(vm);
            }
            else
            {
                var artikli = await _modelService.SviModeliAdmin(_mapper.Map<ShopSearchCriteriaModel>(criteria));
                var pl = await Models.PaginatedList<Data.Models.Model>.CreateAsync(artikli, criteria.PageNumber ?? 1, pageSize);
                var grupe = await _artikliService.GrupeArtikala();
                var brand = await _artikliService.Brand_Get();


                ArtikalListViewModel vm = new ArtikalListViewModel();

                vm.Modeli = pl;

                vm.RecordByPage = pageSize;
                vm.Sort = ((EnSort)criteria.SortMode).ToString().Replace("_", " ");
                vm.Grupe = new SelectList(grupe, "Id", "Naziv");
                vm.Brendovi = new SelectList(brand, "Id", "SourceId");
                vm.Group_Id = criteria.Group_Id;
                vm.Brand_Id = criteria.Brand_Id;
                vm.SamoArtikli = false;
                return View(vm);
            }

        }
        public async Task<bool> Sync(int id)
        {
            var artikal = await _artikliService.Artikal(id);
            if (artikal != null)
            {
                switch (artikal.Source)
                {
                    case "PROMOSOLUTIONS":
                        await _syncService.PROMOSOLUTIONS_SyncArtikala(artikal.SourceId.Trim());
                        break;
                }
                return true;

            }

            return true;
        }

        public async Task<bool> Stanje(int id)
        {
            var artikal = await _artikliService.Artikal(id);
            await _syncService.SyncStanja(artikal.Sifra);
            return true;
        }
        // GET: Artikals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artikal = await _artikliService.Artikal(id.Value);
            if (artikal == null)
            {
                return NotFound();
            }
            var model = await _modelService.Model(artikal.ModelId.Value);
            var artikalD = await _artikliService.ArtikalDetalj(id);
            if (artikalD == null)
            {
                //switch (artikal.Source)
                //{
                //    case "PROMOSOLUTIONS":
                //        //var result= _syncService.PROMOSOLUTIONS_SyncArtikala(artikal.SourceId.Trim());   
                //        break;
                //    default: return null;
                //}
                artikalD = await _artikliService.Artikal_Detail_New();
               
            }
            //artikalD = await _artikliService.ArtikalDetalj(id);
            var frontLink = Configuration.GetValue<string>("FrontLink");
            var fileFolder = Configuration.GetValue<string>("StorageLink");
            //string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(contentPath, fileFolder);

            var vm = new ArtikalDetailModelView()
            {
                Artikal_Id = id,
                MainImage = artikal.Slika,
                Image_Source = artikal.SlikaSource,
                FilePath = path,
                Naziv = artikal.Naziv,
                Boja = artikal.Color.Trim(),
                Velicina = artikal.Size.Trim(),
                Source = artikal.Source,
                VpCena = artikal.Vpcena.ToString(),
                Model_Id = model?.Id,
                Model_Naziv = model?.Naziv,
                Model_Id_Old = model?.Id
            };
            var svojstva = await _artikliService.Artikal_Svojstva(id.Value);
            if (svojstva != null)
            {
                foreach (var item in svojstva)
                {
                    vm.Svojstva.Add(new Artikal_Svojstva() { Artikal_Id = id.Value, Naziv = item.Naziv, Vrednost = item.Vrednost });
                }
            }

            if (artikalD != null)
            {
                var dodatneSlike = await _artikliService.VratiiDodatneSlike(id.Value);
                foreach (var item in dodatneSlike)
                {
                    vm.DodatneSlike.Add(new Slika() { Name = item.Slika, Source = item.Source, Id = item.Id });
                }
                vm.Opis = (artikalD != null) ? artikalD.Opis : "";
                vm.Opis2 = (artikalD != null) ? artikalD.Opis2 : "";
                vm.OpisOverride = artikalD.OpisOverride;
                vm.Webshop_Naziv = artikalD.WebshopNaziv;
                vm.OpisSEO = artikalD.OpisSeo;
                //vm.Deklaracija = artikalD.Deklaracija;


            }
            else
            {

            }
            return View(vm);
        }
        // POST: Artikals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Artikal_Id,Model_Id,Model_Id_Old,Opis,Opis2,OpisSEO,Webshop_Naziv,Boja,Velicina,Source,MpCena,Popust,PDV,FinalnaCena,OpisOverride,OpisDetalj")] ArtikalDetailModelView detail)
        {
            if (id != detail.Id)
            {
                return NotFound();
            }
            var artikal = await _artikliService.Artikal(detail.Artikal_Id.Value);
            if (ModelState.IsValid)
            {
                try
                {

                    if (await _artikliService.ExistsD(detail.Artikal_Id))
                    {
                        var artikalDetaljDB = await _artikliService.ArtikalDetalj(detail.Artikal_Id);
                        artikalDetaljDB.Opis = detail.Opis;
                        artikalDetaljDB.Opis2 = detail.Opis2;
                        artikalDetaljDB.OpisSeo = detail.OpisSEO;
                        artikalDetaljDB.WebshopNaziv = detail.Webshop_Naziv;
                        artikalDetaljDB.OpisOverride = detail.OpisOverride;
                        //artikalDetaljDB.Deklaracija = detail.Deklaracija;
                        var result = _artikliService.UpdateD(artikalDetaljDB);

                        artikal.Mpcena = Convert.ToDecimal(detail.MpCena);
                        artikal.Popust = Convert.ToDouble(detail.Popust);
                        artikal.Pdv = detail.PDV;
                        artikal.FinalnaCena = Convert.ToDouble(detail.FinalnaCena);
                        artikal.Color = detail.Boja;
                        artikal.Size = detail.Velicina;
                        artikal.Source = detail.Source;
                        artikal.ModelId = detail.Model_Id;
                        await _artikliService.UpdateArtikal(artikal);

                    }
                    else
                    {
                        var result = _artikliService.InsertD(new ArtikalDetalj() { ArtikalId = detail.Artikal_Id.Value, Opis = detail.Opis, Opis2 = detail.Opis2, OpisSeo = detail.OpisSEO, WebshopNaziv = detail.Webshop_Naziv });
                    }
                    if (detail.Model_Id != detail.Model_Id_Old)
                    {
                        //Kada jer polje prazno onda obrisi tu konbinaciju
                        var model = await _modelService.Model(artikal.ModelId.Value);
                        var model_old = await _modelService.Model(detail.Model_Id_Old.Value);

                        var model_color = model.ModelColor.Where(x => x.Color.Trim() == detail.Boja).FirstOrDefault();


                        if (model_color != null)
                        {
                            var model_color_size = model_color.ModelColorSize.Where(x => x.Size == detail.Velicina).FirstOrDefault();
                            if (model_color_size == null)
                            {
                                model.ModelColor.Where(x => x.Color.Trim() == detail.Boja).FirstOrDefault().ModelColorSize.Add(new ModelColorSize() { ArtikalId = artikal.Id, Size = detail.Velicina });

                            }

                        }
                        else
                        {
                            var m_c = new ModelColor() { Color = detail.Boja };
                            m_c.ModelColorSize.Add(new ModelColorSize() { Size = detail.Velicina, ArtikalId = artikal.Id });
                            model.ModelColor.Add(m_c);
                        }
                        await _modelService.Model_Update(model);
                        await _modelService.Model_Size_Delete(model_old.Id);


                    }


                }
                catch (DbUpdateConcurrencyException)
                {

                }
                //return RedirectToAction(nameof(Index));
            }
            return View(detail);
        }

        public async Task<bool> FixModel(int model_id, string models)
        {

            List<int> modeli_ostali = new List<int>();
            var model_glavni = await _modelService.Model(model_id);
            modeli_ostali = models.Split(',').Select(n => (int)Convert.ToInt32(n)).ToList();
            try
            {
                foreach (var model in modeli_ostali)
                {
                    var model_old = await _modelService.Model(model);
                    var model_color_old = model_old.ModelColor.FirstOrDefault();

                    if (model_color_old != null)
                    {
                        var artikal_id = model_color_old.ModelColorSize.FirstOrDefault().ArtikalId;
                        var artikal = await _artikliService.Artikal(artikal_id.Value);
                        if (model_glavni.ModelColor.Any(x => x.Color == model_color_old.Color))
                        {
                            model_glavni.ModelColor.Where(x => x.Color == model_color_old.Color).FirstOrDefault().ModelColorSize.
                                Add(model_color_old.ModelColorSize.FirstOrDefault());
                            model_old.ModelColor.Remove(model_color_old);
                        }
                        else
                        {
                            model_glavni.ModelColor.Add(model_color_old);
                            model_old.ModelColor.Remove(model_color_old);

                        }
                        artikal.ModelId = model_glavni.Id;

                        await _modelService.Model_Update(model_glavni);
                        await _modelService.Model_Update(model_old);
                        await _artikliService.UpdateArtikal(artikal);
                        //await _artikliService.Model_Size_Delete(model_old.Id);
                        await _modelService.Model_Delete(model_old.Id);
                    }
                    else
                    {
                        await _modelService.Model_Delete(model_old.Id);
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> FixModels()
        {
            var modeli_lacuna = await _modelService.Modeli_Lacuna();
            ILookup<string, Model> lookupM = null;
            if (modeli_lacuna != null && modeli_lacuna.Count > 0)
            {
                lookupM = modeli_lacuna.OrderBy(x => x.Naziv).ToLookup(x => x.Naziv);
                foreach (IGrouping<string, Model> record in lookupM)
                {
                    var modeli = record.Select(x => x).ToList();
                    if (modeli.Count > 1)
                    {
                        int i = 1;
                        var model_main = await _modelService.Model(modeli[0].Id);
                        var model_main_cena = model_main.Vpcena;
                        foreach (var modeli_record in modeli)
                        {
                            if (i > 1)
                            {

                                var model_old = await _modelService.Model(modeli_record.Id);

                                var model_color_old = model_old.ModelColor.FirstOrDefault();
                                if (model_color_old != null)
                                {
                                    var artikal_id = model_color_old.ModelColorSize.FirstOrDefault().ArtikalId;
                                    var artikal = await _artikliService.Artikal(artikal_id.Value);
                                    if (model_main.ModelColor.Any(x => x.Color == model_color_old.Color))
                                    {
                                        model_main.ModelColor.Where(x => x.Color == model_color_old.Color).FirstOrDefault().ModelColorSize.
                                            Add(model_color_old.ModelColorSize.FirstOrDefault());
                                        model_old.ModelColor.Remove(model_color_old);
                                    }
                                    else
                                    {
                                        model_main.ModelColor.Add(model_color_old);
                                        model_old.ModelColor.Remove(model_color_old);

                                    }
                                    artikal.ModelId = model_main.Id;
                                    if (model_main_cena != model_old.Vpcena)
                                    {
                                        artikal.Temp = "w";
                                    }
                                    await _modelService.Model_Update(model_main);
                                    await _modelService.Model_Update(model_old);
                                    await _artikliService.UpdateArtikal(artikal);
                                    //await _artikliService.Model_Size_Delete(model_old.Id);
                                    await _modelService.Model_Delete(model_old.Id);
                                }
                                else
                                {
                                    await _modelService.Model_Delete(model_old.Id);
                                }
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }

            }

            return true;

        }

        // GET: Artikals/Edit/5
        public async Task<IActionResult> Model_Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _modelService.Model(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            var model_detail = await _modelService.Model_Detail(id);
            if (model_detail == null)
            {
                var result = _modelService.Model_Detail_Insert(
                           new ModelDetail()
                           {
                               ModelId = id.Value,
                               Opis2 = null,
                               OpisDetalj = null,
                               OpisOverride = false,
                               OpisSeo = null,
                               WebshopNaziv = null
                           });
                model_detail = result.Result;

            }

            var frontLink = Configuration.GetValue<string>("FrontLink");
            var fileFolder = Configuration.GetValue<string>("StorageLink");
            //string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(contentPath, fileFolder);

            var vm = new Model_Detail_VM()
            {
                Id = model_detail.Id,
                Model_Id = model.Id,
                MainImage = model.Slika,
                Image_Source = model.SlikaSource,
                FilePath = path,
                Naziv = model.Naziv,
                Opis = model.Description,
                Opis2 = (model_detail != null) ? model_detail.Opis2 : null,
                OpisDetalj = (model_detail != null) ? model_detail.OpisDetalj : null,
                Source = model.Source,
                MpCena = model.Mpcena.ToString(),
                Popust = model.Popust.ToString(),
                Tag = model.Tag,
                FinalnaCena = model.FinalnaCena.ToString(),
                FinalnaCenaBezPdv = model.FinalnaCenaPdv.ToString()
            };
            var grupa = await _artikliService.GrupeArtikalaById(model.Grupa1.Value);
            var grupe = await _artikliService.GrupeArtikala();
            if (model.Grupa1 != null)
            {
                foreach (var record in grupe)
                {
                    vm.Grupe.Add(new SelectListItem { Value = record.Id.ToString(), Text = record.Naziv });
                }
            }
            if (model.Grupa2 != null)
            {
                var pod_grupa = await _artikliService.GrupeArtikalaById(model.Grupa2.Value);
                var pod_grupe = await _artikliService.Pod_Grupe(grupa.Id);
                vm.Pod_Grupe.Add(new SelectListItem { Value = "-1", Text = "Izaberi..." });

                foreach (var record in pod_grupe)
                {
                    vm.Pod_Grupe.Add(new SelectListItem { Value = record.Id.ToString(), Text = record.Naziv });
                }
                vm.Pod_Grupa = pod_grupa != null ? pod_grupa.Id.ToString() : "-1";
            }
            else
            {
                vm.Pod_Grupa = "-1";
            }

            vm.Grupa = grupa.Id.ToString();



            if (model_detail != null)
            {
                var dodatneSlike = await _artikliService.VratiiDodatneSlike(id.Value);
                foreach (var item in dodatneSlike)
                {
                    vm.DodatneSlike.Add(new Slika() { Name = item.Slika, Source = item.Source });
                }

                vm.Opis2 = (model_detail != null) ? model_detail.Opis2 : "";
                vm.OpisDetalj = model_detail.OpisDetalj;
                vm.OpisOverride = model_detail.OpisOverride;
                vm.Webshop_Naziv = model_detail.WebshopNaziv;
                vm.OpisSEO = model_detail.OpisSeo;
                //vm.Deklaracija = model_detail.Deklaracija;


            }
            var model_artikli = await _artikliService.Model_Artikli(model.Id);
            vm.Artikli = model_artikli.ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Model_Edit(int id, [Bind("Id,Model_Id,Opis,Opis2,OpisSEO,Webshop_Naziv,OpisOverride,OpisDetalj,Grupa,Pod_Grupa,Tag")] Model_Detail_VM detail)
        {
            if (id != detail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model_from_db = await _modelService.Model(detail.Model_Id.Value);
                    model_from_db.Description = detail.Opis;
                    model_from_db.WebshopNaziv = detail.Webshop_Naziv;
                    model_from_db.Tag = detail.Tag;
                    model_from_db.Grupa1 = Convert.ToInt32(detail.Grupa);
                    if (detail.Pod_Grupa == "-1")
                    {
                        model_from_db.Grupa2 = null;
                    }
                    else
                    {
                        model_from_db.Grupa2 = Convert.ToInt32(detail.Pod_Grupa);
                    }

                    await _modelService.Model_Update(model_from_db);
                    var model_detail_from_db = new ModelDetail();
                    if (await _modelService.Model_Detail_Exist(detail.Model_Id))
                    {
                        model_detail_from_db = await _modelService.Model_Detail(detail.Model_Id);

                        model_detail_from_db.Opis2 = detail.Opis2;
                        model_detail_from_db.OpisDetalj = detail.OpisDetalj;
                        //model_detail_from_db.Deklaracija = detail.Deklaracija;
                        model_detail_from_db.OpisSeo = detail.OpisSEO;
                        var result = _modelService.Model_Detail_Update(model_detail_from_db);


                    }
                    else
                    {
                        var result = _modelService.Model_Detail_Insert(
                            new ModelDetail()
                            {
                                ModelId = detail.Model_Id.Value,
                                Opis2 = detail.Opis2,
                                OpisDetalj = detail.OpisDetalj,
                                OpisOverride = detail.OpisOverride,
                                OpisSeo = detail.OpisSEO,
                                WebshopNaziv = detail.Webshop_Naziv
                            });
                    }
                    IList<Artikal> model_artikli = await _artikliService.Model_Artikli(detail.Model_Id.Value);
                    //foreach (Artikal artikal in model_artikli)
                    //{
                    //    await Model_Artikal_Update(artikal.Id, model_from_db, model_detail_from_db);
                    //}

                    model_from_db = await _modelService.Model(detail.Model_Id.Value);
                    model_detail_from_db = await _modelService.Model_Detail(id);
                    var frontLink = Configuration.GetValue<string>("FrontLink");
                    var fileFolder = Configuration.GetValue<string>("StorageLink");
                    //string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(contentPath, fileFolder);
                    var vm = new Model_Detail_VM()
                    {
                        Id = id,
                        Model_Id = id,
                        MainImage = model_from_db.Slika,
                        Image_Source = model_from_db.SlikaSource,
                        FilePath = path,
                        Naziv = model_from_db.Naziv,
                        Opis = model_from_db.Description,
                        Opis2 = (model_detail_from_db != null) ? model_detail_from_db.Opis2 : null,
                        OpisDetalj = (model_detail_from_db != null) ? model_detail_from_db.OpisDetalj : null,
                        Source = model_from_db.Source,
                        MpCena = model_from_db.Mpcena.ToString(),
                        Popust = model_from_db.Popust.ToString(),
                        Tag = model_from_db.Tag,
                        FinalnaCena = model_from_db.FinalnaCena.ToString(),
                        FinalnaCenaBezPdv = model_from_db.FinalnaCenaPdv.ToString()
                    };

                    var grupe = await _artikliService.GrupeArtikala();

                    var pod_grupe = await _artikliService.Pod_Grupe(Convert.ToInt16(detail.Grupa));
                    vm.Pod_Grupe.Add(new SelectListItem { Value = "-1", Text = "Izaberi..." });
                    foreach (var record in grupe)
                    {
                        vm.Grupe.Add(new SelectListItem { Value = record.Id.ToString(), Text = record.Naziv });
                    }
                    foreach (var record in pod_grupe)
                    {
                        vm.Pod_Grupe.Add(new SelectListItem { Value = record.Id.ToString(), Text = record.Naziv });
                    }
                    vm.Grupa = detail.Grupa;
                    vm.Pod_Grupa = detail.Pod_Grupa != "-1" ? detail.Pod_Grupa : "-1";

                    vm.Artikli = model_artikli.ToList();
                    return View(vm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View(detail);
                }

                //return RedirectToAction(nameof(Index));
            }
            return View(detail);
        }

        public async Task Model_Artikal_Update(int id, Model model, ModelDetail model_detail)
        {
            var artikal_from_db = await _artikliService.Artikal(id);
            if (artikal_from_db != null)
            {
                var artikal_detail_from_db = await _artikliService.ArtikalDetalj(id);
                artikal_from_db.Mpcena = Convert.ToDecimal(model.Mpcena);
                artikal_from_db.Popust = Convert.ToDouble(model.Popust);
                //artikal_from_db.Pdv = model.Pdv;
                artikal_from_db.FinalnaCena = Convert.ToDouble(model.FinalnaCena);
                artikal_from_db.FinalnaCenaPdv = Convert.ToDouble(model.FinalnaCenaPdv);

                artikal_from_db.WebshopNaziv = model.WebshopNaziv;
                await _artikliService.UpdateArtikal(artikal_from_db);
                artikal_detail_from_db.Opis = model.Description;
                artikal_detail_from_db.OpisSeo = model_detail.OpisSeo;
                //artikal_detail_from_db.Deklaracija = model_detail.Deklaracija;
                await _artikliService.Artikal_Detail_Update(artikal_detail_from_db);
            }
        }

        // POST: Artikals/Edit/5
        public ActionResult SomeImage(string imageName)
        {
            //var frontLink = Configuration.GetValue<string>("FrontLink");
            //var fileFolder = Configuration.GetValue<string>("StorageLink")


            var fileFolder = Configuration.GetValue<string>("ImageFolder");
            //string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(contentPath, fileFolder);

            //string contentPath = this.Environment.ContentRootPath;
            //var path = Path.Combine(contentPath, fileFolder);
            path = Path.GetFullPath(path + "\\" + imageName);
            //if (!path.StartsWith(root))
            //{
            //    // Ensure that we are serving file only inside the root folder
            //    // and block requests outside like "../web.config"
            //    //throw new Exception(403, "Forbidden");
            //}

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet]
        public async Task<JsonResult> ChangePrice(int id, double mp, double popust)
        {
            var model_from_db = await _modelService.Model(id);
            model_from_db.Mpcena = Convert.ToDecimal(mp);
            model_from_db.Popust = Convert.ToDouble(popust);
            //model_from_db.Pdv = detail.PDV;
            double pdv = 20;
            double finalna_cena = mp - (mp * (popust / 100));
            double finalna_cena_bez_pdv = finalna_cena / (1 + (pdv / 100));
            model_from_db.FinalnaCena = finalna_cena;
            model_from_db.FinalnaCenaPdv = finalna_cena_bez_pdv;
            await _modelService.Model_Update(model_from_db);
            return Json(new { mp = mp, popust = popust, fc = finalna_cena, fcnopdv = finalna_cena_bez_pdv });
        }

        [HttpGet]
        public async Task<JsonResult> ChangeGroup(int g_id)
        {
            var pod_grupe = await _artikliService.Pod_Grupe(g_id);
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Value = "-1", Text = "Izaberi..." });
            foreach (var record in pod_grupe)
            {
                lista.Add(new SelectListItem { Value = record.Id.ToString(), Text = record.Naziv });
            }
            return Json(lista);
        }
        [HttpGet]
        public async Task<JsonResult> Model_Snimi_Sliku_Extern(int m_id, string url, bool copy_to_all_atricles)
        {
            var model = await _modelService.Model(m_id);
            model.SlikaSource = "extern";
            model.Slika = url;
            await _modelService.Model_Update(model);
            if (copy_to_all_atricles)
            {
                var artikli = await _artikliService.Model_Artikli(m_id);
                foreach (var artikal in artikli)
                {
                    artikal.SlikaSource = "extern";
                    artikal.Slika = url;
                    artikal.ModelId = m_id;
                    await _artikliService.UpdateArtikal(artikal);
                }
            }

            return Json(url);
        }

        [HttpGet]
        public async Task<JsonResult> Artikal_Snimi_Sliku_Extern(int a_id, string url, bool set_as_model_img, bool set_to_another_articles, string another_articles)
        {
            bool has_main_img = false;
            var artikal = await _artikliService.Artikal(a_id);
            has_main_img = (String.IsNullOrEmpty(artikal.Slika)) ? false : true;
            if (set_to_another_articles)
            {
                var ids = another_articles.Split(',').ToList();
                foreach (var id in ids)
                {
                    var _artikal = await _artikliService.Artikal(Convert.ToInt32(id));
                    var _has_main_img = (String.IsNullOrEmpty(_artikal.Slika)) ? false : true;
                    if (!_has_main_img)
                    {
                        _artikal.SlikaSource = "extern";
                        _artikal.Slika = url;
                        await _artikliService.UpdateArtikal(_artikal);
                    }
                    else
                    {
                        var dodatne_slike = await _artikliService.VratiiDodatneSlike(Convert.ToInt32(id));
                        ArtikalSlike dodatna_slika = new ArtikalSlike()
                        {
                            ArtikalId = Convert.ToInt32(id),
                            Slika = url,
                            Source = "extern",
                            Main = false
                        };
                        _artikliService.SnimiDodatnuSliku(dodatna_slika);
                    }
                }
            }
            else
            {
                if (!has_main_img)
                {
                    artikal.SlikaSource = "extern";
                    artikal.Slika = url;
                    await _artikliService.UpdateArtikal(artikal);
                }
                else
                {
                    var dodatne_slike = await _artikliService.VratiiDodatneSlike(a_id);
                    ArtikalSlike dodatna_slika = new ArtikalSlike()
                    {
                        ArtikalId = a_id,
                        Slika = url,
                        Source = "extern",
                        Main = false
                    };
                    _artikliService.SnimiDodatnuSliku(dodatna_slika);
                }
            }

            if (set_as_model_img)
            {
                var model_id = artikal.ModelId;
                var model = await _modelService.Model(model_id.Value);
                model.Slika = url;
                model.SlikaSource = "extern";
                _modelService.Model_Update(model);
            }

            //if (copy_to_all_atricles)
            //{
            //    var artikli = await _artikliService.Model_Artikli(m_id);
            //    foreach (var artikal in artikli)
            //    {
            //        artikal.SlikaSource = "extern";
            //        artikal.Slika = url;
            //        artikal.ModelId = m_id;
            //        await _artikliService.UpdateArtikal(artikal);
            //    }
            //}

            return Json(url);
        }
        public async Task<bool> Artikal_Obrisi_Sliku(int id,int a_id, bool isMain, string source)
        {
            if (isMain)
            {
                var artikal = await _artikliService.Artikal(a_id);
                artikal.Slika = null;
                artikal.SlikaSource = null;
                return await _artikliService.UpdateArtikal(artikal);
            }
            else
            {
                var dodatna_slika = await _artikliService.Artikal_Dodatna_Slika_Delete(id);
                return true;
            }
        }

        public async Task<List<Artikal_Za_Izbor_Slike_Model>> Artikli_Za_Izbor_Slike(int a_id)
        {
            List<Artikal_Za_Izbor_Slike_Model> lista_output = new List<Artikal_Za_Izbor_Slike_Model>();
            var artikal = await _artikliService.Artikal(a_id);
            var model_id = artikal.ModelId.Value;
            var artikli = await _artikliService.Model_Artikli(model_id);
            foreach (var _artikal in artikli)
            {
                lista_output.Add(new Artikal_Za_Izbor_Slike_Model()
                {
                    Id = _artikal.Id,
                    Naziv = _artikal.Naziv,
                    Boja = _artikal.Color,
                    Velicina = _artikal.Size
                });
            }
            return lista_output;
        }

        [HttpPost]
        public async Task<JsonResult> Model_Snimi_Sliku_Intern(List<IFormFile> files, IFormCollection data)
        {
            string filePath = "";
            string fileName = "";
            string extension = "";
            long size = files.Sum(f => f.Length);
            string artikal_id = data["id"];
            foreach (var file in files)
            {


                var fileFolder = Configuration.GetValue<string>("ImageFolder");
                //string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(contentPath, fileFolder);
                string pathThumb = path + "thumb/";
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(path);
                if (!basePathExists) Directory.CreateDirectory(path);
                //fileName = Path.GetFileNameWithoutExtension(file.FileName);
                fileName = Guid.NewGuid().ToString();
                filePath = Path.Combine(path, file.FileName);
                extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await file.CopyToAsync(stream);

                        //thumb.Save(pathThumb);
                    }
                    var fileModel = new FileOnFileSystemModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,

                        FilePath = filePath
                    };
                    await _artikliService.SnimiGlavnuSliku(Convert.ToInt32(artikal_id), fileName + extension);

                }
            }
            return Json(filePath);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IList<IFormFile> files)
        {
            foreach (IFormFile source in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                //using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                //    await source.CopyToAsync(output);
            }
            return null;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        //private string GetPathAndFilename(string filename)
        //{
        //    return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
        //}


        public Image GetReducedImage(int width, int height, FileStream resourceImage)
        {
            try
            {
                var uploadedImage = Image.FromStream(resourceImage);
                var w = uploadedImage.Width;
                var h = uploadedImage.Height;
                var thumb = uploadedImage.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        [HttpPost]
        public async Task<IActionResult> ObrisiGlavnuSliku(List<IFormFile> files, int Artikal_Id)
        {


            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                var frontLink = Configuration.GetValue<string>("FrontLink");
                var fileFolder = Configuration.GetValue<string>("ImageFolder");
                //string wwwPath = this.Environment.WebRootPath;
                //string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(frontLink, fileFolder);
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
                    await _artikliService.SnimiGlavnuSliku(Artikal_Id, fileName + extension);
                    //context.FilesOnFileSystem.Add(fileModel);
                    //context.SaveChanges();
                }
            }

            return RedirectToAction("Edit", new { id = Artikal_Id });
        }

        public async Task<JsonResult> Vrati_Boje_Za_Model(int id)
        {
            var boje = await _modelService.Model_Colors_By_Model(id);
            return new JsonResult(boje);
        }
        public async Task<JsonResult> PromeniAktivan(int id, bool value)
        {
            bool result = true;
            try
            {
                var artikal = await _modelService.Model(id);
                artikal.Aktivan = value;
                _modelService.Model_Update(artikal);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> PromeniNajprodavaniji(int id, bool value)
        {
            bool result = true;
            try
            {
                var artikal = await _modelService.Model(id);
                artikal.Najprodavaniji = value;
                _modelService.Model_Update(artikal);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> PromeniNovo(int id, bool value)
        {
            bool result = true;
            try
            {
                var artikal = await _modelService.Model(id);
                artikal.Novo = value;
                _modelService.Model_Update(artikal);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return new JsonResult(result);
        }
    }

}
