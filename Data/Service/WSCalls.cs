using Data.Model.Models;
using Data.Model.Models.Promosolutions;
using Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public class WSCalls : IWSCalls
    {
        private readonly IWSHelper _wsHepler;
        private readonly IAuthService _authService;
        public WSCalls(IWSHelper wsHepler, IAuthService authService)
        {
            _wsHepler = wsHepler;
            _authService = authService;
        }
        public async Task<OrganizacioneJedinice.NewDataSet> PodaciOrgJed()
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("sifra", "");
            wsParams.Add("naziv", "");
            wsParams.Add("ID", "");
            wsParams.Add("tip", "");
            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/PodaciOrgJed", wsParams, new OrganizacioneJedinice.NewDataSet(), false, false, true);
            return (OrganizacioneJedinice.NewDataSet)result;

        }
        public async Task<StanjeArtikla.NewDataSet> StanjeArtikla(string sifraMag, string sifraArt, string nazivArt)
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("sifmag", sifraMag);
            wsParams.Add("tipcen", "");
            wsParams.Add("valcen", "");
            wsParams.Add("ojcen", "");
            wsParams.Add("sifgrart", "");
            wsParams.Add("sifart", String.IsNullOrEmpty(sifraArt) ? "" : sifraArt);
            wsParams.Add("nazivart", String.IsNullOrEmpty(nazivArt) ? "" : nazivArt);
            wsParams.Add("zr", "");
            wsParams.Add("sort", "");
            wsParams.Add("uslov", "");
            wsParams.Add("stanje0", "");
            wsParams.Add("datnab", "");
            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/StanjeArtikla", wsParams, new StanjeArtikla.NewDataSet(), false, false, true);
            return (StanjeArtikla.NewDataSet)result;

        }

        public async Task<PodaciArtikla.NewDataSet> PodaciArtikla(string sifra, string uslov = "")
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("grupa", "");
            wsParams.Add("sifra", (String.IsNullOrEmpty(sifra)) ? "" : sifra);
            wsParams.Add("naziv", "");
            wsParams.Add("barkod", "");
            wsParams.Add("serbr", "");
            wsParams.Add("nazivsvojstva", "");
            wsParams.Add("vredsvoj", "");
            wsParams.Add("sort", "");
            wsParams.Add("uslov", uslov);
            wsParams.Add("jezik", "");
            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/PodaciArtikla", wsParams, new PodaciArtikla.NewDataSet(), false, false, true);
            return (PodaciArtikla.NewDataSet)result;
        }

        public async Task<Data.Model.Models.GrupaArtUsl.NewDataSet> GrupaArtUsl()
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("artusl", "A");
            wsParams.Add("klasifikacija", "");
            wsParams.Add("sifra", "");
            wsParams.Add("naziv", "");
            wsParams.Add("nivo", "");
            wsParams.Add("jezik", "");

            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/GrupaArtUsl", wsParams, new Data.Model.Models.GrupaArtUsl.NewDataSet(), false, false, true);
            return (Data.Model.Models.GrupaArtUsl.NewDataSet)result;
        }

        public async Task<TipoviCenovnika.NewDataSet> TipoviCenovnika()
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("sifra", "");
            wsParams.Add("naziv", "");


            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/TipoviCenovnika", wsParams, new TipoviCenovnika.NewDataSet(), false, false, true);
            return (TipoviCenovnika.NewDataSet)result;
        }

        public async Task<CenovnikArtUsl.NewDataSet> CenovnikArtUsl(string tip, string naDan)
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("tipcen", tip);
            wsParams.Add("valcen", "");
            wsParams.Add("ojcen", "");
            wsParams.Add("sifgrart", "");
            wsParams.Add("sifart", "");
            wsParams.Add("nazivart", "");
            wsParams.Add("valprikaz", "");
            wsParams.Add("nadan", naDan);

            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/CenovnikArtUsl", wsParams, new CenovnikArtUsl.NewDataSet(), false, false, true);
            return (CenovnikArtUsl.NewDataSet)result;
        }

        public async Task<string> UbaciKomitenta(Data.Models.User komitent, Adresa adresa)
        {

            var wsParams = new Dictionary<string, string>();
            wsParams.Add("sifra", "");
            wsParams.Add("naziv", komitent.FirstName + " " + komitent.LastName);
            wsParams.Add("pib", "");
            wsParams.Add("adresa", adresa.AdresaPl);
            wsParams.Add("postbr", adresa.PbrojPl);
            wsParams.Add("mesto", adresa.GradPl);
            wsParams.Add("tel", adresa.Telefon1Pl);
            wsParams.Add("fax", "");
            wsParams.Add("mail", komitent.Email);
            wsParams.Add("tekuci", "");
            wsParams.Add("rabatk", "");
            wsParams.Add("rabatd", "");
            wsParams.Add("rokplacanjakupcu", "");
            wsParams.Add("pravnofizicko", "F");

            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/UbaciKomitenta", wsParams, null, true, false, true);
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }

        }


        public async Task<string> UbaciZagDok(ZagDokUnos dokument)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            DateTime tempDate = Convert.ToDateTime(dokument.datum, culture).AddDays(1);
            Dictionary<string, string> param = new Dictionary<string, string>();
            string datumT = tempDate.ToString("yyyy-MM-dd");
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("vrstadokumenta", dokument.vrstaDokumenta);
            wsParams.Add("datum", datumT);
            wsParams.Add("komitentID", dokument.komitentID);
            wsParams.Add("magacinID", dokument.magacinID);
            wsParams.Add("kreatorID", dokument.kreatorID);
            wsParams.Add("agentID", dokument.agentID);
            wsParams.Add("datumprometa", datumT);
            wsParams.Add("valuta", dokument.valuta);
            wsParams.Add("valutaPlacanja", datumT);
            wsParams.Add("napomena", dokument.napomena);
            wsParams.Add("ekstdok1", dokument.ekstdok1);
            wsParams.Add("ekstdok2", dokument.ekstdok2);
            wsParams.Add("ekstdok3", dokument.ekstdok3);
            wsParams.Add("status", dokument.status);
            wsParams.Add("prokmag", dokument.prokmag);
            wsParams.Add("prokknj", dokument.prokknj);
            wsParams.Add("zavrsen", dokument.zavrsen);
            wsParams.Add("statusdok", dokument.statusdok);
            wsParams.Add("nacinisporuke", dokument.nacinisporuke);
            wsParams.Add("poslat", dokument.poslat);
            wsParams.Add("delbroj", dokument.delbroj);
            wsParams.Add("magacioner", dokument.magacioner);
            wsParams.Add("poslao", dokument.poslao);
            wsParams.Add("porn", dokument.porn);
            wsParams.Add("poug", dokument.poug);
            wsParams.Add("nacpl", dokument.nacpl);
            wsParams.Add("idloyaltykartice", "");

            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/UbaciZagDokID", wsParams, null, true, false, true);
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }

        }

        public async Task<string> UbaciStavDok(StavkaDokumenta stavka, string magacin, string kjucDokumenta)
        {
            var wsParams = new Dictionary<string, string>();
            wsParams.Add("kljucdok", kjucDokumenta);
            wsParams.Add("magacin", magacin);
            wsParams.Add("artusl", "A");
            wsParams.Add("sifartusl", "");
            wsParams.Add("nazartusl", stavka.NazArtUsl);
            wsParams.Add("tarifa", "PDVOS");
            wsParams.Add("kolicina", stavka.UnetaKolicina.ToString());
            wsParams.Add("napomena", "");
            wsParams.Add("poruceno", "");
            wsParams.Add("nabcena", "");
            wsParams.Add("vpcena", "");
            wsParams.Add("mpcena", (stavka.MPCena == null) ? "" : stavka.MPCena);
            wsParams.Add("popust", "");
            wsParams.Add("poulazu", "");
            wsParams.Add("kb", "");
            wsParams.Add("rabatk", "");
            wsParams.Add("popustk", "");
            wsParams.Add("lomk", "");
            wsParams.Add("rabatd", "");
            wsParams.Add("popustd", "");
            wsParams.Add("lomd", "");
            wsParams.Add("cenovniksk", "");
            wsParams.Add("zaproizvod", "");
            var result = await _wsHepler.CallWebService("CALCULUS", "CalculusWebService.asmx/UbaciStavDok", wsParams, null, true, false, true);
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }

        }

        //PROMOSOLUTIONS
        public async Task<List<Data.Model.Models.Promosolutions.GrupaArtUsl>> PROMOSOLUTIONS_GrupaArtUsl(string id)
        {
            string token = await PROMOSOLUTIONS_Token_Get();
           
            if (token == null)
            {
                token=await PROMOSOLUTIONS_Token_Set();
            }
            
            var wsParams = new Dictionary<string, string>
            {
                { "id", id },
          
            };
          
            var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Category", wsParams, new Data.Model.Models.Promosolutions.GrupaArtUsl(), false, false, token);
          
            return JsonConvert.DeserializeObject<List<Data.Model.Models.Promosolutions.GrupaArtUsl>>(result.ToString());
        }

        public async Task<string> PROMOSOLUTIONS_Token_Get()
        {
            var token = await _authService.Promosolutions_Token_Get();
            if (token != null) {
                if (token.Expires <= DateTime.Now)
                {
                    return null;
                }
                else
                {
                    return token.AccessToken;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string> PROMOSOLUTIONS_Token_Set()
        {
            var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Token", null, new Data.Model.Models.Promosolutions.TokenResponse(), false, true, "");
            TokenResponse tokenModel = (Data.Model.Models.Promosolutions.TokenResponse)result;
            var token = await _authService.Promosolutions_Token_Set(tokenModel);
            return token.AccessToken;
        }

        public async Task<List<Brand_Response>> PROMOSOLUTIONS_Brand()
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }
            var wsParams = new Dictionary<string, string>
            {
                { "id", "" },

            };

            var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Brand", wsParams, new Data.Model.Models.Promosolutions.Brand_Response(), false, false, token);
            return JsonConvert.DeserializeObject<List<Brand_Response>>(result.ToString());
            //return (List<Data.Model.Models.Promosolutions.Brand_Response>)result;
        }

        public async Task<List<Artikal_Response>> PROMOSOLUTIONS_Artikli()
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }
     
            try
            {
                var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Product", null, new Data.Model.Models.Promosolutions.Brand_Response(), false, false, token);

                return JsonConvert.DeserializeObject<List<Artikal_Response>>(result.ToString());
            }
            catch(Exception ex)
            {
               return null;
            }
            
            //return (List<Data.Model.Models.Promosolutions.Brand_Response>)result;
        }

        public async Task<List<Model_Reponse>> PROMOSOLUTIONS_Modeli()
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }

            try
            {
                var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Model", null, new Data.Model.Models.Promosolutions.Model_Reponse(), false, false, token);

                return JsonConvert.DeserializeObject<List<Model_Reponse>>(result.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Model_Detail_Reponse> PROMOSOLUTIONS_Modeli(string source_id)
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }
            var wsParams = new Dictionary<string, string>
            {
                { "id", source_id },

            };
            try
            {
                var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Model", wsParams, new Data.Model.Models.Promosolutions.Model_Detail_Reponse(), false, false, token);
                if(result == null)
                {
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<Model_Detail_Reponse>(result.ToString());
                }
            
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<Artikal_Detail_Response> PROMOSOLUTIONS_Artikli(string source_id)
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }
            var wsParams = new Dictionary<string, string>
            {
                { "id", source_id },

            };
            try
            {
                var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Product", wsParams, new Data.Model.Models.Promosolutions.Brand_Response(), false, false, token);
                if (result != null)
                {
                    return JsonConvert.DeserializeObject<Artikal_Detail_Response>(result.ToString());
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }

            //return (List<Data.Model.Models.Promosolutions.Brand_Response>)result;
        }
        public async Task<List<Color_Response>> PROMOSOLUTIONS_Color()
        {
            string token = await PROMOSOLUTIONS_Token_Get();

            if (token == null)
            {
                token = await PROMOSOLUTIONS_Token_Set();
            }
            var wsParams = new Dictionary<string, string>
            {
                { "id", "" },

            };

            var result = await _wsHepler.PromoSolutions_CallWebService("PROMOSOLUTIONS", "Color", wsParams, new Data.Model.Models.Promosolutions.Color_Response(), false, false, token);
            return JsonConvert.DeserializeObject<List<Color_Response>>(result.ToString());
        }

        public async Task<Data.Model.Models.Lacuna.Artikal_Response> LACUNA_Artikli()
        {
            var wsParams = new Dictionary<string, string>();
           
            var result = await _wsHepler.LACUNA_CallWebService("LACUNA", "CalculusWebService.asmx/PodaciOrgJed", wsParams, new Data.Model.Models.Lacuna.Artikal_Response(), false, false);
            return (Data.Model.Models.Lacuna.Artikal_Response)result;
        }

        
    }
}
