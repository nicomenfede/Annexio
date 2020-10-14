using Annexio4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Annexio4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<CountriesViewModel> countries;
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/");
                var responseTask = client.GetAsync("all");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<CountriesViewModel>>();
                    readJob.Wait();
                    countries = readJob.Result;
                }
                else
                {
                    countries = Enumerable.Empty<CountriesViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error ocurred");
                }
            }
                return View(countries);
        }

        public ActionResult Country(string name)
        {
            IEnumerable<CountryViewModel> country;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/name/");
                var responseTask = client.GetAsync(name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<CountryViewModel>>();
                    readJob.Wait();
                    country = readJob.Result;
                }
                else
                {
                    country = Enumerable.Empty<CountryViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error ocurred");
                }
            }
            return View(country.First());
        }

        public ActionResult Region(string name)
        {
            IEnumerable<Region> region;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/region/");
                var responseTask = client.GetAsync(name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Region>>();
                    readJob.Wait();
                    region = readJob.Result;
                }
                else
                {
                    region = Enumerable.Empty<Region>();
                    ModelState.AddModelError(string.Empty, "Server error ocurred");
                }
            }
            
            var model = new RegionViewModel();
            model.Name = name;
            
            var countries = new List<string>();
            var subregions = new List<string>();

            foreach (var country in region)
            {
                model.Population += country.Population;
                countries.Add(country.Name);
                if(!IsSubregionInList(country.Subregion, subregions))
                {
                    subregions.Add(country.Subregion);
                }
            }

            model.Countries = countries;
            model.Subregions = subregions;

            return View(model);
        }

        private bool IsSubregionInList(string subregion, IEnumerable<string> myList)
        {
            foreach (var item in myList)
            {
                if (item == subregion) return true;
            }
            return false;
        }
    }
}