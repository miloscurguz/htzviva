
using Data.Model.Models.Promosolutions;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IArtikliService
    {
        //PROMOSOLUTIONS
       
        public Task<Artikal> Artikal(int id);
        Task<ModelColorSize> Artikal_Size_Detail(int a_id);



        Task<IList<Artikal>> Model_Artikli(int id_m);
        public Task<IQueryable<Artikal>> SviArtikli(ShopSearchCriteriaModel criteria);
        public Task<IQueryable<Artikal>> SviArtikliAdmin(ShopSearchCriteriaModel criteria);
       
        Task<IQueryable<Artikal>> Pretraga(ShopSearchCriteriaModel criteria);
      
        public Task<List<Artikal>> SviArtikliNajprodavaniji();
        public Task<ArtikalDetalj> ArtikalDetalj(int? id);
        Task<Artikal> Artikal_By_Model_Color_Size(int c_id, int s_id);
        Task<ArtikalDetalj> Artikal_Detail_New();
        public Task<bool> Artikal_Detail_Update(ArtikalDetalj detail);
        public Task<List<ArtikalSvojstva>> Artikal_Svojstva(int id);
      
        public Task<bool> UpdateArtikal(Artikal artikal);
        public Task<bool> UpdateD(ArtikalDetalj artikal);
        public Task<bool> InsertD(ArtikalDetalj artikal);
        public Task<bool> ExistsD(int? id);

   
        Task<double> Artikal_Cena(int id);
        public Task<GrupeArtikala> GrupeArtikalaById(int id);
        public Task<List<GrupeArtikala>> GrupeArtikala();
        public Task<GrupeArtikala> GrupeArtikala(int id);
        public Task<GrupeArtikala> GrupeArtikala(string source_id);
        Task<GrupeArtikala> Grupa_Add(GrupeArtikala grupeArtikala);
        Task<List<GrupeArtikala>> GrupeArtikalaHome();
        Task<List<GrupeArtikala>> Pod_Grupe(int g_id);
        Task<GrupeArtikala> Pod_Grupe_By_Naziv(int g_id, string naziv);
        Task<bool> GrupeUpdate(GrupeArtikala grupeArtikala);

        Task<List<Brand>> Brand_Get();
        
        Task<bool> AktivirajGrupu(bool artikal, int gId);
        Task<bool> DeaktivirajGrupu(bool artikal, int gId);

        Order Order_Kreiraj(Order order);
        int Order_Item_Kreiraj(OrderItem item);
        List<OrderItem> Order_Item_Get(int oId);
        Order Order_Get_Active(int uId);
        bool Cart_Clean_After_Order(int oId);

        //Slike
        public Task<string> SnimiGlavnuSliku(int id, string path);
        public Task<bool> SnimiDodatnuSliku(ArtikalSlike model);
        Task<string> SnimiGlavnuSlikuGrupa(int id, string path);
        public Task<List<ArtikalSlike>> VratiiDodatneSlike(int id);

        public Task<bool> Artikal_Dodatna_Slika_Delete(int id);




    }
}
