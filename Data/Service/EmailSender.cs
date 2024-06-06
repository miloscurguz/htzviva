using Data.Model.Models;
using Data.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Data.Service
{
    public class EmailSender : IEmailSender
    {
        SmtpClient smtpClient = new SmtpClient("mailcluster.loopia.se", 587);
        public readonly IArtikliService _aService;
        public readonly IUserService _uService;

        public EmailSender(IArtikliService aService, IUserService uService)
        {
            _aService = aService;
            _uService = uService;
        }


        public bool SendOrderToAdmin(int oId)
        {
            throw new NotImplementedException();
        }

        public bool SendORderToCustomer(int odId, Data.Model.Models.Shipping input)
        {

            smtpClient.Credentials = new System.Net.NetworkCredential("web@htzviva.rs", "MailSifra123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var order = _aService.Order_Get_Active(odId);
            var orerItems = _aService.Order_Item_Get(order.Id);
            //var user = _uService.GetUserById(order.UserId).Result;
            //var adresa = _uService.Adresa_Get(user.Id).Result;
            var sb = OrderMailTemplateCustomer(order, orerItems, input);
            MailMessage mail = new MailMessage();
            mail.Subject = "HTZ VIVA/ Porudzbina sa sajta/" + order.Referenca;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), Encoding.UTF8, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("web@htzviva.rs", "HTZ VIVA/Porudzbina sa sajta/" + order.Referenca);
            mail.To.Add(new MailAddress(input.Email));
            mail.Bcc.Add(new MailAddress("mirjana@htzviva.rs"));
            mail.Bcc.Add(new MailAddress("marijana@htzviva.rs"));

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string Kontakt_Template(Kontakt kontakt)
        {
            string MailBody =
                "<!DOCTYPE html>" +
                        "<html>" +
                            "<body>" + "<br/>" +
                            "<table width = \"100%\">" + "<tr>" + "<td align = \"center\">" + "<img src = \"https://test.htzviva.rs/img/logo_viva.png\"/>" + "<br/>" + "</td>" + "</tr>" + "<tr>" + "<td align=\"center\" valign=\"top\">" + "<table border=\"0\" cellpadding=\"0\" cellspacing = \"0\" style = \"width:100%;background-color:#F8B131;border-radius:3px 3px 0 0!important;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif\">" + "<tbody>" +
                                            "</tbody>" +
                                        "</table>" +

                                "</td>" +
                                "</tr>" +
                                "<tr align =\"center\">" +
                                    "<td>" +
                                        "<p>Poruka sa web sajta:</p>" +
                                        "<br/>" +
                                    "</td>" +
                                "</tr>" +
                                   "<tr align =\"center\">" +
                                    "<td>" +
                                        "<table cellspacing = \"0\" cellpadding = \"6\" style = \"width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;border:1px solid #e4e4e4\" border = \"1\">" +
                                            "<thead>" +
                                            "<tr>" +
                                                "<th scope= \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Email</th>" +
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Telefon</th>" +
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Ime</th>" +
                                            "</tr>" +
                                            "</thead>" +
                                            "<tbody>" +
                                                "<tr>" +
                "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word;color:#737373;padding:12px\">" + kontakt.Email + "</td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\">" + kontakt.Telefon + "</td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\" ><span>" + kontakt.Ime + "</td>" +
                    "</tr>" +
            "</tbody>" +
                                        "</table>" +
                                   "</td>" +
                                "</tr>" +


                                      "<tr>" +
                                    "<td>" +
                                    "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Poruka:</h2>" +
                                    "<p>" + kontakt.Poruka+ "</p>" +
                                    "</td>" +
                                "</tr>" +
                  

                                "</table>" +
                      "</body>" +
                     "</html>";

            return MailBody;
        }
        public string OrderMailTemplateCustomer(Order order, List<OrderItem> items, Data.Model.Models.Shipping shipping)
        {
            string MailBody =
                "<!DOCTYPE html>" +
                        "<html>" +
                            "<body>" + "<br/>" +
                            "<table width = \"100%\">" + "<tr>" + "<td align = \"center\">" + "<img src = \"https://test.htzviva.rs/img/logo_viva.png\"/>" + "<br/>" + "</td>" + "</tr>" + "<tr>" + "<td align=\"center\" valign=\"top\">" + "<table border=\"0\" cellpadding=\"0\" cellspacing = \"0\" style = \"width:100%;background-color:#F8B131;border-radius:3px 3px 0 0!important;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif\">" + "<tbody>" + "<tr>" + "<td style=\"padding:10px 10px;display:block\">" + "<h3 style = \"color:#000000;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:12px;font-weight:300;line-height:150%;margin:0;text-align:left\">Porudzbenica broj:" + order.Referenca + "</h1>" +

                                                    "</td>" +
                                                "</tr>" +
                                            "</tbody>" +
                                        "</table>" +

                                "</td>" +
                                "</tr>" +
                                "<tr align =\"center\">" +
                                    "<td>" +
                                        "<p> Vaša narudžbenica je primljena i upravo se obrađuje. Detalji vaše narudžbenice prikazani su dole:</p>" +
                                        "<br/>" +
                                    "</td>" +
                                "</tr>" +
                                   "<tr align =\"center\">" +
                                    "<td>" +
                                        "<table cellspacing = \"0\" cellpadding = \"6\" style = \"width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;border:1px solid #e4e4e4\" border = \"1\">" +
                                            "<thead>" +
                                            "<tr>" +
                                                "<th scope= \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Proizvod</th>" +
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Količina</th>" +
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Cena</th>" +
                                            "</tr>" +
                                            "</thead>" +
                                            "<tbody>" +
                                            GenerateIems(items) +
                                            "</tbody>" +
                                          "<tfoot>" +
                                            "<tr>" +
                                                "<th scope=\"row\" colspan=\"2\" style=\"text-align:left;border:1px solid #e4e4e4;padding:12px\">Iznos:</th>" +
                                                "<td style=\"text-align:left;border:1px solid #e4e4e4;padding:12px\"><span>" + String.Format("{0:n}", order.Iznos) + " <span> RSD. </span></span ></td>" +
                                            "</tr>" +
                                             "<tr>" +
                                                "<th scope=\"row\" colspan=\"2\" style=\"text-align:left;border:1px solid #e4e4e4;padding:12px\">Isporuka:</th>" +
                                                "<td style=\"text-align:left;border:1px solid #e4e4e4;padding:12px\"><span>" + String.Format("{0:n}", order.Isporuka) + " <span> RSD. </span></span ></td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<th scope = \"row\" colspan = \"2\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\" > Ukupno:</th>" +
                                                "<td style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"><span>" + String.Format("{0:n}", order.Ukupno) + " <span> RSD.</span></span></td>" +
                                            "</tr>" +
                                            "</tfoot>" +
                                        "</table>" +
                                   "</td>" +
                                "</tr>" +


                                      "<tr>" +
                                    "<td>" +
                                    "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Napomena:</h2>" +
                                    "<p>" + order.Napomena + "</p>" +
                                    "</td>" +
                                "</tr>" +
                                 GenerateNacinPlacanja(shipping) +
                                 GenerateDetaljiKupca(shipping) +
                                 GenerateAdress(shipping) +

                                "</table>" +
                      "</body>" +
                     "</html>";

            return MailBody;
        }

        public string GenerateIems(List<OrderItem> items)
        {
            string rows = "";
            foreach (var item in items)
            {
                string cena = String.Format("{0:n}", item.Cena * item.Kolicina);
                var artikal = _aService.Artikal(item.ArtikalId).Result;
                rows +=
                "<tr>" +
                "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word;color:#737373;padding:12px\">" + artikal.Naziv + "<br/><strong>Boja:</strong><span>" + artikal.Color + "</span><br/><strong>Velicina:</strong><span>" + artikal.Size + "</span></td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\">" + item.Kolicina + "</td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\" ><span>" + cena + "<span> RSD </span ></span></td>" +
                    "</tr>";
            }
            return rows;

        }
        public string GenerateNacinPlacanja(Shipping shipping)
        {
            string rows = "";
            if (shipping.Nacin_Placanja == 1)
            {
                //Fizicko
                rows += "<tr>" +
                                                  "<td>" +
                                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Način plaćanja:</h2>" +
                                                  "<p>Lično Gotovina</p>" +

                                                  "</td>" +
                                              "</tr>";
                return rows;
            }
            else if (shipping.Nacin_Placanja == 2)
            {
                //Fizicko
                rows += "<tr>" +
                                                  "<td>" +
                                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Način plaćanja:</h2>" +
                                                  "<p>Lično Kartica</p>" +

                                                  "</td>" +
                                              "</tr>";
                return rows;
            }
            else if (shipping.Nacin_Placanja == 3)
            {
                //Fizicko
                rows += "<tr>" +
                                                  "<td>" +
                                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Način plaćanja:</h2>" +
                                                  "<p>Dostava - plaćanje kuriru</p>" +

                                                  "</td>" +
                                              "</tr>";
                return rows;
            }
            else if (shipping.Nacin_Placanja == 4)
            {
                //Fizicko
                rows += "<tr>" +
                                                  "<td>" +
                                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Način plaćanja:</h2>" +
                                                  "<p>Profaktura</p>" +

                                                  "</td>" +
                                              "</tr>";
                return rows;
            }
            else
            {
                return "";
            }

        }
        public string GenerateDetaljiKupca(Shipping shipping)
        {
            string rows = "";
            if (shipping.Tip_Kupca == 1)
            {
                //Fizicko
                rows += "<tr>" +
                                                  "<td>" +
                                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Podaci o kupcu:</h2>" +
                                                  "<p><span> Ime: <span>" + shipping.Ime + "</p>" +
                                                   "<p><span> Prezime: <span>" + shipping.Prezime + "</p>" +
                                                    "<p><span> Email: <span>" + shipping.Email + "</p>" +
                                                     "<p><span> Telefon: <span>" + shipping.Telefon + "</p>" +
                                                  "</td>" +
                                              "</tr>";
                return rows;
            }
            else
            {
                rows += "<tr>" +
                                  "<td>" +
                                  "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Podaci o kupcu:</h2>" +
                                  "<p><span> Naziv: <span>" + shipping.Naziv + "</p>" +
                                   "<p><span> Pib: <span>" + shipping.PIB + "</p>" +
                                    "<p><span> Email: <span>" + shipping.Email + "</p>" +
                                     "<p><span> Telefon: <span>" + shipping.Telefon + "</p>" +
                                  "</td>" +
                              "</tr>";
                return rows;
            }


        }

        public string GenerateAdress(Shipping shipping)
        {
            string rows = "";
            if (shipping.Nacin_Isporuke == 2)
            {
                if (shipping.Tip_Kupca == 1)
                {
                    //Fizicko
                    rows += "<tr>" +
                                                      "<td>" +
                                                      "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Adresa za isporuku:</h2>" +
                                                      "<p><span> Adresa: <span>" + shipping.Adresa + "</p>" +
                                                       "<p><span> Grad: <span>" + shipping.Grad + "</p>" +
                                                        "<p><span> Postanski broj: <span>" + shipping.PostanskiBroj + "</p>" +
                                                         "<p><span> Telefon 1: <span>" + shipping.Telefon + "</p>" +
                                                      "</td>" +
                                                  "</tr>";
                    return rows;
                }
                else
                {
                    //Pravno
                    rows += "<tr>" +
                                                            "<td>" +
                                                            "<h2 style=\"color:#557da1;display:block;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:16px 0 8px;text-align:left\">Adresa za isporuku:</h2>" +
                                                            "<p><span> Adresa: <span>" + shipping.Adresa + "</p>" +
                                                             "<p><span> Grad: <span>" + shipping.Grad + "</p>" +
                                                              "<p><span> Postanski broj: <span>" + shipping.PostanskiBroj + "</p>" +
                                                               "<p><span> Telefon 1: <span>" + shipping.Telefon + "</p>" +
                                                            "</td>" +
                                                        "</tr>";
                    return rows;
                }
            }
            else
            {
                return "";
            }
        }

        public bool SendContactMessage(Kontakt model)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential("web@htzviva.rs", "MailSifra123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            //var user = _uService.GetUserById(order.UserId).Result;
            //var adresa = _uService.Adresa_Get(user.Id).Result;
            var sb = Kontakt_Template(model);
            MailMessage mail = new MailMessage();
            mail.Subject = "HTZ VIVA/Poruka sa sajta/";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), Encoding.UTF8, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("web@htzviva.rs", "HTZ VIVA/Poruka sa sajta");
         
            mail.To.Add(new MailAddress("mirjana@htzviva.rs"));
            mail.Bcc.Add(new MailAddress("marijana@htzviva.rs"));
            //mail.To.Add(new MailAddress("mirjana@htzviva.rs"));


            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
