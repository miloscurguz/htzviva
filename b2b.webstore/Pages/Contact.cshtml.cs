using viva.webstore.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using viva.webstore.Models;
using Data.Service;
using AutoMapper;
using Data.Models;
using DNTCaptcha.Core;
using System;
using Microsoft.Extensions.Options;

namespace viva.webstore.Pages
{
   
    public class ContactModel : PageModel
    {
        private readonly ILogger<ContactModel> _logger;
        private readonly IEmailSender _mailService;
        private readonly IMapper _mapper;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;
        [BindProperty] public Models.KontaktVM VM { get; set; }
        public ContactModel(
            ILogger<ContactModel> logger,
            IEmailSender mailService,
            IMapper mapper,
            IDNTCaptchaValidatorService validatorService,
            IOptions<DNTCaptchaOptions> captchaOptions)
        {
            _logger = logger;
            _mailService = mailService;
            _mapper = mapper;
            _validatorService = validatorService;
            _captchaOptions = captchaOptions==null?throw new ArgumentNullException(nameof(captchaOptions)):captchaOptions.Value;
        }
        public void OnGet()
        {
            VM = new KontaktVM() { };
        }
      
        public IActionResult OnPost(KontaktVM model)
        {
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Molimo Vas da unesite tačan sigurnosni broj!");
                    return Page();
                }
                else
                {
                    try{
                        _mailService.SendContactMessage(_mapper.Map<Kontakt>(VM));
                        VM.Info_Message="Poruka je uspečno poslata!";
                        return Page();
                    }
                    catch(Exception ex)
                    {

                        this.ModelState.AddModelError("Greska", "Došlo je do greške!");
                        return Page();
                    }
                
                   
                }
            }
            else
            {
                this.ModelState.AddModelError("Greska", "Došlo je do greške!");
                return Page();
            }
               
        }
    }
}
