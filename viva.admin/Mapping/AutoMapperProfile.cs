using AutoMapper;
using Data.Models;
namespace viva.admin.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Artikli.ShopSearchCriteriaModel, ShopSearchCriteriaModel>();
            CreateMap<Models.Podesavanja.Meni, Meni>();
            CreateMap<Meni, Models.Podesavanja.Meni>();
        }
    }
}
