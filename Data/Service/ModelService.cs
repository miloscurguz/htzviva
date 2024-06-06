using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
 
    public class ModelService : IModelService
    {
       
        //B2BContext dbContext = new B2BContext();
        e003186Context dbContext = new e003186Context();
        public async Task<List<Models.Model>> SviModeliNovi()
        {
            var artikli = (from x in dbContext.Model
                           where x.Novo == true && x.Aktivan == true
                           select new Models.Model
                           {
                               Id = x.Id,
                               SourceId = x.SourceId,
                               Naziv = x.Naziv,
                               //Grupa1 = x.Grupa1,
                               //Sifra = x.Sifra,
                               Brand = x.Brand,
                               Najprodavaniji = x.Najprodavaniji,
                               Novo = x.Novo,
                               Aktivan = x.Aktivan,
                               Slika = x.Slika,
                               Description = x.Description,
                               //NaStanju = x.NaStanju,
                               //Popust = x.Popust,
                               FinalnaCena = x.FinalnaCena,
                               FinalnaCenaPdv = x.FinalnaCenaPdv,
                               Mpcena = x.Mpcena
                           }).Take(10).ToList();
            return artikli;
        }
        public async Task<List<Models.Model>> Modeli_Promoslutions()
        {
            var artikli = await dbContext.Model.Where(x => x.Aktivan == true && x.Source == "PROMOSOLUTIONS").ToListAsync();
            return artikli;
        }

        public async Task<List<Models.Model>> Modeli_Lacuna()
        {
            var artikli = await dbContext.Model.Where(x => x.Aktivan == true && x.Source == "LACUNA").ToListAsync();
            return artikli;
        }
        public async Task<IQueryable<Models.Model>> SviModeliAdmin(ShopSearchCriteriaModel criteria)
        {
            string grupa_id = "";
            string brand_id = "";
            if (criteria.Group_Id != 0)
            {
                var grupa = dbContext.GrupeArtikala.Where(x => x.Id == Convert.ToInt32(criteria.Group_Id)).FirstOrDefault();

                grupa_id = grupa.SourceId;

            }
            if (!String.IsNullOrEmpty(criteria.Brand))
            {
                var brand = dbContext.Brand.Where(x => x.Id == Convert.ToInt32(criteria.Brand)).FirstOrDefault();

                brand_id = brand.SourceId;

            }


            //var pGroupId = new SqlParameter("groupId", grupa.CalculusId);
            if (String.IsNullOrEmpty(criteria.Keyword))
            {
                var artikli = dbContext.Model.Where(x => (String.IsNullOrEmpty(brand_id) || x.Brand == brand_id)  /*&& (grupa_id == "" || x.Grupa1 == grupa_id)*/)
                    .Select(x => new Models.Model
                    {
                        Id = x.Id,
                        SourceId = x.SourceId,
                        Naziv = x.Naziv,
                        Description = x.Description,
                        //Grupa1 = x.Grupa1,
                        //Sifra = x.Sifra,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        //NaStanju = x.NaStanju
                    });

                //var test = artikli.ToList();

                if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                {
                    artikli = artikli.Take(criteria.RecordsByPage.Value);

                }

                return artikli;


            }
            else
            {
                var artikli = dbContext.Model.Where(x => x.Naziv.Contains(criteria.Keyword) || x.Description.Contains(criteria.Keyword) && (String.IsNullOrEmpty(brand_id) || x.Brand == brand_id) /*&& (grupa_id == "" || x.Grupa1 == grupa_id)*/)
                    .Select(x => new Models.Model
                    {
                        Id = x.Id,
                        Naziv = x.Naziv,
                        SourceId = x.SourceId,
                        //Grupa1 = x.Grupa1,
                        //Sifra = x.Sifra,
                        Description = x.Description,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        //NaStanju = x.NaStanju
                    });

                if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                {
                    artikli = artikli.Take(criteria.RecordsByPage.Value);

                }

                if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                {
                    artikli = artikli.Take(criteria.RecordsByPage.Value);

                }
                return artikli;
            }


        }
        public async Task<IQueryable<Models.Model>> SviModeli(ShopSearchCriteriaModel criteria)
        {
            int grupa_id = criteria.Group_Id;
            int sub_grupa_id = criteria.Sub_Group_Id;
            string brand_id = "";
            bool search_by_sub_category = false;
            List<int?> grupe_splited = null;
            if (sub_grupa_id != 0)
            {
                search_by_sub_category = true;
            }
            else
            {
                if (grupa_id != 0)
                {

                    search_by_sub_category = false;
                    //grupa_id = grupa.Id;

                }
            }

            //if (!String.IsNullOrEmpty(criteria.Brand_Id))
            //{
            //    var brand = dbContext.Brand.Where(x => x.Id == Convert.ToInt32(criteria.Brand_Id)).FirstOrDefault();

            //    brand_id = brand.SourceId;

            //}


            //var pGroupId = new SqlParameter("groupId", grupa.CalculusId);
            if (String.IsNullOrEmpty(criteria.Keyword))
            {
                var modeli = dbContext.Model.
                    Where(x => x.Aktivan == true &&
                    (String.IsNullOrEmpty(criteria.Brand) ||
                    x.Brand == criteria.Brand)
                    && (grupa_id == 0 || x.Grupa1 == grupa_id)
                    && (sub_grupa_id == 0 || x.Grupa2 == sub_grupa_id))
                    .Select(x => new Models.Model
                    {
                        Id = x.Id,
                        SourceId = x.SourceId,
                        Naziv = x.Naziv,
                        //Grupa1 = x.Grupa1,
                        //Sifra = x.Sifra,
                        Brand = x.Brand,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        Description = x.Description,
                        //NaStanju = x.NaStanju,
                        //Popust = x.Popust,
                        FinalnaCena = x.FinalnaCena,
                        FinalnaCenaPdv = x.FinalnaCenaPdv,
                        Mpcena = x.Mpcena
                    });
                switch (criteria.SortMode)
                {
                    case 1: return modeli.OrderBy(x => x.Description);
                    case 2: return modeli.OrderByDescending(x => x.Description);
                    case 3: return modeli.OrderBy(x => x.FinalnaCena);
                    case 4: return modeli.OrderByDescending(x => x.FinalnaCena);
                    default: return modeli.OrderBy(x => x.Description);
                }



            }
            else
            {
                var queryWords = criteria.Keyword.Split();

                //var artikli = dbContext.Artikal.Where(x => x.Naziv.Contains(criteria.Keyword) || x.Sifra.Contains(criteria.Keyword));
                var modeli = dbContext.Model.
                    Where(x => x.Aktivan == true &&
                    (
                    x.Naziv.Contains(criteria.Keyword) ||
                    x.Description.Contains(criteria.Keyword) ||
                    x.Tag.Contains(criteria.Keyword) ||
                    x.Brand.Contains(criteria.Keyword))
                    && (grupa_id == 0 || x.Grupa1 == grupa_id)
                    && (sub_grupa_id == 0 || x.Grupa2 == sub_grupa_id))
               .Select(x => new Models.Model
               {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   //Grupa1 = x.Grupa1,
                   //Sifra = x.Sifra,
                   Najprodavaniji = x.Najprodavaniji,
                   Novo = x.Novo,
                   Aktivan = x.Aktivan,
                   Slika = x.Slika,
                   Description = x.Description,
                   Brand = x.Brand,
                   //NaStanju = x.NaStanju,
                   //Popust = x.Popust,
                   FinalnaCena = x.FinalnaCena,
                   FinalnaCenaPdv = x.FinalnaCenaPdv,
                   Mpcena = x.Mpcena

               });
                switch (criteria.SortMode)
                {
                    case 0: modeli.OrderBy(x => x.Naziv); break;
                    case 1: modeli.OrderByDescending(x => x.Naziv); break;
                    case 2: modeli.OrderBy(x => x.FinalnaCena); break;
                    case 3: modeli.OrderByDescending(x => x.FinalnaCena); break;
                }

                //if (!String.IsNullOrEmpty(criteria.Group_Id))
                //{
                //    var grupa = dbContext.GrupeArtikala.Where(x => x.SourceId == criteria.Group_Id).FirstOrDefault();

                //    artikli = artikli.Where(a => a.GrupaId == grupa.SourceId);

                //}

                //if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                //{
                //    modeli = modeli.Take(criteria.RecordsByPage.Value);

                //}
                return modeli;
            }


        }
        public async Task<ModelDetail> Model_Detail(int? id)
        {
            var detalj = await dbContext.ModelDetail.Where(x => x.ModelId == id).FirstOrDefaultAsync();
            return detalj;
        }
        public async Task<List<ModelColorSize>> Model_Color_Sizes(int id)
        {
            var sizes = await dbContext.ModelColorSize.Where(x => x.ModelColorId == id).OrderBy(x => x.Size).ToListAsync();
            return sizes;
        }
        public async Task<ModelColor> Model_Color_By_Artikal(int a_id)
        {
            var size = await dbContext.ModelColorSize.Where(x => x.ArtikalId == a_id).FirstOrDefaultAsync();
            var color = await dbContext.ModelColor.Where(x => x.Id == size.ModelColorId).FirstOrDefaultAsync();
            return color;
        }
        public async Task<List<ModelColor>> Model_Colors_By_Model(int m_id)
        {
            var colors = await dbContext.ModelColor.Where(x => x.ModelId == m_id).ToListAsync();
            return colors;
        }
        public async Task<bool> Model_Detail_Update(ModelDetail model_detail)
        {
            try
            {
                dbContext.Update(model_detail);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public async Task<ModelDetail> Model_Detail_Insert(ModelDetail model_detail)
        {
            try
            {
                await dbContext.AddAsync(model_detail);
                dbContext.SaveChanges();
                return model_detail;
            }
            catch (Exception ex)
            {
                string error_msg = ex.Message.ToString();
                return null;
            }
        }
        public async Task<Models.Model> Model(int id)
        {
            var model = await dbContext.Model.Include(x => x.ModelColor).Where(x => x.Id == id).FirstOrDefaultAsync();
            foreach (var item in model.ModelColor)
            {
                var sizes = await dbContext.ModelColorSize.Where(x => x.ModelColorId == item.Id).ToListAsync();
                item.ModelColorSize = sizes;
            }
            //string zemlja_code = model.ZemljaPorekla;
            //Models.Zemlje ZemljaPorekla = await dbContext.Zemlje.Where(x => x.Code == zemlja_code).FirstOrDefaultAsync();
            //if (ZemljaPorekla == null)
            //{
            //    model.ZemljaPorekla = "";
            //}
            //else
            //{
            //    model.ZemljaPorekla = ZemljaPorekla.Naziv;
            //}

            return model;


        }
        public async Task<bool> Model_Update(Models.Model model)
        {
            try
            {
                dbContext.Update(model);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Model_Delete(int id)
        {
            var model = await dbContext.Model.Where(x => x.Id == id).FirstOrDefaultAsync();

            dbContext.Model.Remove(model);
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> Model_Detail_Exist(int? id)
        {
            return await dbContext.ModelDetail.Where(x => x.ModelId == id).AnyAsync();
        }
        public async Task<bool> Model_Size_Delete(int model_id)
        {
            var model_color = await dbContext.ModelColor.Where(x => x.ModelId == model_id).FirstOrDefaultAsync();
            var model_color_size = await dbContext.ModelColorSize.Where(x => x.ModelColorId == model_color.Id).FirstOrDefaultAsync();
            dbContext.ModelColorSize.Remove(model_color_size);
            dbContext.ModelColor.Remove(model_color);
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
