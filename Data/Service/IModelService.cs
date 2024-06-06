using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IModelService
    {
        public Task<Models.Model> Model(int id);
        Task<bool> Model_Delete(int id);
        Task<ModelDetail> Model_Detail(int? id);
        Task<bool> Model_Detail_Exist(int? id);
        Task<bool> Model_Detail_Update(ModelDetail artikal);
        Task<ModelDetail> Model_Detail_Insert(ModelDetail model);
        Task<List<ModelColorSize>> Model_Color_Sizes(int id);
        Task<bool> Model_Size_Delete(int model_id);
        Task<ModelColor> Model_Color_By_Artikal(int a_id);
        Task<List<ModelColor>> Model_Colors_By_Model(int m_id);
        Task<bool> Model_Update(Models.Model model);
      
        public Task<IQueryable<Models.Model>> SviModeli(ShopSearchCriteriaModel criteria);
        Task<List<Models.Model>> Modeli_Promoslutions();
        Task<List<Models.Model>> Modeli_Lacuna();
        Task<IQueryable<Models.Model>> SviModeliAdmin(ShopSearchCriteriaModel criteria);
        public Task<List<Models.Model>> SviModeliNovi();


    }
}
