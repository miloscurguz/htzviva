using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface ISettingsService
    {
        Task<bool> UbaciMeni(string nazic, int gId, int parentId);
        Task<List<Meni>> PrikaziMeni();
        Task<bool> IzbaciMeni(int id);
        Task<bool> SnimiMagacin(string sifra);
        Task<List<Magacini>> AktivniMagacin();
        Task<bool> SnimiCenovnik(string sifra);
        Task<string> AktivniCenovnik();

        bool IzbaciMagacin(int id);
        Task<bool> InsertSlider(Slider slider);
        Task<List<Slider>> GetSliders();
        Task<bool> IzbaciSlider(int id);
        Task<bool> UpdateSlider(Slider slider);


    }
}
