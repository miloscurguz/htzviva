using System.ComponentModel.DataAnnotations;

namespace viva.webstore.Models.Enum
{
    public enum EnPlacanje
    {
        [Display(Name = "Lično Gotovina")]
        LicnoGtovina =1,
        [Display(Name = "Lično Kartica")]
        LicnoKartica=2,
        [Display(Name = "Dostava - plaćanje kuriru")]
        Dosatva = 3,
        [Display(Name = "Profaktura")]
        Profaktura = 4
    }
    public enum EnIsporuka
    {
        [Display(Name = "Lično")]
        LicnoGtovina = 1,
        [Display(Name = "Dostava")]
        LicnoKartica = 2
    }
    public enum EnTipKupca
    {
        [Display(Name = "Fizičko")]
        LicnoGtovina = 1,
        [Display(Name = "Pravno")]
        LicnoKartica = 2
    }


}
