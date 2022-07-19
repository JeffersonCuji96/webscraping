using Microsoft.AspNetCore.Mvc;
using Scraping.Models;
using Scraping.Operations;
using ScrapySharp.Extensions;
using System.Text.RegularExpressions;

namespace Scraping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetScraping()
        {
            /*
               1. Dentro de un bucle se obtiene el contenido html, teniendo en cuenta la paginación y la numeración ordinal.
               2. Con el nodo principal se accede a los subnodos que contienen la información para armar un objeto y guardarlos en una lista. 
               3. Cuando el valor del mercado sea menor a 30 millones se detiene el scraping y se obtiene el listado completo.
            */
            var listPlayer = new List<Player>();
            bool requestEnabled = true;
            int numberPage = 0;
            while (requestEnabled)
            {
                numberPage++;
                var doc = Operation.GetHtml(numberPage);
                var tableMarketValues = doc.SelectNodes("//table[@class='items']//tbody//tr[not(ancestor::table[@class='inline-table'])]");
                for (int i = 0; i < tableMarketValues.Count; i++)
                {
                    int marketValue = int.Parse(Regex.Match(tableMarketValues[i].SelectSingleNode("td[6]").InnerText, @"\d+").Value);
                    if (marketValue >= 30)
                    {
                        var oPLayer = new Player()
                        {
                            Id = int.Parse(tableMarketValues[i].SelectSingleNode("td[1]").InnerText),
                            Nombre = tableMarketValues[i].CssSelect("tr td.hauptlink").Single().InnerText,
                            Edad = int.Parse(tableMarketValues[i].SelectSingleNode("td[3]").InnerText),
                            Posicion = tableMarketValues[i].SelectSingleNode(".//table[@class='inline-table']//tr[2]").InnerText,
                            Club = tableMarketValues[i].SelectSingleNode("td[5]//a").Attributes["title"].Value,
                            ValorMercado = marketValue.ToString() + "M €"
                        };
                        listPlayer.Add(oPLayer);
                    }
                    else
                    {
                        requestEnabled = false;
                        break;
                    }
                }
            }
            return Ok(listPlayer);
        }
    }
}
