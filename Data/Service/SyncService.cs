using Data.Model.Models.Lacuna;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Service
{
    public class SyncService : ISyncService
    {

        private readonly IWSCalls _apiService;
        private readonly IModelService _modelService;
        private readonly IArtikliService _artikliService;

        public SyncService(IWSCalls apiService, IModelService modelService, IArtikliService artikliService)
        {
            _apiService = apiService;
            _modelService = modelService;
            _artikliService = artikliService;
        }

        e003186Context dbContext = new e003186Context();

        public async Task<bool> SyncTipoveCenovnika()
        {
            return false;
        }

        public async Task<bool> SyncCenovnika(string tip)
        {
            return false;
        }
        public async Task<bool> SyncSlika()
        {
            int[] a = new int[60] {10387,
                10389,
                10396,
                10400,
                10403,
                10406,
                10408,
                10410,
                10414,
                10418,
                10419,
                10437,
                10438,
                10439,
                10440,
                10442,
                10443,
                10444,
                10445,
                10454,
                10456,
                10458,
                10461,
                10464,
                10466,
                10467,
                10472,
                10473,
                10474,
                10475,
                10485,
                10489,
                10490,
                10492,
                10493,
                10494,
                10495,
                10496,
                10497,
                10501,
                10503,
                10510,
                10512,
                10529,
                10532,
                10547,
                10548,
                10557,
                10572,
                10576,
                10577,
                10578,
                10579,
                10580,
                10581,
                10583,
                10586,
                10591,
                10592,
                10618 };
            foreach(var a_id in a)
            {
                var modeli = await dbContext.Model.Where(x => x.Id==a_id).ToListAsync();
                //var modeli = await dbContext.Model.Where(x => x.Aktivan == true && x.Source == "ALBO" && x.Slika != null).ToListAsync();
                foreach (var record in modeli)
                {
                    var artikli = await dbContext.Artikal.Where(x => x.ModelId == record.Id).ToListAsync();
                    if (artikli != null)
                    {
                        foreach (var artikal in artikli)
                        {
                            if (artikal.Slika == null)
                            {
                                artikal.Slika = record.Slika;
                                artikal.SlikaSource = "extern";
                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            
            return true;
        }

        public async Task<bool> SyncStanja(string sifra)
        {
            return true;
        }
        public async Task<bool> SyncGrupeArtikala()
        {
            var wsGrupe = await _apiService.GrupaArtUsl();
            try
            {
                foreach (var item in wsGrupe.Table)
                {
                    var grupa = await dbContext.GrupeArtikala.Where(x => x.SourceId == item.ID).FirstOrDefaultAsync();
                    if (grupa != null)
                    {

                        grupa.Naziv = item.naziv;

                    }
                    else
                    {
                        GrupeArtikala ga = new GrupeArtikala()
                        {
                            SourceId = item.ID,
                            Naziv = item.naziv,
                            Aktivan = false,
                            ParentSourceId = item.IDnadredjene,
                            Source = "Calculus"
                        };



                        dbContext.GrupeArtikala.Add(ga);
                    }
                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        public async Task<bool> SavaCoop_Sync()
        {

            var modeli_from_excel = await dbContext.SavaCoop.Where(x => x.Tip == "M" && x.Vpcena!=null).ToListAsync();
            foreach (var record in modeli_from_excel)
            {
                int m_id = 0;
                var model_from_db = await dbContext.Model.Where(x => x.Naziv == record.Naziv.Trim()).FirstOrDefaultAsync();
                if (model_from_db == null)
                {
                    var model = new Models.Model()
                    {
                        Naziv = record.Naziv,
                        Slika = record.Slika,
                        SlikaSource = record.SlikaSource,
                        Grupa1 = Convert.ToInt32(record.Grupa1),
                        Grupa2 = Convert.ToInt32(record.Grupa2),
                        Source = record.Source,
                        Brand = record.Brand,
                        Aktivan = true,
                        Novo = false,
                        Description = record.Description,
                        Tag = record.Tag,
                        Popust = 0,
                        ZemljaPorekla = record.ZemljaPorekla
                    };
                    double vp = record.Vpcena.Value;
                    double mp = vp * 1.2;
                    model.Vpcena = Convert.ToDecimal(vp);
                    model.Mpcena= Convert.ToDecimal(mp);
                    model.FinalnaCenaPdv = vp;
                    model.FinalnaCena = mp;
                    var model_input = await dbContext.Model.AddAsync(model);
                    dbContext.SaveChanges();
                    m_id = model.Id;
                }
                else
                {
                    m_id = model_from_db.Id;
                }
                var artikli_from_excel = await dbContext.SavaCoop.Where(x => x.Tip == "A" && x.NazivModel==record.Naziv.Trim()).ToListAsync();
                if (artikli_from_excel.Count == 0)
                {
                    string boja = "";
                    if (String.IsNullOrEmpty(record.Boja.Trim()))
                    {
                        boja = "*";
                    }
                    else
                    {
                        boja = record.Boja.Trim();
                    }
                    var artikal_from_db = dbContext.Artikal.Where(x => x.ModelId == m_id && x.Size == record.Velicina && x.Color.Trim() == record.Boja.Trim()).FirstOrDefault(); ;
                    if (artikal_from_db == null)
                    {
                        var artikal_for_db = new Artikal()
                        {
                            ModelId = m_id,
                            Naziv = record.Naziv,
                            Slika = record.Slika,
                            SlikaSource = "extern",
                            Sifra=record.Sifra.ToString(),
                            Source = record.Source,
                            Brand = record.Brand,
                            Aktivan = true,
                            Novo = false,
                            Color=boja,
                            Size=record.Velicina,
                            Popust = 0
                        };
                        double vp = record.Vpcena.Value;
                        double mp = vp * (1 + (20 / 100));
                        artikal_for_db.Vpcena = Convert.ToDecimal(vp);
                        artikal_for_db.Mpcena = Convert.ToDecimal(mp);
                        artikal_for_db.FinalnaCenaPdv = vp;
                        artikal_for_db.FinalnaCena = mp;

                        dbContext.Artikal.Add(artikal_for_db);
                        dbContext.SaveChanges();
                     
                        var model_color_from_db = dbContext.ModelColor.Where(x => x.ModelId == m_id && x.Color.Trim() == boja).FirstOrDefault();
                        int m_c_id = 0;
                        if (model_color_from_db == null)
                        {
                            var model_color_for_db = new ModelColor()
                            {
                                ModelId = m_id,
                                Color = record.Boja.Trim()
                            };
                            dbContext.ModelColor.Add(model_color_for_db);
                            dbContext.SaveChanges();
                            m_c_id = model_color_for_db.Id;
                        }
                        else
                        {
                            m_c_id = model_color_from_db.Id;
                        }
                        var model_color_size_from_db = dbContext.ModelColorSize.Where(x => x.ModelColorId == m_c_id && x.Size == record.Velicina).FirstOrDefault();

                        if (model_color_size_from_db == null)
                        {
                            var model_color_size_for_db = new ModelColorSize()
                            {
                                ModelColorId = m_c_id,
                                ArtikalId = artikal_for_db.Id,
                                Size = record.Velicina
                            };
                            dbContext.ModelColorSize.Add(model_color_size_for_db);
                            dbContext.SaveChanges();
                        }

                    }
                }
                else
                {
                    foreach (var artikal in artikli_from_excel)
                    {
                        string boja = "";
                        if (String.IsNullOrEmpty(artikal.Boja.Trim()))
                        {
                            boja = "*";
                        }
                        else
                        {
                            boja = artikal.Boja.Trim();
                        }
                        var artikal_from_db = dbContext.Artikal.Where(x => x.ModelId == m_id && x.Size == artikal.Velicina && x.Color.Trim() == artikal.Boja.Trim()).FirstOrDefault(); ;
                        if (artikal_from_db == null)
                        {
                            var artikal_for_db = new Artikal()
                            {
                                ModelId = m_id,
                                Naziv = artikal.Naziv,
                                Sifra = artikal.Sifra.ToString(),
                                Slika = artikal.Slika,
                                SlikaSource = "extern",
                                Source = record.Source,
                                Brand = record.Brand,
                                Aktivan = true,
                                Novo = false,
                                Color = boja,
                                Size = artikal.Velicina,
                                Popust = 0
                            };
                            double vp = record.Vpcena.Value;
                            double mp = vp * 1.2;
                            artikal_for_db.Vpcena = Convert.ToDecimal(vp);
                            artikal_for_db.Mpcena = Convert.ToDecimal(mp);
                            artikal_for_db.FinalnaCenaPdv = vp;
                            artikal_for_db.FinalnaCena = mp;

                            dbContext.Artikal.Add(artikal_for_db);
                            dbContext.SaveChanges();
                        
                            var model_color_from_db = dbContext.ModelColor.Where(x => x.ModelId == m_id && x.Color.Trim() == boja).FirstOrDefault();
                            int m_c_id = 0;
                            if (model_color_from_db == null)
                            {
                                var model_color_for_db = new ModelColor()
                                {
                                    ModelId = m_id,
                                    Color = artikal.Boja.Trim()
                                };
                                dbContext.ModelColor.Add(model_color_for_db);
                                dbContext.SaveChanges();
                                m_c_id = model_color_for_db.Id;
                            }
                            else
                            {
                                m_c_id = model_color_from_db.Id;
                            }
                            var model_color_size_from_db = dbContext.ModelColorSize.Where(x => x.ModelColorId == m_c_id && x.Size == artikal.Velicina).FirstOrDefault();

                            if (model_color_size_from_db == null)
                            {
                                var model_color_size_for_db = new ModelColorSize()
                                {
                                    ModelColorId = m_c_id,
                                    ArtikalId = artikal_for_db.Id,
                                    Size = artikal.Velicina
                                };
                                dbContext.ModelColorSize.Add(model_color_size_for_db);
                                dbContext.SaveChanges();
                            }

                        }


                    }
                }
                



            }



            return true;
        }
        public async Task<bool> ALBO_Sync_Brand()
        {
            var wsBrandovi = await dbContext.Albo.Select(x => x.Proizvođač).ToListAsync();
            try
            {
                foreach (var item in wsBrandovi)
                {
                    var brand = await dbContext.Brand.Where(x => x.SourceId == item).FirstOrDefaultAsync();
                    if (brand != null)
                    {


                    }
                    else
                    {
                        Brand brnd = new Brand()
                        {
                            SourceId = item,
                            Image = null,
                            Source = "ALBO"
                        };



                        dbContext.Brand.Add(brnd);

                        dbContext.SaveChanges();
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> ALBO_Sync_Model()
        {
            var cene = await dbContext.AlboCene.ToListAsync();

            try
            {
                foreach (var item in cene)
                {
                    var model_from_db = await dbContext.Model.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                    if (model_from_db != null)
                    {
                        model_from_db.Mpcena = Convert.ToDecimal(item.Mpcena);
                        model_from_db.FinalnaCena = Convert.ToDouble(item.FinalnaCena);
                        model_from_db.FinalnaCenaPdv = Convert.ToDouble(item.FinalnaCenaPdv);
                        model_from_db.Aktivan = true;
                        dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> ALBO_SyncGrupeArtikala()
        {

            var wsGrupe = await dbContext.AlboGrupe.ToListAsync();

            try
            {
                foreach (var item in wsGrupe)
                {
                    var model = await dbContext.Model.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                    if (model != null)
                    {

                        model.Grupa1 = Convert.ToInt32(item.Grupa1);

                    }

                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> SyncAllGrupe()
        {

            var wsGrupe = await dbContext.ImportGroup.ToListAsync();
            var grupe_from_fb = await dbContext.GrupeArtikala.ToListAsync();
            try
            {
                foreach (var item in wsGrupe)
                {
                    var model = await dbContext.Model.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        var g1 = item.Grupa1.ToLower().Trim();
                        var primary_group = grupe_from_fb.Where(x => x.Naziv.ToLower().Trim() == g1 && x.Source == null).FirstOrDefault();
                        if (primary_group != null)
                        {
                            var g2 = item.Grupa2.ToLower().Trim();
                            if (!String.IsNullOrEmpty(g2))
                            {
                                var secondary_group = grupe_from_fb.Where(x => x.ParentId == primary_group.Id && x.Naziv.ToLower().Trim() == g2).FirstOrDefault();
                                if (secondary_group != null)
                                {
                                    model.Grupa2 = secondary_group.Id;
                                    await dbContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                model.Grupa2 = null;
                                await dbContext.SaveChangesAsync();
                            }

                        }


                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> ALBO_Sync_Artikla()
        {
            var albo_artikli = await dbContext.Albo.ToListAsync();
            var albo_modeli = await dbContext.Model.Where(x => x.Source == "ALBO" && x.Aktivan == true).ToListAsync();
            try
            {
                foreach (var model in albo_modeli)
                {
                    string source_id = model.SourceId;
                    var artikli_result = albo_artikli.Where(x => x.KomercijalniBroj == source_id.Trim()).ToList();
                    if (artikli_result.Count > 0)
                    {
                        foreach (var artikal in artikli_result)
                        {
                            int artikal_id = 0;
                            var artikal_from_db = dbContext.Artikal.Where(x => x.SourceId == artikal.Br).FirstOrDefault();
                            if (artikal_from_db != null)
                            {
                                artikal_id = artikal_from_db.Id;
                            }
                            else
                            {
                                var artikal_for_db = new Artikal()
                                {
                                    SourceId = artikal.Br,
                                    Naziv = artikal.Opis2,
                                    Sifra = artikal.Br,
                                    Barkod = null,
                                    Aktivan = true,
                                    Brand = artikal.Proizvođač,
                                    SourceSifra = artikal.Br,
                                    Source = "ALBO",
                                    ModelId = model.Id,
                                    Color = artikal.NazivBoje,
                                    Size = artikal.NazivVeličine,
                                    Jm = artikal.OsnovnaJedinicaMere
                                };
                                dbContext.Artikal.Add(artikal_for_db);
                                dbContext.SaveChanges();
                                artikal_id = artikal_for_db.Id;
                            }
                            var artikal_detalj_from_db = await dbContext.ArtikalDetalj.Where(x => x.ArtikalId == artikal_id).FirstOrDefaultAsync();
                            if (artikal_detalj_from_db == null)
                            {
                                var artikal_detalj_for_db = new ArtikalDetalj()
                                {
                                    ArtikalId = artikal_id,
                                    Opis = artikal.Opis,
                                    Opis2 = artikal.Opis

                                };
                                dbContext.ArtikalDetalj.Add(artikal_detalj_for_db);
                                dbContext.SaveChanges();
                            }

                            int model_color_id = 0;
                            var model_color_from_db = await dbContext.ModelColor.Where(x => x.ModelId == model.Id && x.Color == artikal.NazivBoje).FirstOrDefaultAsync();
                            if (model_color_from_db != null)
                            {
                                model_color_id = model_color_from_db.Id;
                            }
                            else
                            {
                                var model_color_for_db = new Models.ModelColor()
                                {
                                    ModelId = model.Id,
                                    Color = artikal.NazivBoje

                                };
                                dbContext.ModelColor.Add(model_color_for_db);
                                dbContext.SaveChanges();
                                model_color_id = model_color_for_db.Id;
                            }

                            var model_color_size_from_db = await dbContext.ModelColorSize.Where(x => x.ModelColorId == model_color_id && x.Size == artikal.NazivVeličine).FirstOrDefaultAsync();
                            if (model_color_size_from_db != null)
                            {

                            }
                            else
                            {
                                var model_color_size_for_db = new Models.ModelColorSize()
                                {
                                    ModelColorId = model_color_id,
                                    Size = artikal.NazivVeličine,
                                    ArtikalId = artikal_id

                                };
                                dbContext.ModelColorSize.Add(model_color_size_for_db);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PROMOSOLUTIONS_Sync_Brand()
        {
            var wsBrandovi = await _apiService.PROMOSOLUTIONS_Brand();
            try
            {
                foreach (var item in wsBrandovi)
                {
                    var brand = await dbContext.Brand.Where(x => x.SourceId == item.Id).FirstOrDefaultAsync();
                    if (brand != null)
                    {

                        brand.SourceId = item.Id;
                        dbContext.SaveChanges();

                    }
                    else
                    {
                        Brand brnd = new Brand()
                        {
                            SourceId = item.Id,
                            Image = item.Image,
                            Source = "PROMOSOLUTIONS"
                        };



                        dbContext.Brand.Add(brnd);

                        dbContext.SaveChanges();
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> PROMOSOLUTIONS_Sync_Color()
        {
            var wsColors = await _apiService.PROMOSOLUTIONS_Color();
            try
            {
                foreach (var item in wsColors)
                {
                    var color = await dbContext.Color.Where(x => x.SourceId == item.Id).FirstOrDefaultAsync();
                    if (color != null)
                    {

                        color.SourceId = item.Id;
                        dbContext.SaveChanges();

                    }
                    else
                    {
                        Models.Color color_input = new Models.Color()
                        {
                            SourceId = item.Id,
                            Naziv = item.Name,
                            HtmlColor = item.HtmlColor
                        };



                        dbContext.Color.Add(color_input);

                        dbContext.SaveChanges();
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        public async Task<bool> PROMOSOLUTIONS_SyncGrupeArtikala()
        {

            var wsGrupe = await _apiService.PROMOSOLUTIONS_GrupaArtUsl("");
            try
            {
                foreach (var item in wsGrupe)
                {
                    var grupa = await dbContext.GrupeArtikala.Where(x => x.SourceId == item.Id).FirstOrDefaultAsync();
                    if (grupa != null)
                    {

                        grupa.Naziv = item.Name;

                    }
                    else
                    {
                        GrupeArtikala ga = new GrupeArtikala()
                        {
                            SourceId = item.Id,
                            Naziv = item.Name,
                            Aktivan = false,
                            ParentSourceId = item.Parent,
                            Source = "PROMOSOLUTIONS"
                        };



                        dbContext.GrupeArtikala.Add(ga);
                    }
                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> PROMOSOLUTIONS_SyncGrupeArtikala2()
        {

            var wsGrupe = await dbContext.PublikGrupe.ToListAsync();

            try
            {
                foreach (var item in wsGrupe)
                {
                    var model = await dbContext.Model.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                    if (model != null)
                    {

                        model.Grupa1 = Convert.ToInt32(item.Grupa);

                    }

                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        public async Task<bool> PROMOSOLUTIONS_SyncModela(string source_id)
        {

            try
            {
                if (String.IsNullOrEmpty(source_id))
                {
                    var result = await _apiService.PROMOSOLUTIONS_Modeli();
                    var grupe_from_api = await _apiService.PROMOSOLUTIONS_GrupaArtUsl("");
                    int? grupa1_id = null;
                    int? grupa2_id = null;
                    int? grupa3_id = null;
                    foreach (var item in result)
                    {
                        var model_from_db = dbContext.Model.Where(x => x.SourceId == item.Id && x.Source == "PROMOSOLUTIONS").FirstOrDefault();
                        var group1 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb1).FirstOrDefault();
                        var group2 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb2).FirstOrDefault();
                        var group3 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb3).FirstOrDefault();
                        if (group1 == null)
                        {
                            var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb1).FirstOrDefault();
                            if (grupa_from_api != null)
                            {
                                var grupa_for_db = new GrupeArtikala { SourceId = item.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                dbContext.GrupeArtikala.Add(grupa_for_db);
                                dbContext.SaveChanges();
                                grupa1_id = grupa_for_db.Id;
                            }

                        }
                        else
                        {
                            grupa1_id = group1.Id;
                        }
                        if (group2 == null)
                        {
                            var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb2).FirstOrDefault();
                            if (grupa_from_api != null)
                            {
                                var grupa_for_db = new GrupeArtikala { SourceId = item.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                dbContext.GrupeArtikala.Add(grupa_for_db);
                                dbContext.SaveChanges();
                                grupa2_id = grupa_for_db.Id;
                            }

                        }
                        else
                        {
                            grupa2_id = group2.Id;
                        }
                        if (group3 == null)
                        {
                            var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb3).FirstOrDefault();
                            if (grupa_from_api != null)
                            {
                                var grupa_for_db = new GrupeArtikala { SourceId = item.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                dbContext.GrupeArtikala.Add(grupa_for_db);
                                dbContext.SaveChanges();
                                grupa3_id = grupa_for_db.Id;
                            }

                        }
                        else
                        {
                            grupa3_id = group3.Id;
                        }
                        if (model_from_db != null)
                        {

                            model_from_db.Naziv = item.Name;

                            model_from_db.Grupa1 = grupa1_id;
                            model_from_db.Grupa2 = grupa2_id;
                            model_from_db.Grupa3 = grupa3_id;
                            model_from_db.Aktivan = true;
                            model_from_db.Novo = false;
                            model_from_db.Najprodavaniji = false;

                            //model.Brand = item.Brand;
                            //model.Vpcena = Convert.ToDecimal(item.Price);
                            model_from_db.Source = "PROMOSOLUTIONS";
                            model_from_db.Slika = item.Image;
                            model_from_db.SlikaSource = "extern";
                            model_from_db.Video = item.Video;
                            model_from_db.Description = item.Description;
                            model_from_db.Description2 = item.Description2;
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            Models.Model model_for_db = new Models.Model()
                            {
                                SourceId = item.Id,
                                Naziv = item.Name,

                                Aktivan = true,
                                Novo = false,
                                Najprodavaniji = false,

                                Grupa1 = group1?.Id,
                                Grupa2 = group2?.Id,
                                Grupa3 = group3?.Id,

                                Source = "PROMOSOLUTIONS",
                                Slika = item.Image,
                                SlikaSource = "extern",
                                Video = item.Video,
                                Description = item.Description,
                                Description2 = item.Description2

                            };


                            dbContext.Model.Add(model_for_db);
                            dbContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    var result = await _apiService.PROMOSOLUTIONS_Modeli();
                    var grupe_from_api = await _apiService.PROMOSOLUTIONS_GrupaArtUsl("");
                    foreach (var item in result.Skip(1072))
                    {

                        int? grupa1_id = null;
                        int? grupa2_id = null;
                        int? grupa3_id = null;
                        var model_from_api = await _apiService.PROMOSOLUTIONS_Modeli(item.Id);
                        if (model_from_api != null)
                        {

                            var model_from_db = dbContext.Model.Where(x => x.SourceId == item.Id && x.Source == "PROMOSOLUTIONS").FirstOrDefault();

                            var group1 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb1).FirstOrDefault();
                            var group2 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb2).FirstOrDefault();
                            var group3 = dbContext.GrupeArtikala.Where(x => x.SourceId == item.GroupWeb3).FirstOrDefault();
                            if (group1 == null)
                            {
                                var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb1).FirstOrDefault();
                                if (grupa_from_api != null)
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = grupa_from_api.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa1_id = grupa_for_db.Id;
                                }
                                else
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = item.GroupWeb1, Naziv = "", ParentSourceId = "*", Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa1_id = grupa_for_db.Id;
                                }

                            }
                            else
                            {
                                grupa1_id = group1.Id;
                            }
                            if (group2 == null)
                            {
                                var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb2).FirstOrDefault();
                                if (grupa_from_api != null)
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = grupa_from_api.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa2_id = grupa_for_db.Id;
                                }
                                else
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = item.GroupWeb2, Naziv = "", ParentSourceId = item.GroupWeb1, Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa2_id = grupa_for_db.Id;
                                }

                            }
                            else
                            {
                                grupa2_id = group2.Id;
                            }
                            if (group3 == null)
                            {
                                var grupa_from_api = grupe_from_api.Where(x => x.Id == item.GroupWeb3).FirstOrDefault();
                                if (grupa_from_api != null)
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = grupa_from_api.Id, Naziv = grupa_from_api.Name, ParentSourceId = grupa_from_api.Parent, Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa3_id = grupa_for_db.Id;
                                }
                                else
                                {
                                    var grupa_for_db = new GrupeArtikala { SourceId = item.GroupWeb3, Naziv = "", ParentSourceId = item.GroupWeb2, Aktivan = true, Source = "PROMOSOLUTION" };
                                    dbContext.GrupeArtikala.Add(grupa_for_db);
                                    dbContext.SaveChanges();
                                    grupa3_id = grupa_for_db.Id;
                                }

                            }
                            else
                            {
                                grupa3_id = group3.Id;
                            }
                            if (model_from_db != null)
                            {

                                model_from_db.Naziv = item.Name;

                                model_from_db.Grupa1 = grupa1_id;
                                model_from_db.Grupa2 = grupa2_id;
                                model_from_db.Grupa3 = grupa3_id;
                                model_from_db.Aktivan = true;
                                model_from_db.Novo = false;
                                model_from_db.Najprodavaniji = false;

                                //model.Brand = item.Brand;
                                //model.Vpcena = Convert.ToDecimal(item.Price);
                                model_from_db.Source = "PROMOSOLUTIONS";
                                model_from_db.Slika = item.Image;
                                model_from_db.SlikaSource = "extern";
                                model_from_db.Video = item.Video;
                                model_from_db.Description = item.Description;
                                model_from_db.Description2 = item.Description2;
                                dbContext.SaveChanges();

                                var colors = model_from_api.Colors;
                                foreach (var color in colors)
                                {
                                    var clr_for_dtb = new ModelColor() { Color = color.Name, HtmlColor = color.HtmlColor };
                                    var sizes = color.Sizes;
                                    foreach (var size in sizes)
                                    {
                                        var product = size.Product;
                                        var artikal_From_db = dbContext.Artikal.Where(x => x.SourceId == product.Id).FirstOrDefault();


                                        if (artikal_From_db != null)
                                        {
                                            model_from_db.Brand = artikal_From_db.Brand;
                                            artikal_From_db.ModelId = model_from_db.Id;
                                            var mcs = new ModelColorSize() { Size = size.Id, ArtikalId = artikal_From_db.Id };
                                            clr_for_dtb.ModelColorSize.Add(new ModelColorSize() { Size = size.Id, ArtikalId = artikal_From_db.Id });
                                        }
                                        else
                                        {
                                            var artikal_from_api = await _apiService.PROMOSOLUTIONS_Artikli(product.Id);
                                            if (artikal_from_api != null)
                                            {
                                                var artikal_for_db = new Artikal()
                                                {
                                                    SourceId = artikal_from_api.Id,
                                                    Naziv = artikal_from_api.Name,
                                                    Sifra = artikal_from_api.ProductIdView,
                                                    Barkod = artikal_from_api.EAN,
                                                    Aktivan = true,
                                                    Brand = (artikal_from_api.Brand == null) ? null : artikal_from_api.Brand.Id,
                                                    Vpcena = Convert.ToDecimal(artikal_from_api.Price),
                                                    Source = "PROMOSOLUTIONS",
                                                    ModelId = model_from_db.Id,
                                                    Color = color.Name,
                                                    Size = size.Id
                                                };
                                                model_from_db.Brand = (artikal_from_api.Brand == null) ? null : artikal_from_api.Brand.Id;
                                                dbContext.Artikal.Add(artikal_for_db);
                                                dbContext.SaveChanges();
                                                if (artikal_from_api.Images.Count > 0)
                                                {
                                                    for (var i = 0; i <= artikal_from_api.Images.Count - 1; i++)
                                                    {
                                                        if (i == 0)
                                                        {
                                                            artikal_for_db.Slika = artikal_from_api.Images[i].Image;
                                                            artikal_for_db.SlikaSource = "extern";
                                                        }
                                                        else
                                                        {
                                                            if (!dbContext.ArtikalSlike.Where(x => x.Slika == artikal_from_api.Images[i].Image).Any())
                                                            {
                                                                dbContext.ArtikalSlike.Add(new ArtikalSlike() { ArtikalId = artikal_for_db.Id, Slika = artikal_from_api.Images[i].Image, Source = "extern" });
                                                            }

                                                        }
                                                    }
                                                }
                                                dbContext.SaveChanges();
                                                var artikal_detail = dbContext.ArtikalDetalj.Where(x => x.ArtikalId == artikal_for_db.Id).FirstOrDefault();
                                                if (artikal_detail != null)
                                                {
                                                    if (!artikal_detail.OpisOverride)
                                                    {
                                                        artikal_detail.Opis = artikal_from_api.Model.Description;
                                                        artikal_detail.Opis2 = artikal_from_api.Model.Description2;
                                                    }

                                                }
                                                else
                                                {
                                                    dbContext.ArtikalDetalj.Add(new ArtikalDetalj() { ArtikalId = artikal_for_db.Id, Opis = artikal_from_api.Model.Description, Opis2 = artikal_from_api.Model.Description2, WebshopNaziv = "" });
                                                }

                                                dbContext.SaveChanges();

                                                if (artikal_from_api.Stocks.Count > 0)
                                                {
                                                    decimal stanje = 0;
                                                    foreach (var stock in artikal_from_api.Stocks)
                                                    {
                                                        stanje += Convert.ToDecimal(stock.Qty);
                                                    }
                                                    var artikal_stanje = dbContext.ArtikalStanje.Where(x => x.ArtikalId == artikal_for_db.Id).FirstOrDefault();
                                                    if (artikal_stanje == null)
                                                    {
                                                        dbContext.ArtikalStanje.Add(new ArtikalStanje() { ArtikalId = artikal_for_db.Id, Stanje = stanje });
                                                    }
                                                    else
                                                    {
                                                        artikal_stanje.Stanje = stanje;
                                                    }
                                                }
                                                dbContext.SaveChanges();
                                                var mcs = new ModelColorSize() { Size = size.Id, ArtikalId = artikal_for_db.Id };
                                                clr_for_dtb.ModelColorSize.Add(new ModelColorSize() { Size = size.Id, ArtikalId = artikal_for_db.Id });
                                            }

                                        }

                                    }
                                    model_from_db.ModelColor.Add(clr_for_dtb);
                                }
                                //model_from_db.Description2 = item.Description2;
                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Models.Model model_for_db = new Models.Model()
                                {
                                    SourceId = item.Id,
                                    Naziv = item.Name,

                                    Aktivan = true,
                                    Novo = false,
                                    Najprodavaniji = false,

                                    Grupa1 = group1?.Id,
                                    Grupa2 = group2?.Id,
                                    Grupa3 = group3?.Id,

                                    Source = "PROMOSOLUTIONS",
                                    Slika = item.Image,
                                    SlikaSource = "extern",
                                    Video = item.Video,
                                    Description = item.Description,
                                    Description2 = item.Description2

                                };


                                dbContext.Model.Add(model_for_db);
                                dbContext.SaveChanges();
                            }
                            //if (model_from_db == null)
                            //{

                            //}
                            //if(model_from_db != null) {
                            //    var colors = model_from_api.Colors;
                            //    foreach(var color in colors)
                            //    {
                            //        var clr_for_dtb = new ModelColor() { Color = color.Name, HtmlColor = color.HtmlColor };
                            //        var sizes = color.Sizes;
                            //        foreach(var size in sizes)
                            //        {
                            //            var product = size.Product;
                            //            var artikal_From_db = dbContext.Artikal.Where(x => x.SourceId == product.Id).FirstOrDefault();


                            //            if (artikal_From_db != null)
                            //            {
                            //                model_from_db.Brand = artikal_From_db.Brand;
                            //                artikal_From_db.ModelId = model_from_db.Id;
                            //                var mcs = new ModelColorSize() { Size = size.Id, ArtikalId = artikal_From_db.Id };
                            //                clr_for_dtb.ModelColorSize.Add(new ModelColorSize() { Size = size.Id, ArtikalId = artikal_From_db.Id });
                            //            }

                            //        }
                            //        model_from_db.ModelColor.Add(clr_for_dtb);
                            //    }
                            //    model_from_db.Description2= item.Description2;
                            //    dbContext.SaveChanges();
                            //}

                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public async Task<bool> PROMOSOLUTIONS_SyncArtikala(string source_id)
        {

            try
            {
                if (String.IsNullOrEmpty(source_id))
                {
                    var result = await _apiService.PROMOSOLUTIONS_Artikli();
                    foreach (var item in result)
                    {
                        var model = item.Model;


                        if (dbContext.Artikal.Where(x => x.SourceId == item.Id && x.Source == "PROMOSOLUTIONS").Any())
                        {
                            var artikal = dbContext.Artikal.Where(x => x.SourceId == item.Id && x.Source == "PROMOSOLUTIONS").FirstOrDefault();
                            artikal.Naziv = item.Name;
                            artikal.Sifra = item.ProductIdView;
                            artikal.Barkod = item.EAN;
                            //artikal.Grupa1 = item.Category;
                            //artikal.Grupa2 = item.SubCategory;
                            artikal.Aktivan = true;
                            artikal.Brand = item.Brand;
                            artikal.Vpcena = Convert.ToDecimal(item.Price);
                            artikal.Source = "PROMOSOLUTIONS";
                            artikal.ModelId = null;
                            artikal.IsModel = false;

                            dbContext.SaveChanges();
                        }
                        else
                        {
                            Artikal artikal = new Artikal()
                            {
                                SourceId = item.Id,
                                Naziv = item.Name,
                                Sifra = "",
                                Aktivan = true,
                                Novo = false,
                                Najprodavaniji = false,
                                NaStanju = false,
                                //Grupa1 = item.Category,
                                //Grupa2 = item.SubCategory,
                                Barkod = item.EAN,
                                Vpcena = Convert.ToDecimal(item.Price),
                                Brand = item.Brand,
                                ModelId = null,
                                IsModel = false,
                                Source = "PROMOSOLUTIONS"

                            };


                            dbContext.Artikal.Add(artikal);
                            dbContext.SaveChanges();
                        }
                    }

                    return true;
                }
                else
                {
                    var result = await _apiService.PROMOSOLUTIONS_Artikli(source_id);
                    var artikal = dbContext.Artikal.Where(x => x.SourceId == result.Id && x.Source == "PROMOSOLUTIONS").FirstOrDefault();
                    var model = result.Model;
                    if (artikal != null)
                    {
                        artikal.Naziv = result.Name;
                        artikal.Sifra = result.ProductIdView;
                        artikal.Barkod = result.EAN;
                        //artikal.Grupa1 = result.Category.Id;
                        //artikal.Grupa2 = result.SubCategory.Id;
                        artikal.Aktivan = true;
                        artikal.Brand = (result.Brand == null) ? null : result.Brand.Id;
                        artikal.Vpcena = Convert.ToDecimal(result.Price);
                        artikal.Source = "PROMOSOLUTIONS";
                        if (result.Images.Count > 0)
                        {
                            for (var i = 0; i <= result.Images.Count - 1; i++)
                            {
                                if (i == 0)
                                {
                                    artikal.Slika = result.Images[i].Image;
                                    artikal.SlikaSource = "extern";
                                }
                                else
                                {
                                    if (!dbContext.ArtikalSlike.Where(x => x.Slika == result.Images[i].Image).Any())
                                    {
                                        dbContext.ArtikalSlike.Add(new ArtikalSlike() { ArtikalId = artikal.Id, Slika = result.Images[i].Image, Source = "extern" });
                                    }

                                }
                            }
                        }

                        dbContext.SaveChanges();
                        var detalj = dbContext.ArtikalDetalj.Where(x => x.ArtikalId == artikal.Id).FirstOrDefault();
                        if (detalj != null)
                        {
                            if (!detalj.OpisOverride)
                            {
                                detalj.Opis = result.Model.Description;
                                detalj.Opis2 = result.Model.Description2;
                            }

                        }
                        else
                        {
                            dbContext.ArtikalDetalj.Add(new ArtikalDetalj() { ArtikalId = artikal.Id, Opis = result.Model.Description, Opis2 = result.Model.Description2, WebshopNaziv = "" });
                        }

                        dbContext.SaveChanges();
                        if (result.Size != null)
                        {
                            if (!dbContext.ArtikalSvojstva.Where(x => x.ArtikalId == artikal.Id && x.Vrednost == "Velicina" && x.Vrednost == result.Size.Id).Any())
                            {
                                dbContext.ArtikalSvojstva.Add(new ArtikalSvojstva() { ArtikalId = artikal.Id, Naziv = "Velicina", Vrednost = result.Size.Id });
                            }

                        }
                        if (result.Color != null)
                        {
                            if (!dbContext.ArtikalSvojstva.Where(x => x.ArtikalId == artikal.Id && x.Vrednost == "Boja" && x.Vrednost == result.Color.Id).Any())
                            {
                                dbContext.ArtikalSvojstva.Add(new ArtikalSvojstva() { ArtikalId = artikal.Id, Naziv = "Boja", Vrednost = result.Color.Id });
                            }

                        }
                        if (result.Stocks.Count > 0)
                        {
                            decimal stanje = 0;
                            foreach (var stock in result.Stocks)
                            {
                                stanje += Convert.ToDecimal(stock.Qty);
                            }
                            var artikal_stanje = dbContext.ArtikalStanje.Where(x => x.ArtikalId == artikal.Id).FirstOrDefault();
                            if (artikal_stanje == null)
                            {
                                dbContext.ArtikalStanje.Add(new ArtikalStanje() { ArtikalId = artikal.Id, Stanje = stanje });
                            }
                            else
                            {
                                artikal_stanje.Stanje = stanje;
                            }
                        }
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                //var wsArtikli = await _apiService.PodaciArtikla(sifra, "klasifikacija='D'");
                //var wsArtikli = await _apiService.PROMOSOLUTIONS_Artikli();



            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> PROMOSOLUTIONS_Update()
        {

            try
            {

                var db_artikli = await _modelService.Modeli_Promoslutions();
                foreach (var item in db_artikli)
                {
                    var result = await _apiService.PROMOSOLUTIONS_Modeli(item.SourceId);
                    if (result != null)
                    {
                        var model_detail = await _modelService.Model_Detail(item.Id);
                        if (model_detail != null)
                        {
                            model_detail.Opis2 = result.Description2;
                            if (result.ExtDescr != null)
                            {
                                if (result.ExtDescr.Count > 1)
                                {
                                    string test = "";
                                }
                                else
                                {
                                    model_detail.OpisDetalj = result.ExtDescr[0].Description;
                                }

                            }
                            await _modelService.Model_Detail_Update(model_detail);
                        }
                        else
                        {

                            var model_detail_for_db = new ModelDetail()
                            {
                                ModelId = item.Id,
                                Opis2 = result.Description2
                            };
                            if (result.ExtDescr != null)
                            {
                                if (result.ExtDescr.Count > 1)
                                {
                                    string test = "";
                                }
                                else
                                {
                                    model_detail_for_db.OpisDetalj = result.ExtDescr[0].Description;
                                }

                            }
                            await _modelService.Model_Detail_Insert(model_detail_for_db);

                        }
                        item.Description = result.Description;
                        await _modelService.Model_Update(item);
                    }


                }
                return true;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        private void ProveraModela(string model)
        {
            if (String.IsNullOrEmpty(model))
            {

            }
            else
            {
                var artikal_model = dbContext.Artikal.Where(a => a.IsModel == true && a.SourceId == model).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SyncArtikala(string sifra)
        {


            try
            {
                //var wsArtikli = await _apiService.PodaciArtikla(sifra, "klasifikacija='D'");
                var wsArtikli = await _apiService.PodaciArtikla(sifra, "");
                if (String.IsNullOrEmpty(sifra))
                {
                    dbContext.Database.ExecuteSqlCommand("Update Artikal Set Aktivan=0");
                }
                else
                {
                    string query = "Update Artikal Set Aktivan = 0 Where Sifra = '" + sifra + "'";
                    dbContext.Database.ExecuteSqlCommand(query);
                }
                foreach (var item in wsArtikli.Table)
                {

                    if (dbContext.Artikal.Where(x => x.SourceId == item.ArtikalID).Any())
                    {
                        var artikal = dbContext.Artikal.Where(x => x.SourceId == item.ArtikalID).FirstOrDefault();
                        artikal.Naziv = item.Naziv;
                        artikal.Sifra = item.Sifra;
                        artikal.Barkod = item.Barkod;
                        //artikal.Grupa1 = item.IDgrupe;
                        artikal.Aktivan = true;
                        var artikal_detail = dbContext.ArtikalDetalj.Where(x => x.ArtikalId == artikal.Id).FirstOrDefault();
                        if (artikal_detail == null)
                        {
                            ArtikalDetalj ad = new ArtikalDetalj();
                            ad.Opis = (String.IsNullOrEmpty(item.Opis)) ? "" : item.Opis;
                            ad.ArtikalId = artikal.Id;
                            dbContext.ArtikalDetalj.Add(ad);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(item.Opis))
                            {
                                artikal_detail.Opis = item.Opis;
                            }


                        }
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        Artikal artikal = new Artikal()
                        {
                            SourceId = item.Id,
                            Naziv = item.Naziv,
                            Sifra = item.Sifra,
                            Aktivan = true,
                            Novo = false,
                            Najprodavaniji = false,
                            NaStanju = false,
                            //Grupa1 = item.IDgrupe,
                            Barkod = item.Barkod,

                        };



                        ArtikalDetalj ad = new ArtikalDetalj();
                        ad.Opis = (String.IsNullOrEmpty(item.Opis)) ? "" : item.Opis;
                        ad.ArtikalId = artikal.Id;
                        artikal.ArtikalDetalj.Add(ad);
                        //dbContext.ArtikalDetalj.Add(ad);
                        dbContext.Artikal.Add(artikal);
                        dbContext.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }
        public async Task<bool> LACUNA_SyncGrupeArtikala()
        {

            var wsGrupe = await dbContext.LacunaGrupe.ToListAsync();

            try
            {
                foreach (var item in wsGrupe)
                {
                    var model = await dbContext.Model.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                    if (model != null)
                    {

                        model.Grupa1 = Convert.ToInt32(item.Grupa);

                    }

                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        public async Task<bool> Lacuna_SyncArtikala(string spource_id)
        {
            int counter = 1;
            var artikli = await _apiService.LACUNA_Artikli();
            var grupe = await _artikliService.GrupeArtikala();
            var proizvodi = artikli.proizvodi.Skip(4000);
            foreach (var artikal in proizvodi)
            {
                var artikal_from_db = await dbContext.Artikal.Where(x => x.SourceId == artikal.ID && x.Source == "LACUNA").FirstOrDefaultAsync();
                if (artikal_from_db == null)
                {
                    var artikla_for_db = new Artikal();
                    var artikal_detail_for_db = new ArtikalDetalj();
                    var artikal_atributi_for_db = new List<ArtikalSvojstva>();

                    var db_group = dbContext.GrupeArtikala.Where(x => x.Naziv == artikal.Grupa && x.Source == "LACUNA" && x.Aktivan == true).FirstOrDefault();
                    var db_sub_group = dbContext.GrupeArtikala.Where(x => x.Naziv == artikal.Podgrupa && x.Source == "LACUNA" && x.Aktivan == true).FirstOrDefault();
                    if (db_group != null)
                    {
                        NumberFormatInfo numberFormatWithComma = new NumberFormatInfo();
                        numberFormatWithComma.NumberDecimalSeparator = ",";


                        try
                        {

                            var numb = decimal.Parse(artikal.Cena, numberFormatWithComma);
                            artikla_for_db.Naziv = artikal.Naziv;
                            artikla_for_db.Brand = artikal.Brand;
                            //artikla_for_db.Grupa1 = artikal.Grupa.Trim();
                            //artikla_for_db.Grupa2 = artikal.Podgrupa.Trim();
                            artikla_for_db.Aktivan = true;
                            artikla_for_db.Barkod = artikal.Barkod;
                            artikla_for_db.Novo = false;
                            artikla_for_db.Najprodavaniji = false;
                            artikla_for_db.Jm = artikal.JM;
                            artikla_for_db.Vpcena = numb;
                            artikla_for_db.NaStanju = (artikal.Dostupno == "Da") ? true : false;
                            artikla_for_db.SourceId = artikal.ID;
                            artikla_for_db.SourceSifra = artikal.SifraProizvoda;
                            artikla_for_db.Sifra = artikal.Sifra;
                            artikla_for_db.Source = "LACUNA";
                            artikla_for_db.IsModel = false;
                            if (artikal.Atributi != null)
                            {
                                var boja_from_atributi = artikal.Atributi.Atribut.Where(x => x.Naziv == "BOJA").FirstOrDefault();
                                artikla_for_db.Color = (boja_from_atributi != null) ? boja_from_atributi.Vrednost : "*";
                            }
                            if (!String.IsNullOrEmpty(artikal.Variant))
                            {
                                artikla_for_db.Size = artikal.Variant;
                            }
                            else
                            {
                                artikla_for_db.Size = "*";
                            }
                            if (artikal.SlikaProizvoda != null && artikal.SlikaProizvoda.Count > 0)
                            {
                                artikla_for_db.Slika = artikal.SlikaProizvoda[0].slika;
                                artikla_for_db.SlikaSource = "extern";


                            }
                            var result = dbContext.Artikal.Add(artikla_for_db);
                            try
                            {
                                dbContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }

                            artikal_detail_for_db.Opis = artikal.Opis;
                            artikal_detail_for_db.Opis2 = artikal.Opis;
                            artikal_detail_for_db.ArtikalId = artikla_for_db.Id;
                            var result2 = dbContext.ArtikalDetalj.Add(artikal_detail_for_db);
                            try
                            {
                                dbContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                Lacuna_Slike_Insert_Update(artikal, artikla_for_db);
                            }
                            catch (Exception ex)
                            {

                            }

                            if (artikal.Atributi != null)
                            {
                                if (artikal.Atributi.Atribut.Count > 0)
                                {
                                    foreach (var item in artikal.Atributi.Atribut)
                                    {
                                        artikal_atributi_for_db.Add(new ArtikalSvojstva() { Naziv = item.Naziv, ArtikalId = artikla_for_db.Id, Vrednost = item.Vrednost });

                                    }
                                    dbContext.ArtikalSvojstva.AddRange(artikal_atributi_for_db);
                                    dbContext.SaveChanges();

                                }


                            }
                            var model_for_db = new Models.Model()
                            {
                                Naziv = artikla_for_db.Naziv,
                                Grupa1 = db_group.Id,
                                Grupa2 = db_sub_group?.Id,
                                Grupa3 = null,
                                Aktivan = true,
                                Novo = false,
                                Najprodavaniji = false,
                                Brand = artikla_for_db.Brand,
                                Vpcena = Convert.ToDecimal(artikla_for_db.Vpcena),
                                Source = "LACUNA",
                                Slika = artikla_for_db.Slika,
                                SlikaSource = "extern",
                                Video = null,
                                Description = artikla_for_db.Naziv,
                                SourceId = artikal.ID
                            };


                            var clr_for_dtb = new ModelColor() { Color = artikla_for_db.Color, HtmlColor = null };

                            clr_for_dtb.ModelColorSize.Add(new ModelColorSize() { Size = artikla_for_db.Size, ArtikalId = artikla_for_db.Id });
                            model_for_db.ModelColor.Add(clr_for_dtb);
                            dbContext.Model.Add(model_for_db);
                            dbContext.SaveChanges();

                            var model_detail_for_db = new ModelDetail() { Opis2 = artikal.Opis, OpisDetalj = artikal.Opis, ModelId = model_for_db.Id };

                            dbContext.SaveChanges();
                            artikla_for_db.ModelId = model_for_db.Id;
                            dbContext.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }

                counter++;
            }
            return false;
        }

        public void Lacuna_Slike_Insert_Update(Proizvod artikal, Artikal artikla_for_db)
        {
            var slike_for_db = new List<ArtikalSlike>();
            if (artikal.SlikaProizvoda != null && artikal.SlikaProizvoda.Count > 0)
            {

                foreach (var slika in artikal.SlikaProizvoda)
                {
                    var slika_from_db = dbContext.ArtikalSlike.Where(x => x.Slika == slika.slika && x.ArtikalId == artikla_for_db.Id).FirstOrDefault();
                    if (slika_from_db != null)
                    {
                        slika_from_db.Slika = slika.slika;
                    }
                    else
                    {
                        slike_for_db.Add(new ArtikalSlike() { Slika = slika.slika, ArtikalId = artikla_for_db.Id, Source = "extern" });
                    }


                }
                dbContext.ArtikalSlike.AddRange(slike_for_db);
                dbContext.SaveChanges();

            }
        }
        public void Lacuna_Atributi_Insert_Update() { }


    }
}
