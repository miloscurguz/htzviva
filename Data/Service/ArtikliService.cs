
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Service
{
    public class ArtikliService : IArtikliService
    {

        private readonly IWSCalls _apiService;
        public IQueryable<Artikal> artikli;
        private string _token;
        public ArtikliService(IWSCalls apiService)
        {
            _apiService = apiService;
        }
        //B2BContext dbContext = new B2BContext();
        e003186Context dbContext = new e003186Context();


        public async Task<List<Artikal>> SviArtikliNajprodavaniji()
        {
            var artikli = (from a in dbContext.Artikal
                               //join c in dbContext.Cenovnik on a.Sifra equals c.SifraArtikla
                           where a.Najprodavaniji == true && a.Aktivan == true && a.NaStanju == true

                           select new Artikal
                           {
                               Id = a.Id,
                               Naziv = a.Naziv,
                               //Mpcena = c.Cena,
                               //Grupa1 = a.Grupa1,
                               Sifra = a.Sifra,
                               Najprodavaniji = a.Najprodavaniji,
                               Novo = a.Novo,
                               Aktivan = a.Aktivan,
                               Slika = (String.IsNullOrEmpty(a.Slika)) ? "https://admin.monteks.rs/Artikli/no-image.png" : "https://admin.monteks.rs/Artikli/" + a.Slika.Trim(),
                               NaStanju = a.NaStanju
                           }).Take(10).ToList();


            return artikli;
        }


        public async Task<IQueryable<Artikal>> SviArtikliAdmin(ShopSearchCriteriaModel criteria)
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
                var artikli = dbContext.Artikal.Where(x => (String.IsNullOrEmpty(brand_id) || x.Brand == brand_id) /*&& (grupa_id == "" || x.Grupa1 == grupa_id)*/)
                    .Select(x => new Artikal
                    {
                        Id = x.Id,
                        SourceId = x.SourceId,
                        Naziv = x.Naziv,
                        //Grupa1 = x.Grupa1,
                        Sifra = x.Sifra,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        NaStanju = x.NaStanju
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
                var artikli = dbContext.Artikal.Where(x => x.Naziv.Contains(criteria.Keyword) || x.Sifra.Contains(criteria.Keyword) || x.Barkod.Contains(criteria.Keyword) && (String.IsNullOrEmpty(brand_id) || x.Brand == brand_id)  /*&& (grupa_id == "" || x.Grupa1 == grupa_id)*/)
                    .Select(x => new Artikal
                    {
                        Id = x.Id,
                        Naziv = x.Naziv,
                        //Grupa1 = x.Grupa1,
                        Sifra = x.Sifra,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        NaStanju = x.NaStanju
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

        public async Task<IList<Artikal>> Model_Artikli(int id_m)
        {
            var artikli = await dbContext.Artikal.Where(x => x.ModelId == id_m).ToListAsync();
            return artikli;
        }
        public async Task<IQueryable<Artikal>> SviArtikli(ShopSearchCriteriaModel criteria)
        {
            string grupa_id = "";
            string brand_id = "";
            if (criteria.Group_Id != 0)
            {
                var grupa = dbContext.GrupeArtikala.Where(x => x.Id == Convert.ToInt32(criteria.Group_Id)).FirstOrDefault();
                grupa_id = grupa.SourceId;
            }
            if (String.IsNullOrEmpty(criteria.Keyword))
            {
                var artikli = dbContext.Artikal.Where(x => (String.IsNullOrEmpty(criteria.Brand) || x.Brand == criteria.Brand.Trim()) /*&& (grupa_id == "" || x.Grupa1 == grupa_id)*/ && x.Aktivan == true)
                    .Select(x => new Artikal
                    {
                        Id = x.Id,
                        SourceId = x.SourceId,
                        Naziv = x.Naziv,
                        //Grupa1 = x.Grupa1,
                        Sifra = x.Sifra,
                        Najprodavaniji = x.Najprodavaniji,
                        Novo = x.Novo,
                        Aktivan = x.Aktivan,
                        Slika = x.Slika,
                        NaStanju = x.NaStanju,
                        Popust = x.Popust,
                        FinalnaCena = x.FinalnaCena,
                        Mpcena = x.Mpcena
                    });


                if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                {
                    artikli = artikli.Take(criteria.RecordsByPage.Value);

                }


                return artikli;

            }
            else
            {
                //var artikli = dbContext.Artikal.Where(x => x.Naziv.Contains(criteria.Keyword) || x.Sifra.Contains(criteria.Keyword));
                var artikli = dbContext.Artikal.Where(x => x.Naziv.Contains(criteria.Keyword) || x.Sifra.Contains(criteria.Keyword) || x.Barkod.Contains(criteria.Keyword) && (brand_id == "" || x.Brand == brand_id) /*&& (grupa_id == "" || x.Grupa1 == grupa_id) */)
               .Select(x => new Artikal
               {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   //Grupa1 = x.Grupa1,
                   Sifra = x.Sifra,
                   Najprodavaniji = x.Najprodavaniji,
                   Novo = x.Novo,
                   Aktivan = x.Aktivan,
                   Slika = x.Slika,
                   NaStanju = x.NaStanju,
                   Popust = x.Popust,
                   FinalnaCena = x.FinalnaCena,
                   Mpcena = x.Mpcena

               });



                return artikli;
            }


        }
        public async Task<IQueryable<Artikal>> Pretraga(ShopSearchCriteriaModel criteria)
        {


            //var pGroupId = new SqlParameter("groupId", grupa.CalculusId);
            if (!String.IsNullOrEmpty(criteria.Keyword))
            {
                var artikli = (from a in dbContext.Artikal
                                   //join c in dbContext.Cenovnik on a.Sifra equals c.SifraArtikla
                               where a.Aktivan == true && (a.Naziv.Contains(criteria.Keyword) || a.Sifra.Contains(criteria.Keyword))
                               select new Artikal
                               {
                                   Id = a.Id,
                                   Naziv = a.Naziv,
                                   //Mpcena = c.Cena,
                                   //Grupa1 = a.Grupa1,
                                   Sifra = a.Sifra,
                                   Najprodavaniji = a.Najprodavaniji,
                                   Novo = a.Novo,
                                   Aktivan = a.Aktivan,
                                   Slika = a.Slika
                               });


                if (criteria.RecordsByPage > 0 && criteria.RecordsByPage != null)
                {
                    artikli = artikli.Take(criteria.RecordsByPage.Value);
                }
                //var artikli =  dbContext.Artikal.FromSqlRaw($"EXECUTE Get_Artikli @groupId",pGroupId);
                //var artikli = dbContext.Artikal.Where(x=>x.GrupaId==grupa.CalculusId);
                switch (criteria.SortMode)
                {
                    case 1: return artikli;
                    case 2: return artikli.OrderByDescending(x => x.Mpcena);
                    case 3: return artikli.OrderBy(x => x.Mpcena);
                    case 4: return artikli.OrderByDescending(x => x.Naziv);
                    case 5: return artikli.OrderBy(x => x.Naziv);
                    default: return artikli;

                }


            }
            return null;
        }

        public async Task<ArtikalDetalj> ArtikalDetalj(int? id)
        {
            var detalj = await dbContext.ArtikalDetalj.Where(x => x.ArtikalId == id).FirstOrDefaultAsync();
            return detalj;
        }


        public async Task<Artikal> Artikal_By_Model_Color_Size(int c_id, int s_id)
        {
            var sizes = await dbContext.ModelColorSize.Where(x => x.ModelColorId == c_id && x.Id == s_id).FirstOrDefaultAsync();
            var artikal = await dbContext.Artikal.Where(x => x.Id == sizes.ArtikalId.Value).FirstOrDefaultAsync();
            return artikal;
        }


        public async Task<bool> UpdateD(ArtikalDetalj artikal)
        {
            try
            {
                dbContext.Update(artikal);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<bool> InsertD(ArtikalDetalj artikal)
        {

            try
            {
                dbContext.Add(artikal);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExistsD(int? id)
        {
            return await dbContext.ArtikalDetalj.Where(x => x.ArtikalId == id).AnyAsync();
        }


        public async Task<string> SnimiGlavnuSliku(int id, string path)
        {
            var artikal = await dbContext.Artikal.Where(x => x.Id == id).FirstOrDefaultAsync();
            artikal.Slika = path;
            dbContext.SaveChanges();
            return path;
        }
        public async Task<string> SnimiGlavnuSlikuGrupa(int id, string path)
        {
            var artikal = await dbContext.GrupeArtikala.Where(x => x.Id == id).FirstOrDefaultAsync();
            artikal.Slika = path;
            dbContext.SaveChanges();
            return path;
        }

        public async Task<Artikal> Artikal(int id)
        {
            var artikal = await dbContext.Artikal.Where(x => x.Id == id).FirstOrDefaultAsync();
            return artikal;
        }

        public async Task<double> Artikal_Cena(int id)
        {
            //var artikal = await dbContext.Artikal.Where(x => x.Id == id).FirstOrDefaultAsync();
            //var cena = await dbContext.Cenovnik.Where(x => x.SifraArtikla == artikal.Sifra).FirstOrDefaultAsync();
            //return Convert.ToDouble(cena.Cena.Value);
            return 0;
        }

        public async Task<bool> SnimiDodatnuSliku(ArtikalSlike model)
        {
            await dbContext.ArtikalSlike.AddAsync(model);
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

        public async Task<List<ArtikalSlike>> VratiiDodatneSlike(int id)
        {
            var slike = await dbContext.ArtikalSlike.Where(x => x.ArtikalId == id).ToListAsync();
            return slike;
        }
        public async Task<bool> Artikal_Dodatna_Slika_Delete(int id)
        {
            var slika = await dbContext.ArtikalSlike.Where(x => x.Id == id).FirstOrDefaultAsync();
            try
            {
                dbContext.ArtikalSlike.Remove(slika);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            };
        }

        public async Task<bool> UpdateArtikal(Artikal artikal)
        {
            try
            {
                dbContext.Update(artikal);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Artikal_Detail_Update(ArtikalDetalj detail)
        {
            try
            {
                dbContext.Update(detail);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<List<GrupeArtikala>> GrupeArtikala()
        {
            return await dbContext.GrupeArtikala.Where(x => x.AdminList == true && x.ParentId == 0).OrderByDescending(x => x.Naziv).ToListAsync();
        }

        public async Task<GrupeArtikala> GrupeArtikala(int id)
        {
            GrupeArtikala grupeArtikala = new GrupeArtikala();

            grupeArtikala = await dbContext.GrupeArtikala
                 .FirstOrDefaultAsync(m => m.Id == id);
            return grupeArtikala;
        }
        public async Task<GrupeArtikala> GrupeArtikala(string source_id)
        {
            var grupeArtikala = await dbContext.GrupeArtikala
                 .FirstOrDefaultAsync(m => m.SourceId == source_id);
            return grupeArtikala;
        }
        public async Task<List<GrupeArtikala>> GrupeArtikalaHome()
        {
            var grupeArtikala = await dbContext.GrupeArtikala
                .Where(m => m.Aktivan == true && m.Naslovna == true)
                .OrderBy(m => m.Order).ToListAsync();
            return grupeArtikala;
        }

        public async Task<List<GrupeArtikala>> Pod_Grupe(int g_id)
        {
            var grupeArtikala = await dbContext.GrupeArtikala
                            .Where(m => m.ParentId == g_id)
                            .OrderByDescending(m => m.Order).ToListAsync();
            return grupeArtikala;
        }
        public async Task<GrupeArtikala> Pod_Grupe_By_Naziv(int g_id, string naziv)
        {
            var grupa = await dbContext.GrupeArtikala
                            .Where(m => m.ParentId == g_id && m.Naziv == naziv)
                            .OrderByDescending(m => m.Order).FirstOrDefaultAsync();
            return grupa;
        }
        public async Task<GrupeArtikala> Grupa_Add(GrupeArtikala grupeArtikala)
        {
            await dbContext.GrupeArtikala.AddAsync(grupeArtikala);
            await dbContext.SaveChangesAsync();
            return grupeArtikala;
        }
        public async Task<bool> GrupeUpdate(GrupeArtikala grupeArtikala)
        {
            var grupaDB = dbContext.GrupeArtikala.Where(x => x.Id == grupeArtikala.Id).FirstOrDefault();

            grupaDB.WebshopNaziv = grupeArtikala.WebshopNaziv;
            grupaDB.Aktivan = grupeArtikala.Aktivan;
            grupaDB.Naslovna = grupeArtikala.Naslovna;
            grupaDB.Order = grupeArtikala.Order;
            grupaDB.Slika = grupeArtikala.Slika;
            await dbContext.SaveChangesAsync();
            return true;
        }

        //Brand
        public async Task<List<Brand>> Brand_Get()
        {
            return await dbContext.Brand.OrderBy(x => x.Id).ToListAsync();
        }


        public async Task<bool> AktivirajGrupu(bool artikal, int gId)
        {
            //var result = await dbContext.GrupeArtikala.Where(x => x.Id == gId).FirstOrDefaultAsync();
            //result.Aktivan = true;
            //dbContext.SaveChanges();
            //if (artikal)
            //{
            //    var artikli = await dbContext.Artikal.Where(x => x.Grupa1 == result.SourceId).ToListAsync();
            //    foreach (var item in artikli)
            //    {
            //        item.Aktivan = true;
            //    }
            //    dbContext.SaveChanges();
            //}
            return true;
        }

        public async Task<bool> DeaktivirajGrupu(bool artikal, int gId)
        {
            //var result = await dbContext.GrupeArtikala.Where(x => x.Id == gId).FirstOrDefaultAsync();
            //result.Aktivan = false;
            //dbContext.SaveChanges();
            //if (artikal)
            //{
            //    var artikli = await dbContext.Artikal.Where(x => x.Grupa1 == result.SourceId).ToListAsync();
            //    foreach (var item in artikli)
            //    {
            //        item.Aktivan = false;
            //    }
            //    dbContext.SaveChanges();
            //}
            return true;
        }


        public Order Order_Kreiraj(Order order)
        {
            var nOrder = new Order()
            {
                UserId = order.UserId,
                Datum = order.Datum,
                Status = order.Status,
                Placanje = order.Placanje,
                Napomena = order.Napomena,
                Iznos = order.Iznos,
                Isporuka = order.Isporuka,
                Ukupno = order.Ukupno,
                Referenca = order.Referenca
            };
            dbContext.Order.Add(nOrder);
            dbContext.SaveChanges();
            return nOrder;
        }

        public int Order_Item_Kreiraj(OrderItem item)
        {
            dbContext.OrderItem.Add(item);
            dbContext.SaveChanges();
            return item.Id;
        }
        public List<OrderItem> Order_Item_Get(int oId)
        {
            var items = dbContext.OrderItem.Where(x => x.OrderId == oId).ToList();

            return items;
        }

        public Order Order_Get_Active(int uId)
        {
            var order = dbContext.Order.Where(x => x.Id == uId).FirstOrDefault();

            return order;
        }

        public bool Cart_Clean_After_Order(int oId)
        {
            var items = dbContext.OrderItem.Where(x => x.OrderId == oId).ToList();
            foreach (var record in items)
            {
                dbContext.CartItem.Remove(dbContext.CartItem.Where(x => x.Id == record.CartItemId).FirstOrDefault());

            }
            dbContext.SaveChanges();
            return true;
        }

        public async Task<GrupeArtikala> GrupeArtikalaById(int id)
        {
            return await dbContext.GrupeArtikala.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ArtikalSvojstva>> Artikal_Svojstva(int id)
        {
            return await dbContext.ArtikalSvojstva.Where(x => x.ArtikalId == id).ToListAsync();
        }

        public async Task<ModelColorSize> Artikal_Size_Detail(int a_id)
        {
            return await dbContext.ModelColorSize.Where(x => x.ArtikalId == a_id).FirstOrDefaultAsync();
        }

        public async Task<ArtikalDetalj> Artikal_Detail_New()
        {
            var detail = new ArtikalDetalj();
            await dbContext.ArtikalDetalj.AddAsync(detail);
            dbContext.SaveChanges();
            return detail;
         
        }


    }
}
