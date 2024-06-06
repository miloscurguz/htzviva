using Data.Model.Models;
using Data.Model.Models.Promosolutions;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IWSCalls
    {
        //PROMOSOLUTIONS

        Task<Data.Model.Models.Lacuna.Artikal_Response> LACUNA_Artikli();
        Task<List<Data.Model.Models.Promosolutions.GrupaArtUsl>> PROMOSOLUTIONS_GrupaArtUsl(string id);
        Task<string> PROMOSOLUTIONS_Token_Get();

        Task<string> PROMOSOLUTIONS_Token_Set();
        Task<List<Brand_Response>> PROMOSOLUTIONS_Brand();
        Task<List<Artikal_Response>> PROMOSOLUTIONS_Artikli();
      
        Task<Artikal_Detail_Response> PROMOSOLUTIONS_Artikli(string source_id);
        Task<List<Model_Reponse>> PROMOSOLUTIONS_Modeli();
        Task<Model_Detail_Reponse> PROMOSOLUTIONS_Modeli(string source_id);
        Task<List<Color_Response>> PROMOSOLUTIONS_Color();
        //CALUCLUS

        Task<OrganizacioneJedinice.NewDataSet> PodaciOrgJed();
        Task<PodaciArtikla.NewDataSet> PodaciArtikla(string sifra,string uslov="");
        Task<Data.Model.Models.GrupaArtUsl.NewDataSet> GrupaArtUsl();
  
        Task<CenovnikArtUsl.NewDataSet> CenovnikArtUsl(string tip, string naDan);
        Task<TipoviCenovnika.NewDataSet> TipoviCenovnika();
        Task<string> UbaciKomitenta(Data.Models.User komitent, Data.Models.Adresa adresa);
        Task<string> UbaciZagDok(ZagDokUnos dokument);
        Task<string> UbaciStavDok(StavkaDokumenta stavka, string magacin, string kjucDokumenta);
        Task<StanjeArtikla.NewDataSet> StanjeArtikla(string sifraMag, string sifraArt, string nazivArt);

       
    }
}
