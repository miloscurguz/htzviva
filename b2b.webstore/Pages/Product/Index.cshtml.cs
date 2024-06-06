using Data.Models;
using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using viva.webstore.Models;
using viva.webstore.Models.Product;

namespace viva.webstore.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IModelService _modelService;
        private readonly IArtikliService _artikliService;
        private readonly ICartService _cartService;
        [BindProperty] public Artikal_Detail_VM Detail { get; set; }
        public IndexModel(IModelService modelService, IArtikliService artikliService, ICartService cartService)
        {
            _modelService = modelService;
            _artikliService = artikliService;
            _cartService = cartService;
        }

        public async Task OnGet(int id, bool model)
        {

            CheckCartCookie();
            string opis1 = "";
            string opis2 = "";
            string model_opis2 = "";
            string model_opis_detalj = "";
            string ops_detaljan = "";
            if (model)
            {
                var current_color_id = 0;
                var current_size = "";
                var current_size_id = 0;
                bool single_size = true;
                var model_from_db = await _modelService.Model(id);
                if (model_from_db != null)
                {
                    var model_detail_from_db = await _modelService.Model_Detail(id);
                    if (model_detail_from_db != null)
                    {
                        model_opis2 = model_detail_from_db.Opis2;
                        model_opis_detalj = model_detail_from_db.OpisDetalj;
                    }
                    current_color_id = model_from_db.ModelColor.ToList()[0].Id;
                    current_size = model_from_db.ModelColor.ToList()[0].ModelColorSize.ToList()[0].Size;
                    if (model_from_db.ModelColor.ToList()[0].ModelColorSize.ToList().Count > 1)
                    {
                        single_size = false;
                    }
                    foreach (var item in model_from_db.ModelColor)
                    {
                        current_color_id = item.Id;
                        foreach (var item2 in item.ModelColorSize)
                        {
                            current_size = item2.Size;
                            current_size_id = item2.Id;
                            id = item2.ArtikalId.Value; break;
                        }
                    }

                    var artikal = await _artikliService.Artikal(id);
                    var artikal_detail = await _artikliService.ArtikalDetalj(id);
                    if (artikal_detail != null)
                    {
                        opis1 = artikal_detail.Opis;
                        opis2 = artikal_detail.Opis2;

                    }

                    Detail = new Artikal_Detail_VM()
                    {
                        Artikal_Id = artikal.Id,
                        Naziv = artikal.Naziv,
                        Opis = (String.IsNullOrEmpty(opis1)) ? model_from_db.Description : opis1,
                        Opis2 = (String.IsNullOrEmpty(opis2)) ? model_opis2 : opis2,
                        OpisDetalj = (model_detail_from_db != null) ? model_opis_detalj : "",
                        MainImage = artikal.Slika,
                        Image_Source = artikal.SlikaSource,
                        HasPopust = (model_from_db.Popust == 0 || model_from_db.Popust == null) ? false : true,
                        Popust = (model_from_db.Popust != null) ? model_from_db.Popust.ToString() : "",
                        MpCena = (model_from_db.Mpcena != null) ? model_from_db.Mpcena.ToString() : "",
                        FinalnaCena = (model_from_db.FinalnaCena != null) ? model_from_db.FinalnaCena.Value : 0,
                        FinalnaCenaBezPdv = (model_from_db.FinalnaCenaPdv != null) ? model_from_db.FinalnaCenaPdv.Value : 0,
                        Brand = String.IsNullOrEmpty(artikal.Brand) ? "" : artikal.Brand.ToString(),
                        Brand_Id = String.IsNullOrEmpty(artikal.Brand) ? null : artikal.Brand.ToString(),
                        Deklaracija = NapraviDeklaracije(model_from_db.Source, artikal, model_from_db),
                        //Deklaracija = (model_detail_from_db != null) ? model_detail_from_db.Deklaracija : "",
                        ColorId = current_color_id,
                        SizeId = current_size_id,
                        SingleSize = single_size,
                        Size = current_size,
                        Barkod = artikal.Barkod,
                        Sifra = artikal.Sifra

                    };
                    foreach (var item in model_from_db.ModelColor.ToList())
                    {
                        if (item.Color.Trim() != "")
                        {
                            Detail.Boje_Moguce.Add(new Model_Color() { Id = item.Id, Color = item.Color });
                        }

                    }
                    var model_color_sizes = await _modelService.Model_Color_Sizes(current_color_id);
                    foreach (var item in model_color_sizes)
                    {
                        if (item.Size != null)
                        {
                            if (item.Size.Trim() != "")
                            {

                                Detail.Velicine_Moguce.Add(new Model_Size() { Id = item.Id, Size = item.Size });
                            }
                        }



                    }
                    var grupa = await _artikliService.GrupeArtikala(model_from_db.Grupa1.Value);
                    var pod_grupa = new GrupeArtikala();
                    if (model_from_db.Grupa2 != null)
                    {
                        pod_grupa = await _artikliService.GrupeArtikala(model_from_db.Grupa2.Value);
                    }
                    else
                    {
                        pod_grupa = null;
                    }


                    var svojstva = await _artikliService.Artikal_Svojstva(id);
                    var boje = svojstva.Where(x => x.Naziv == "Color").ToList();
                    var velicina = svojstva.Where(x => x.Naziv == "Size").OrderBy(x => x.Naziv).ToList();
                    var slike = await _artikliService.VratiiDodatneSlike(id);
                    Detail.Category = grupa.Naziv.Trim();
                    Detail.Category_Id = grupa.Id.ToString();
                    if (pod_grupa != null)
                    {
                        Detail.Sub_Category = pod_grupa.Naziv.Trim();
                        Detail.Sub_Category_Id = pod_grupa.Id.ToString();
                    }
                    if (boje != null && boje.Count > 0)
                    {

                        foreach (var item in boje)
                        {

                            Detail.Boje.Add(new Artikal_Svojstva() { Naziv = item.Naziv, Vrednost = item.Vrednost });
                        }
                    }
                    if (velicina != null && velicina.Count > 0)
                    {
                        foreach (var item in velicina)
                        {

                            Detail.Velicine.Add(new Artikal_Svojstva() { Naziv = item.Naziv, Vrednost = item.Vrednost });
                        }
                    }
                    if (slike != null && slike.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(Detail.MainImage))
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {

                                Source = Detail.Image_Source,
                                Name = Detail.MainImage
                            });
                        }
                        foreach (var item in slike)
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {
                                Id = item.Id,
                                Source = item.Source,
                                Name = item.Slika
                            });
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(artikal.Slika))
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {

                                Source = artikal.SlikaSource,
                                Name = artikal.Slika
                            });
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(model_from_db.Slika))
                            {
                                Detail.DodatneSlike.Add(new Models.Slika._Slika()
                                {

                                    Source = model_from_db.SlikaSource,
                                    Name = model_from_db.Slika
                                });
                            }

                        }


                    }
                    //ViewData["Title"] = string.IsNullOrEmpty(artikal_detail.OpisSeo) ? Detail.Naziv : artikal_detail.OpisSeo;
                    ViewData["Title"] = Detail.Naziv;
                }

            }
            else
            {
                var current_color_id = 0;
                var current_size_id = 0;
                var current_size = "";
                bool single_size = true;
                var artikal = await _artikliService.Artikal(id);
                var artikal_detail = await _artikliService.ArtikalDetalj(id);
                if (artikal_detail != null)
                {
                    opis1 = artikal_detail.Opis;
                    opis2 = artikal_detail.Opis2;

                }
                var model_from_db = await _modelService.Model(artikal.ModelId.Value);
                if (model_from_db != null)
                {
                    var model_detail_from_db = await _modelService.Model_Detail(artikal.ModelId.Value);
                    var model_color = model_from_db.ModelColor.Where(x => x.Color.Trim() == artikal.Color.Trim()).FirstOrDefault();
                    if (model_detail_from_db != null)
                    {
                        model_opis2 = model_detail_from_db.Opis2;
                        model_opis_detalj = model_detail_from_db.OpisDetalj;
                    }
                    current_color_id = model_color.Id;


                    if (!String.IsNullOrEmpty(artikal.Size))
                    {
                        var model_color_size = model_color.ModelColorSize.Where(x => x.Size == artikal.Size).FirstOrDefault();
                        if (model_color_size != null) { current_size = model_color_size.Size; }

                    }

                    if (model_from_db.ModelColor.ToList()[0].ModelColorSize.ToList().Count > 1)
                    {
                        single_size = false;
                    }
                    //foreach (var item in model_from_db.ModelColor)
                    //{
                    //    current_color_id = item.Id;
                    //    foreach (var item2 in item.ModelColorSize)
                    //    {
                    //        current_size_id = item2.Id;
                    //        current_size = item2.Size;
                    //        id = item2.ArtikalId.Value; break;
                    //    }
                    //}

                    var current_color = await _modelService.Model_Color_By_Artikal(artikal.Id);
                    current_color_id = current_color.Id;
                    var size = await _artikliService.Artikal_Size_Detail(artikal.Id);
                    current_size = size.Size;
                    current_size_id = size.Id;
                    Detail = new Artikal_Detail_VM()
                    {
                        Artikal_Id = artikal.Id,
                        Naziv = artikal.Naziv,
                        Opis = (String.IsNullOrEmpty(opis1)) ? model_from_db.Description : opis1,
                        Opis2 = (String.IsNullOrEmpty(opis2)) ? model_opis2 : opis2,
                        OpisDetalj = (model_detail_from_db != null) ? model_opis_detalj : "",
                        MainImage = artikal.Slika,
                        Image_Source = artikal.SlikaSource,
                        HasPopust = (model_from_db.Popust == 0 || model_from_db.Popust == null) ? false : true,
                        Popust = (model_from_db.Popust != null) ? model_from_db.Popust.ToString() : "",
                        MpCena = (model_from_db.Mpcena != null) ? model_from_db.Mpcena.ToString() : "",
                        FinalnaCena = (model_from_db.FinalnaCena != null) ? model_from_db.FinalnaCena.Value : 0,
                        FinalnaCenaBezPdv = (model_from_db.FinalnaCenaPdv != null) ? model_from_db.FinalnaCenaPdv.Value : 0,
                        Brand = String.IsNullOrEmpty(artikal.Brand) ? "" : artikal.Brand.ToString(),
                        Brand_Id = String.IsNullOrEmpty(artikal.Brand) ? null : artikal.Brand.ToString(),
                        Deklaracija = NapraviDeklaracije(model_from_db.Source, artikal, model_from_db),
                        ColorId = current_color_id,
                        SizeId = current_size_id,
                        SingleSize = single_size,
                        Size = current_size,
                        Barkod = artikal.Barkod,
                        Sifra = artikal.Sifra

                    };
                    foreach (var item in model_from_db.ModelColor.ToList())
                    {
                        if (item.Color.Trim() != "")
                        {
                            Detail.Boje_Moguce.Add(new Model_Color() { Id = item.Id, Color = item.Color });
                        }

                    }
                    var model_color_sizes = await _modelService.Model_Color_Sizes(current_color_id);
                    foreach (var item in model_color_sizes)
                    {
                        if (item.Size != null)
                        {
                            if (item.Size.Trim() != "")
                            {

                                Detail.Velicine_Moguce.Add(new Model_Size() { Id = item.Id, Size = item.Size });
                            }
                        }

                    }
                    var grupa = await _artikliService.GrupeArtikala(model_from_db.Grupa1.Value);
                    var pod_grupa = new GrupeArtikala();
                    if (model_from_db.Grupa2 != null)
                    {
                        pod_grupa = await _artikliService.GrupeArtikala(model_from_db.Grupa2.Value);
                    }
                    else
                    {
                        pod_grupa = null;
                    }

                    var svojstva = await _artikliService.Artikal_Svojstva(id);
                    var boje = svojstva.Where(x => x.Naziv == "Color").ToList();
                    var velicina = svojstva.Where(x => x.Naziv == "Size").ToList();
                    var slike = await _artikliService.VratiiDodatneSlike(id);
                    Detail.Category = grupa.Naziv.Trim();
                    Detail.Category_Id = grupa.Id.ToString();
                    if (pod_grupa != null)
                    {
                        Detail.Sub_Category = pod_grupa.Naziv.Trim();
                        Detail.Sub_Category_Id = pod_grupa.Id.ToString();
                    }
                    if (boje != null && boje.Count > 0)
                    {
                        foreach (var item in boje)
                        {

                            Detail.Boje.Add(new Artikal_Svojstva() { Naziv = item.Naziv, Vrednost = item.Vrednost });
                        }
                    }
                    if (velicina != null && velicina.Count > 0)
                    {
                        foreach (var item in velicina)
                        {

                            Detail.Velicine.Add(new Artikal_Svojstva() { Naziv = item.Naziv, Vrednost = item.Vrednost });
                        }
                    }
                    if (slike != null && slike.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(Detail.MainImage))
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {

                                Source = Detail.Image_Source,
                                Name = Detail.MainImage
                            });
                        }
                        foreach (var item in slike)
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {
                                Id = item.Id,
                                Source = item.Source,
                                Name = item.Slika
                            });
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(artikal.Slika))
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {

                                Source = artikal.SlikaSource,
                                Name = artikal.Slika
                            });
                        }
                        else
                        {
                            Detail.DodatneSlike.Add(new Models.Slika._Slika()
                            {

                                Source = model_from_db.SlikaSource,
                                Name = model_from_db.Slika
                            });
                        }


                    }
                    //ViewData["Title"] = string.IsNullOrEmpty(artikal_detail.OpisSeo) ? Detail.Naziv : artikal_detail.OpisSeo;
                    ViewData["Title"] = Detail.Naziv;
                }
            }





        }

        public async Task<IActionResult> OnPostAddToCart(string id, string quantity, string price)
        {


            string cookie_token = "";
            cookie_token = Request.Cookies["cart"];

            var vm = new Models.Cart();
            if (String.IsNullOrEmpty(cookie_token))
            {
                Cookie_Set("cart", Guid.NewGuid().ToString(), 1296000000);
                cookie_token = Request.Cookies["cart"];
            }
            var cart = await _cartService.Get_By_Token(cookie_token);
            if (cart == null)
            {
                cart = await _cartService.Add(cookie_token, DateTime.Now.AddDays(30));
            }
            var artikal_from_db = await _artikliService.Artikal(Convert.ToInt32(id));
            var model_from_db = await _modelService.Model(artikal_from_db.ModelId.Value);
            await _cartService.Add_Cart_Item(cart.Id, Convert.ToInt32(id), Convert.ToInt32(quantity), model_from_db.FinalnaCena.Value);
            var cart_items = await _cartService.Get_Cart_Items(cart.Id);
            vm.token = cart.Token;
            double total_price = 0;
            foreach (var item in cart_items)
            {

                var artikal = await _artikliService.Artikal(item.ArtikalId);
                vm.items.Add(new Cart_Item
                {
                    title = artikal.Naziv,
                    quantity = Convert.ToInt32(item.Kolicina),
                    price = Convert.ToDouble(item.Cena),
                    id = artikal.Id,
                    image = artikal.Slika
                });
                total_price += item.Cena * item.Kolicina;
            }
            vm.item_count = vm.items.Count;
            vm.total_price = total_price;
            return new JsonResult(vm);
        }

        public async Task<IActionResult> OnGetCart()
        {
            string cookie_token = Request.Cookies["cart"];
            var vm = new Models.Cart();
            var cart = await _cartService.Get_By_Token(cookie_token);
            var cart_items = await _cartService.Get_Cart_Items(cart.Id);
            vm.token = cart.Token;
            double total_price = 0;
            foreach (var item in cart_items)
            {
                var artikal = await _artikliService.Artikal(item.ArtikalId);
                vm.items.Add(new Cart_Item
                {
                    product_title = artikal.Naziv,
                    quantity = Convert.ToInt32(item.Kolicina),
                    price = Convert.ToDouble(item.Cena),
                    id = artikal.Id,
                    image = artikal.Slika,
                    product_color = artikal.Color,
                    product_size = artikal.Size,
                });
                total_price += item.Cena * item.Kolicina;
            };
            vm.item_count = vm.items.Count;
            vm.total_price = total_price;
            return new JsonResult(vm);
        }
        public async Task<IActionResult> OnGetChangeProductByColor(int c_Id)
        {
            var model_color_sizes = await _modelService.Model_Color_Sizes(c_Id);
            var article_Id = model_color_sizes[0].ArtikalId;
            return new JsonResult(article_Id);
        }
        public async Task<IActionResult> OnGetChangeProductByCSize(int c_id, int s_id)
        {
            var vm = new Artikal_Detail_VM();
            var artikal = await _artikliService.Artikal_By_Model_Color_Size(c_id, s_id);
            var model_from_db = await _modelService.Model(artikal.ModelId.Value);
            artikal.ModelColorSize = null;
            vm.Artikal_Id = artikal.Id;
            vm.Sifra = artikal.Sifra;
            vm.Barkod = artikal.Barkod;
            vm.Deklaracija = NapraviDeklaracije(artikal.Source, artikal, model_from_db);
            return new JsonResult(vm);
        }

        public void CheckCartCookie()
        {
            string cookie_token = Request.Cookies["cart"];
            if (cookie_token == null)
            {
                CookieOptions option = new CookieOptions() { Path = "/", HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.Strict };
                option.Expires = DateTime.Now.AddMilliseconds(1296000000);

                try
                {
                    Response.Cookies.Append("cart", Guid.NewGuid().ToString(), option);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }

        public void Cookie_Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions() { Path = "/", HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.Strict };

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(10);
            else
                option.Expires = DateTime.Now.AddMinutes(10);
            try
            {
                Response.Cookies.Append(key, value, option);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }


        }

        public string NapraviDeklaracije(string _source, Artikal _artikal, Data.Models.Model _model)
        {
            StringBuilder deklaracija = new StringBuilder();
            deklaracija.Append("<div><ul>");

            string naziv = _artikal.Naziv;
            string sifra = _artikal.Sifra;
            string barkod = _artikal.Barkod;
            string uvoznik = "";
            if (_source == "LACUNA")
            {
                uvoznik = "Lacuna d.o.o.";
            }
            else if (_source == "ALBO")
            {
                uvoznik = "ALBO d.o.o.";
            }
            else if (_source == "PROMOSOLUTIONS")
            {
                uvoznik = "PUBLIK d.o.o.";
            }
            else if (_source == "SAVACOOP")
            {
                uvoznik = "SAVACOOP";
            }

            string zemlja_porekla = _model.ZemljaPorekla;
            string li_naziv = "<li><strong>Naziv proizvoda: </strong><span>" + naziv + "</span></li>";
            string li_sifra = "<li><strong>Šifra proizvoda: </strong><span>" + sifra + "</span></li>";
            string li_barkod = String.IsNullOrEmpty(barkod) ? "" : "<li><strong>Barkod: </strong><span>" + barkod + "</span></li>";
            string li_uvoznik = "<li><strong>Uvoznik: </strong><span>" + uvoznik + "</span></li>";
            string li_zemlja = "<li><strong>Zemlja porekla: </strong><span>" + zemlja_porekla + "</span></li>";
            deklaracija.Append(li_naziv);
            deklaracija.Append(li_sifra);
            deklaracija.Append(li_barkod);
            deklaracija.Append(li_uvoznik);
            deklaracija.Append(li_zemlja);
            deklaracija.Append("</ul></div>");
            return deklaracija.ToString();
        }
    }
}
