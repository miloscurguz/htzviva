using System.Collections.Generic;

namespace viva.admin.Models.Grupe
{
    public class Pod_Grupe_VM
    {
        public int Grupa_Id { get; set; }
        public string Grupa_Naziv { get; set; }
        public List<Grupa> Pod_Grupe { get; set; }
    }
}
