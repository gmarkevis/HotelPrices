using HotelPrices.Models;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelPrices.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuartos(DateTime checkIn, DateTime checkOut, int numeroAdultos)
        {
            List<HotelQuarto> quartos = _context.HotelQuarto.Where(x => x.CheckIn == checkIn && x.CheckOut == checkOut && x.NumeroAdultos >= numeroAdultos).ToList();

            if (quartos.Count == 0)
            {
                try
                {
                    WebScrapperPrecos(checkIn, checkOut, numeroAdultos);
                    quartos = _context.HotelQuarto.Where(x => x.CheckIn == checkIn && x.CheckOut == checkOut && x.NumeroAdultos >= numeroAdultos).ToList();
                    throw new Exception("Tivemos um problema em buscar os quartos. Realize a pesquisa novamente por gentileza.");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return PartialView("_Quartos", quartos);
                }
            }

            foreach (var quarto in quartos)
            {
                quarto.Tarifas = _context.HotelQuartoTarifa.Where(x => x.HotelQuartoId == quarto.Id).ToList();
            }

            return PartialView("_Quartos", quartos);
        }

        public void WebScrapperPrecos(DateTime checkIn, DateTime checkOut, int numeroAdultos)
        {
            string url = "https://testautomation.letsbook.com.br/D/Reserva?checkin=" + checkIn.Date.ToString("dd/MM/yyyy")
                + "&checkout=" + checkOut.Date.ToString("dd/MM/yyyy") + "&cidade=&hotel=14&adultos="
                + numeroAdultos + "&criancas=&destino=Hotel+QA+Absoluto&promocode=&tarifa=&mesCalendario=4%2F12%2F2022";

            using (var driver = new ChromeDriver(Environment.CurrentDirectory + "\\bin\\Debug\\netcoreapp3.1"))
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(url);

                //Retorna os elementos html referente aos quartos
                var quartoElements = driver.FindElements(By.CssSelector("#tblAcomodacoes > tbody > .row-quarto"));

                foreach (IWebElement element in quartoElements)
                {
                    HotelQuarto hotelQuarto = new HotelQuarto()
                    {
                        Nome = element.FindElement(By.ClassName("quartoNome")).Text,
                        NumeroAdultos = int.Parse(element.FindElement(By.ClassName("adultos")).GetAttribute("data-n")),
                        CheckIn = checkIn,
                        CheckOut = checkOut,
                        HotelId = 14
                    };

                    _context.Add(hotelQuarto);
                    _context.SaveChanges();

                    var tarifasQuarto = element.FindElements(By.ClassName("row-tarifa"));
                    List<HotelQuartoTarifa> quartoTarifas = new List<HotelQuartoTarifa>();

                    foreach (IWebElement tarifa in tarifasQuarto)
                    {
                        string nomeTarifa = tarifa.FindElement(By.ClassName("nomeTarifa")).GetAttribute("outerHTML");

                        string condicaoQuarto = "";

                        condicaoQuarto += nomeTarifa + "<br>";

                        var condicaoItems = tarifa.FindElements(By.ClassName("marker"));
                        foreach (IWebElement item in condicaoItems)
                        {
                            condicaoQuarto += "<br>" + item.FindElement(By.TagName("span")).GetAttribute("outerHTML");
                        }

                        string valorMedio = RemoverHTML(tarifa.FindElement(By.ClassName("tarifa-dia-valor-medio-value")).GetAttribute("outerHTML"));
                        string valorTotal = RemoverHTML(tarifa.FindElement(By.ClassName("tarifa-dia-valor-total-value")).GetAttribute("outerHTML"));

                        HotelQuartoTarifa hotelTarifa = new HotelQuartoTarifa()
                        {
                            HotelQuartoId = hotelQuarto.Id,
                            Condicao = condicaoQuarto,
                            ValorMedio = valorMedio,
                            ValorTotal = valorTotal
                        };

                        _context.Add(hotelTarifa);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public string RemoverHTML(string texto)
        {
            return Regex.Replace(texto, "<.*?>", "");
        }
    }
}
