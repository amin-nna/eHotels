using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace eHotels.Userdefineddomains
{
    public class CityProvider
    {
        public static async Task<List<string>> GetCitiesInCanadaAsync()
        {
            string apiUrl = "http://api.geonames.org/searchJSON?country=CA&featureClass=P&maxRows=1000&username=geona";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<GeoNamesResult>(content);

                    var cities = result.Cities
                        .FindAll(city => !string.IsNullOrWhiteSpace(city.Name))
                        .ConvertAll(city => city.Name);

                    return cities;
                }
                else
                {
                    throw new Exception($"Failed to retrieve cities: {response.ReasonPhrase}");
                }
            }
        }
    }

    public class GeoNamesCity
    {
        [JsonProperty("toponymName")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Province { get; set; }
    }

    public class GeoNamesResult
    {
        [JsonProperty("geonames")]
        public List<GeoNamesCity> Cities { get; set; }
    }
}
