using System.Collections.Generic;

namespace viva.admin.Models.Podesavanja
{
    public class PodesavanjaViewModel
    {
        public List<Magacin> OrgJed { get; set; }
        public string OrgJedSifra { get; set; }
        public List<Magacin> Magacini { get; set; }
    }
}
