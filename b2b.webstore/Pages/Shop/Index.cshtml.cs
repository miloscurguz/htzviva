using AutoMapper;
using Data.Models;
using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using viva.webstore.Models;
using viva.webstore.Models.Shop;
using ShopSearchCriteriaModel = Data.Models.ShopSearchCriteriaModel;

namespace viva.webstore.Pages.Shop
{
    public class IndexModel : PageModel
    {
        public IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        private readonly IModelService _modelService;
        private readonly IArtikliService _artikleService;
        [BindProperty] public ShopVM vm { get; set; }
        public IndexModel(IConfiguration configuration, IModelService modelService, IArtikliService artikleService, IMapper mapper)
        {
            Configuration = configuration;
            _modelService = modelService;
            _artikleService = artikleService;
            _mapper = mapper;
        }

        public async Task OnGet(Models.Shop.ShopSearchCriteriaModel model)
        {
            CheckCartCookie();
            var adminLink = Configuration.GetValue<string>("AdminLink");
            var fileFolder = Configuration.GetValue<string>("ImageFolder");
            string path = Path.Combine(adminLink, fileFolder);
            int pageSize = 0;
            if (model.SortMode == 0) { model.SortMode = 1; }

            if (model.RecordsByPage == null)
            {
                pageSize = 50;

            }
            else
            {
                pageSize = model.RecordsByPage.Value;
            }
            var modeli = await _modelService.SviModeli(_mapper.Map<ShopSearchCriteriaModel>(model));
            vm = new ShopVM();
            //var art = _mapper.Map<List<Artikal>>(artikli);
            vm.Group_Id = model.Group_Id;
            vm.Sub_Group_Id = model.Sub_Group_Id;
            var pl = await PaginatedList<Data.Models.Model>.CreateAsync(modeli, model.PageNumber ?? 1, pageSize);
            vm.Total = pl.TotalPages;
            if (model.SortMode == 0)
            {
                vm.SortMode = 1;
            }
            else
            {
                vm.SortMode = model.SortMode;
            }
            vm.PageNumber = pl.PageIndex;
            vm.Artikli = pl;
            vm.RecordByPage = pageSize;
            vm.Sort = ((EnSort)model.SortMode).ToString().Replace("_", " ");
            vm.ImagePath = path;
            vm.Group_Id = model.Group_Id;
            vm.Brand = model.Brand;
            vm.Keyword=model.Keyword;
          
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

    }
}
