using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBot.Models;

namespace TgBot.Client
{
  public class BeerApiClient
    {
      private readonly  HttpClient _client;
        private static  string _address;



        public BeerApiClient()
        {
            _address = Constants.addres;
            
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

       
        public async Task<IEnumerable<BeerInfo>> GetBeerByName(string name)
        {
            var response = await _client.GetAsync($"/BeerFind/beerName?Name={name}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BeerInfo>>(content);
                return result;
        }
        public async Task<IEnumerable<BeerInfo>> GetBeerByAbv(string abv)
        {
            var response = await _client.GetAsync($"/BeerFind/beerAbv?Abv={abv}"); 
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BeerInfo>>(content);
            return result;
        }
        public async Task<IEnumerable<BeerInfo>> GetBeerByIbu(string ibu)
        {
            var response = await _client.GetAsync($"/BeerFind/beerIbu?Ibu={ibu}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BeerInfo>>(content);
            return result;
        }
        public async Task<IEnumerable<BeerInfo>> GetBeerByEbc(string ebc)
        {
            var response = await _client.GetAsync($"/BeerFind/beerEbc?Ebc={ebc}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BeerInfo>>(content);
            return result;
        }
        public async Task<IEnumerable<BeerInfo>> GetBeerByRandom()
        {
            var response = await _client.GetAsync($"/BeerFind/beerRandom"); 

            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BeerInfo>>(content);
            return result;
        }
    }
}
