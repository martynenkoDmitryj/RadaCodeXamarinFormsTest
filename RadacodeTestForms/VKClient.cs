using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RadacodeTestForms
{
    public class VKClient
    {
        public Dictionary<string, string> Countries { get; set; }
        public Dictionary<string, string> Cities { get; set; }
        public Dictionary<string, string> Universities { get; set; }

        public VKClient()
        {
            Countries = new Dictionary<string, string>();
            Cities = new Dictionary<string, string>();
            Universities = new Dictionary<string, string>();
        }

        public async Task LoadCountries()
        {
            string json;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/database.getCountries?need_all=1&count=1000");
			WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = await reader.ReadLineAsync();
            }
            SimpleVKJSonParser parser = new SimpleVKJSonParser(json);
            Countries = parser.ParseToDictionary("cid", "title");
        }

        public async Task LoadCities(string country)
        {
            string json;
            string id = Countries.FirstOrDefault(x => x.Value == country).Key;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/database.getCities?country_id=" + id + "&need_all=1&count=1000");
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = await reader.ReadLineAsync();
            }
            SimpleVKJSonParser parser = new SimpleVKJSonParser(json);
            Cities = parser.ParseToDictionary("cid", "title");
        }
        public async Task LoadCities(string country, string toFind)
        {
            string json;
            string id = Countries.FirstOrDefault(x => x.Value == country).Key;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/database.getCities?country_id=" + id + "&need_all=1&count=1000&q=" + toFind);
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = await reader.ReadLineAsync();
            }
            SimpleVKJSonParser parser = new SimpleVKJSonParser(json);
            Cities = parser.ParseToDictionary("cid", "title");
        }
        public async Task LoadUniversities(string country, string city)
        {
            string json;
            string countryId = Countries.FirstOrDefault(x => x.Value == country).Key;
            string cityId = Cities.FirstOrDefault(x => x.Value == city).Key;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/database.getUniversities?country_id=" + countryId + "&city_id=" + cityId + "&need_all=1&count=10000");
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = await reader.ReadLineAsync();
            }
            SimpleVKJSonParser parser = new SimpleVKJSonParser(json);
            Universities = parser.ParseToDictionary("id", "title");
        }
        public async Task LoadUniversities(string country, string city, string toFind)
        {
            string json;
            string countryId = Countries.FirstOrDefault(x => x.Value == country).Key;
            string cityId = Cities.FirstOrDefault(x => x.Value == city).Key;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/database.getUniversities?country_id=" + countryId + "&city_id=" + cityId + "&need_all=1&count=10000&q=" + toFind);
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = await reader.ReadLineAsync();
            }
            SimpleVKJSonParser parser = new SimpleVKJSonParser(json);
            Universities = parser.ParseToDictionary("id", "title");
        }
    }
}
