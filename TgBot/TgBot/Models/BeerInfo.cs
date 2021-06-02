using System;
using System.Collections.Generic;


namespace TgBot.Models
{ 
    public class BeerInfo
    {


        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Description { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        public string Abv { get; set; }
        public string Srm { get; set; }
        public string Ibu { get; set; }
        public string Ebc { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "food_pairing")]
        public IEnumerable<string> FoodPairing { get; set; }
    }
 
}
