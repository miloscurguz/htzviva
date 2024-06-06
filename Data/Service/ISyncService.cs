using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface ISyncService
    {
        public Task<bool> SyncArtikala(string sifra);
        Task<bool> SyncGrupeArtikala();
        Task<bool> SyncTipoveCenovnika();
        Task<bool> SyncCenovnika(string tip);
        Task<bool> PROMOSOLUTIONS_Sync_Brand();
        Task<bool> ALBO_Sync_Brand();
        Task<bool> ALBO_Sync_Model();
        Task<bool> ALBO_Sync_Artikla();
        Task<bool> ALBO_SyncGrupeArtikala();
        Task<bool> PROMOSOLUTIONS_SyncArtikala(string spource_id);
        Task<bool> PROMOSOLUTIONS_SyncModela(string spource_id);
        Task<bool> PROMOSOLUTIONS_Update();
        Task<bool> Lacuna_SyncArtikala(string spource_id);
        Task<bool> LACUNA_SyncGrupeArtikala();
        Task<bool> PROMOSOLUTIONS_Sync_Color();
        Task<bool> PROMOSOLUTIONS_SyncGrupeArtikala2();
        Task<bool> SyncStanja(string sifra);
        Task<bool> SyncAllGrupe();
        Task<bool> SyncSlika();
        Task<bool> SavaCoop_Sync();


        }
}
