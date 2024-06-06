using Data.Model.Models.Promosolutions;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Data
{

    public class WSHelper : IWSHelper
    {
        public IConfiguration Configuration { get; private set; }

        public WSHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<object> CallWebService(string source, string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool Api = false, bool tekuca = true)
        {
            if (source == "PROMOSOLUTIONS")
            {
                string ServiceUrl = "https://apiv1.promosolution.services/sr-Latin-CS/api/";
                try
                {
                    string token = "";

                    //if (tekuca)
                    //{
                    //    ServiceUrl = Configuration.GetValue<string>("Api:TekucaGodina");
                    //}
                    //else
                    //{
                    //    ServiceUrl = Configuration.GetValue<string>("Api:ProslaGodina");
                    //}

                    var apiKey = "htzviva";
                    var apiPassword = "crisaprIga-pi5ewr90i";
                    var client = new RestClient(ServiceUrl);
                    var request = new RestRequest("Token", Method.Post);

                    request.AddParameter("grant_type", "password");
                    request.AddParameter("username", apiKey);
                    request.AddParameter("password", apiPassword);

                    //make the API request and get the response
                    var response = await client.PostAsync<TokenResponse>(request);

                    var request2 = new RestRequest("sr-Latin-CS/api/Category", Method.Get);

                    client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", response.AccessToken));

                    var response2 = await client.ExecuteAsync<List<GrupaArtUsl>>(request2);

                    //var options = new RestClientOptions("https://apiv1.promosolution.services/");
                    //using var client = new RestClient(options)
                    //{
                    //    Authenticator = new HttpBasicAuthenticator("htzviva", "crisaprIga-pi5ewr90i"),
                    //};
                    //var request = new RestRequest("Token").AddParameter("grant_type", "password");
                    //var response = await client.PostAsync<string>(request);
                    //var client = new RestClient("https://apiv1.promosolution.services/");
                    //var request = new RestRequest(Method.Post);
                    //request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //request.AddParameter("application/x-www-form-urlencoded",
                    //    "grant_type=password&username=htzviva&password=crisaprIga-pi5ewr90i", ParameterType.RequestBody);


                    //string encodedBody = string.Format("grant_type={2}&username={0}&password={1}", "htzviva", "crisaprIga-pi5ewr90i", "password");
                    //request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);

                    //var client = new RestClient("https://apiv1.promosolution.services/sr-Latin-CS/api/");
                    //client.Authenticator = new HttpBasicAuthenticator("htzviva", "crisaprIga-pi5ewr90i");

                    //var request = new RestRequest(restRequest);


                    //foreach (var item in par)
                    //{
                    //    request.AddParameter(item.Key, item.Value);
                    //}
                    //var response = await client.ExecuteAsync(request);



                    //if (Api)
                    //{
                    //    return response;
                    //}
                    //else
                    //{
                    //    if (!IsString)
                    //    {
                    //        var t = obj.GetType();
                    //        var content = response.Content;
                    //        XmlDocument x = new XmlDocument();
                    //        x.LoadXml(content);
                    //        var results = x.GetElementsByTagName("NewDataSet")[0];
                    //        var innerText = (results != null) ? results.InnerXml : "";
                    //        StringReader strReader = new StringReader("<?xml version='1.0'?><NewDataSet xmlns=''>" + innerText + "</NewDataSet>");
                    //        XmlTextReader xmlReader = new XmlTextReader(strReader);
                    //        XmlSerializer xms = new XmlSerializer(t);
                    //        return xms.Deserialize(xmlReader);
                    //    }
                    //    else
                    //    {
                    //        var content = response.Content;
                    //        XmlDocument x = new XmlDocument();
                    //        x.LoadXml(content);
                    //        return x.DocumentElement.InnerText;
                    //    }
                    //}


                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else if (source == "CALCULUS")
            {
                string ServiceUrl = "";
                try
                {
                    //if (tekuca)
                    //{
                    //    ServiceUrl = Configuration.GetValue<string>("Api:TekucaGodina");
                    //}
                    //else
                    //{
                    //    ServiceUrl = Configuration.GetValue<string>("Api:ProslaGodina");
                    //}

                    var client = new RestClient("http://89.216.121.88:85/");

                    var request = new RestRequest(restRequest);


                    foreach (var item in par)
                    {
                        request.AddParameter(item.Key, item.Value);
                    }
                    var response = await client.ExecuteAsync(request);



                    if (Api)
                    {
                        return response;
                    }
                    else
                    {
                        if (!IsString)
                        {
                            var t = obj.GetType();
                            var content = response.Content;
                            XmlDocument x = new XmlDocument();
                            x.LoadXml(content);
                            var results = x.GetElementsByTagName("NewDataSet")[0];
                            var innerText = (results != null) ? results.InnerXml : "";
                            StringReader strReader = new StringReader("<?xml version='1.0'?><NewDataSet xmlns=''>" + innerText + "</NewDataSet>");
                            XmlTextReader xmlReader = new XmlTextReader(strReader);
                            XmlSerializer xms = new XmlSerializer(t);
                            return xms.Deserialize(xmlReader);
                        }
                        else
                        {
                            var content = response.Content;
                            XmlDocument x = new XmlDocument();
                            x.LoadXml(content);
                            return x.DocumentElement.InnerText;
                        }
                    }


                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return null;
        }

        public async Task<object> PromoSolutions_CallWebService(string source, string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool isToken,string token)
        {

            string ServiceUrl = "https://apiv1.promosolution.services/";
            if (!isToken)
            {
                ServiceUrl += "sr-Latin-CS/api/";
                var client = new RestClient(ServiceUrl);
                if (par!=null && par.Count > 0)
                {
                    foreach (var item in par)
                    {

                        restRequest += "/" + item.Value;
                    }
                }
           
                var request = new RestRequest(restRequest, Method.Get);
             
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                try
                {
                    var type = obj.GetType();

                    dynamic d = obj;
                    var response = await client.GetAsync(request);
                   
                    return response.Content;
                }
                catch(Exception ex)
                {
                    return null;
                }
               
               
            }
            else
            {

                var apiKey = "htzviva";
                var apiPassword = "crisaprIga-pi5ewr90i";
                var client = new RestClient(ServiceUrl);
                var request = new RestRequest(restRequest, Method.Post);
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", apiKey);
                request.AddParameter("password", apiPassword);
                try
                {
                    var response = await client.PostAsync<TokenResponse>(request);
                    return response;
                }
                catch(Exception ex)
                {
                    return null;
                }
               
               
            }
          

            
            
        }

        public async Task<object> LACUNA_CallWebService(string source, string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool isToken = false, string token = "")
        {
            string ServiceUrl = "";
            try
            {
                //if (tekuca)
                //{
                //    ServiceUrl = Configuration.GetValue<string>("Api:TekucaGodina");
                //}
                //else
                //{
                //    ServiceUrl = Configuration.GetValue<string>("Api:ProslaGodina");
                //}

                var client = new RestClient("https://vp.lacuna.rs/");

                var request = new RestRequest("exportXML.aspx");


                foreach (var item in par)
                {
                    request.AddParameter(item.Key, item.Value);
                }
                var response = await client.ExecuteAsync(request);
                if (!IsString)
                {
                    var t = obj.GetType();
                    var content = response.Content;
                    XmlDocument x = new XmlDocument();
                    x.LoadXml(content);
                    //var kljuc = x.GetElementsByTagName("kljuc")[0];
                    var results = x.GetElementsByTagName("proizvodi")[0];
                    //var results = sadrzaj.RemoveChild(kljuc);
                    var innerText = (results != null) ? results.InnerXml : "";
                    StringReader strReader = new StringReader("<?xml version='1.0' encoding='UTF-8'?><proizvodi>" + innerText+ "</proizvodi>");
                    XmlTextReader xmlReader = new XmlTextReader(strReader);
                    XmlSerializer xms = new XmlSerializer(t);
                    return xms.Deserialize(xmlReader);
                }
                else
                {
                    var content = response.Content;
                    XmlDocument x = new XmlDocument();
                    x.LoadXml(content);
                    return x.DocumentElement.InnerText;
                }




            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}


