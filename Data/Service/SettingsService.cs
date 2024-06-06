using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly IWSCalls _apiService;

        public SettingsService(IWSCalls apiService)
        {
            _apiService = apiService;
        }

        B2BContext dbContext = new B2BContext();
        public async Task<string> AktivniCenovnik()
        {
            return  dbContext.Settings.FirstOrDefault().Cenovnik;
        }

        public async Task<List<Magacini>> AktivniMagacin()
        {
            return await dbContext.Magacini.ToListAsync();
        }
        public bool IzbaciMagacin(int id)
        {
            var magacin = dbContext.Magacini.Where(x=>x.Id==id).FirstOrDefault();
            if (magacin != null)
            {
                dbContext.Magacini.Remove(magacin);
                dbContext.SaveChanges();
            }
        
            return true;
        }
        public async Task<bool> IzbaciMeni(int id)
        {
            var meni = dbContext.Meni.Where(x => x.Id == id).FirstOrDefault();
            var sub_menus = dbContext.Meni.Where(x => x.Parent == meni.Id).ToList();
            if(sub_menus!=null || sub_menus.Count > 0)
            {
                dbContext.Meni.RemoveRange(sub_menus);
            }
            if (meni != null)
            {
                dbContext.Meni.Remove(meni);
            }
            return true;
        }

        public async Task<List<Meni>> PrikaziMeni()
        {
            return dbContext.Meni.ToList();
        }

        public async Task<bool> SnimiCenovnik(string sifra)
        {
            var settings = dbContext.Settings.FirstOrDefault();
            settings.Cenovnik = sifra;
            dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> SnimiMagacin(string sifra)
        {
            var magacin = dbContext.Magacini.Where(x=>x.Magacin==sifra).FirstOrDefault();
            if (magacin != null)
            {
                return false;

            }
            else{
                dbContext.Magacini.Add(new Magacini() {Magacin=sifra });
                dbContext.SaveChanges();
            }
            
            return true;
        }

        public async Task<bool> UbaciMeni(string nazic, int gId, int parentId)
        {
            var meni = new Meni() { };
            meni.Naziv = nazic;
            meni.GrupaId = gId;
            meni.Parent = parentId;
            dbContext.Add(meni);
            dbContext.SaveChanges();
            return true;
        }

        public async Task<List<Slider>> GetSliders()
        {
            return await dbContext.Slider.ToListAsync();
        }

        public async Task<bool> IzbaciSlider(int id)
        {
            var slider = await dbContext.Slider.FirstOrDefaultAsync();
            dbContext.Slider.Remove(slider);
            dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateSlider(Slider slider)
        {
            if (slider == null)
            {
                var dbSlider = await dbContext.Slider.Where(x => x.Id == slider.Id).FirstOrDefaultAsync();
                dbSlider.Order = slider.Order;
                dbSlider.Text = slider.Text;
                dbSlider.Url = slider.Url;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertSlider(Slider slider)
        {
            if (slider != null)
            {
                var nSlider = new Slider();
                nSlider.Order = slider.Order;
                nSlider.Text = slider.Text;
                nSlider.Slika = slider.Slika;
                nSlider.Url = slider.Url;
                await dbContext.Slider.AddAsync(nSlider);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
