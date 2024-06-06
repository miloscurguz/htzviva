using AutoMapper;

namespace viva.webstore.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Data.Models.Artikal, Artikal>();
            //CreateMap<Data.Models.GrupeArtikala, GrupeArtikala>();
            CreateMap<Models.Shop.ShopSearchCriteriaModel, Data.Models.ShopSearchCriteriaModel>();
            //CreateMap<User, Data.Models.User>();
            //CreateMap<Data.Models.User, User>();
            ////CreateMap<Data.Models.CartItem, CartItems>();
            ////CreateMap<CartItems, Data.Models.CartItem>();
            //CreateMap<Adresa, Data.Models.Adresa>();
            //CreateMap<Data.Models.Adresa, Adresa>();
            CreateMap<Data.Models.OrderItem, Models.OrderItem>();
            CreateMap<Models.OrderItem, Data.Models.OrderItem>();
            CreateMap<Models.Order, Data.Models.Order>();
            CreateMap<Data.Models.Order, Models.Order>();
            CreateMap<Data.Model.Models.Shipping, Models.Shipping>();
            CreateMap<Models.Shipping, Data.Model.Models.Shipping>();
            CreateMap<Data.Models.Kontakt, Models.KontaktVM>();
            CreateMap<Models.KontaktVM, Data.Models.Kontakt>();
        }
    }
}
