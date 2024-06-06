using Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Data.Service
{
    public class MailService : IMailService
    {
        SmtpClient smtpClient = new SmtpClient("mailcluster.loopia.se", 587);
        public readonly IArtikliService _aService;
        public readonly IUserService _uService;

        public MailService(IArtikliService aService, IUserService uService)
        {
            _aService = aService;
            _uService = uService;
        }

        public bool SendOrderToAdmin(int oId)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential("info@eberba.rs", "Infoeberba123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var order = _aService.Order_Get_Active(oId);
            var orerItems = _aService.Order_Item_Get(order.Id);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<thead><tr><th> Proizvod <th><th> Ukupno</th></tr ></thead >");
            sb.Append("<tbody>");
            foreach (var item in orerItems)
            {
                sb.AppendFormat("tr><td>{0}<strong>x</strong>{1}</td><td>{2} rsd.</td></tr>", item.ArtikalId.ToString(), item.Kolicina.ToString(), item.Cena * item.Kolicina);
            }

            sb.Append("<tr><td><strong>Ukupno: @Model.Total rsd.</strong></td><td><strong></strong></td></tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            MailMessage mail = new MailMessage();
            mail.Subject = "Order from Site";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), Encoding.UTF8, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("info@eberba.rs", "Poruka sa web sajta");
            mail.To.Add(new MailAddress("milos.curguz.mc@gmail.com"));
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

        public bool SendORderToCustomer(int odId, Data.Model.Models.Shipping input)
        {
            smtpClient.Credentials = new System.Net.NetworkCredential("info@eberba.rs", "Infoeberba123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var order = _aService.Order_Get_Active(odId);
            var orerItems = _aService.Order_Item_Get(order.Id);
            var sb = OrderMailTemplateCustomer(order, orerItems);
            MailMessage mail = new MailMessage();
            mail.Subject = "Order from Site";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), Encoding.UTF8, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("info@eberba.rs", "Poruka sa web sajta");
            mail.To.Add(new MailAddress("milos.curguz.mc@gmail.com"));
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

        public bool SendWelcomeEmail(int uId)
        {
            var user = _uService.GetUserById(uId);
            smtpClient.Credentials = new System.Net.NetworkCredential("info@eberba.rs", "Infoeberba123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Drobro dosli na sajt Monteks.com</p>");

            MailMessage mail = new MailMessage();
            mail.Subject = "Monteks Dobroslica";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(WelcomeMailTemplate(), Encoding.UTF8, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Body = WelcomeMailTemplate();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("info@eberba.rs", "Monteks Dobroslica");
            mail.To.Add(new MailAddress("milos.curguz.mc@gmail.com"));
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

        public string WelcomeMailTemplate()
        {
            string MailBody =
                "<!DOCTYPE html>" +
                    "<html>" +
                        "<body>" +
                            "<br/>" +
                             "<table width = \"50%\">" +
                                "<tr>" +
                                    "<td align = \"center\">" +
                                        "<span style = \"font-size:25px;\" > Dobrodošli u Monteks</span>" +
                                        "<br/>" +
                                        "<img src = \"https://test.monteks.rs/images/logo.jpg\"/>" +
                                        "<br/>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr align =\"center\">" +
                                    "<td>" +
                                        "<p> Uspešno ste se registrovali i sada Vam je omogućena kupovina proizvoda na našem web sajtu.</p>" +
                                        "<br/>" +
                                        "<p>Zahvaljujemo se na ukazanom poverenju.</p>" +
                                        "<br/>" +
                                        "<p>Monteks d.o.o se obavezuje na privatnost Vaših ličnih podataka koji će biti korišćeni isključivo u svrhu kupovine na našem sajtu </p>" +
                                        "<br/>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td bgcolor = \"#2f549a\" style = \"padding:12px 18px 12px 18px\" align = \"center\" ><a href = \"http://monteks.rs\" style = \"font-size:18px;font-family:Arial,sans-serif;font-weight:normal;color:#ffffff;text-decoration:none;display:inline-block\" target = \"_blank\"> Posetite  Sajt </a ></td>" +
                                "</tr>" +
                                "</table>" +
                      "</body>" +
                     "</html>";

            return MailBody;
        }

        public string OrderMailTemplateCustomer(Order order, List<OrderItem> items)
        {
            string MailBody =
                "<!DOCTYPE html>" +
                    "<html>" +
                        "<body>" +
                            "<br/>" +
                             "<table width = \"100%\">" +
                                "<tr>" +
                                    "<td align = \"center\">" +
                                        
                                        "<img src = \"https://test.monteks.rs/images/logo.jpg\"/>" +
                                        "<br/>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td align=\"center\" valign=\"top\">" +
                                        "<table border=\"0\" cellpadding=\"0\" cellspacing = \"0\" style = \"width:100%;background-color:#AB516A;border-radius:3px 3px 0 0!important;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif\">" +
                                            "<tbody>" +
                                                "<tr>" +
                                                    "<td style=\"padding:36px 48px;display:block\">" +
                                                        "<h1 style = \"color:#ffffff;font-family:&quot;Helvetica Neue&quot;,Helvetica,Roboto,Arial,sans-serif;font-size:30px;font-weight:300;line-height:150%;margin:0;text-align:left\">Naruzbenica:"+order.Referenca+"</h1>" +
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
                                        "<table cellspacing = \"0\" cellpadding = \"6\" style = \"width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;border:1px solid #e4e4e4\" border = \"1\">"+
                                            "<thead>"+
                                            "<tr>"+
                                                "<th scope= \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Proizvod</th>"+
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Količina</th>"+
                                                "<th scope = \"col\" style = \"text-align:left;color:#737373;border:1px solid #e4e4e4;padding:12px\"> Cena</th>"+
                                            "</tr>"+
                                            "</thead>"+
                                            "<tbody>"+
                                            GenerateIems(items)+
                                            "</tbody>"+
                                            "<tfoot>"+
                                            "</tfoot>"+
                                        "</table>"+
                                   "</td>" +
                                "</tr>" +
                                                  
                                "</table>" +
                      "</body>" +
                     "</html>";

            return MailBody;
        }

        public string GenerateIems(List<OrderItem> items)
        {
            string rows = "";
            foreach(var item in items)
            {
                string cena = String.Format("{0:n}", item.Cena * item.Kolicina);
                rows +=
                    "<tr>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word;color:#737373;padding:12px\">" + item.ArtikalId + "</td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\">" + item.Kolicina + "</td>" +
                        "<td style = \"text-align:left;vertical-align:middle;border:1px solid #eee;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;color:#737373;padding:12px\" ><span>" + cena + "<span> RSD </span ></span></td>" +
                    "</tr>";
            }
            return rows;
            
        }
    }
}
