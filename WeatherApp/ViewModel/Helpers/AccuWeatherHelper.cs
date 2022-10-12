using WeatherApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp.ViewModel.Helpers;

public class AccuWeatherHelper
{
    public const string Base_URL = "http://dataservice.accuweather.com/";
    public const string Autocomplete_Endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
    public const string Current_Conditions_Endpoint = "currentconditions/v1/{0}?apikey={1}";
    public const string API_Key = "CuGjQMDuhl4q2wB1Sdm7H7i3RbluAGzJ";
    //QE9GcZyuBWkM1PADKpWFNkuelPuVqOde
    //Sx1OF3piASbTJyfXPVv0HJm65AWLnTV6
    //hzAgJdrLIqaFHEPGANsESlEJfBaQS8l6

    public static async Task<List<City>> GetCities(string query)
    {
        var cities = new List<City>();
        string url = Base_URL + string.Format(Autocomplete_Endpoint, API_Key, query);

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            cities = JsonConvert.DeserializeObject<List<City>>(json);
        }

        return cities;
    }

    public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
    {
        var currentConditions = new CurrentConditions();
        
        var url = Base_URL + string.Format(Current_Conditions_Endpoint, cityKey, API_Key);

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();
            currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
        }

        return currentConditions;
    }
}