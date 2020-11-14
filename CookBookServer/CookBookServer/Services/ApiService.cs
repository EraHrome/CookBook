using Mongo.Models.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CookBookServer.Services
{
    public class ApiService
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public ApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<RecipeModel>> GetRecipes()
        {
            var response = await _client.GetAsync($"Get");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(responseStream, options);
        }

        public async Task<RecipeModel> GetRecipeById(string id)
        {            
            var response = await _client.GetAsync($"Get/{id}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RecipeModel>(responseStream, options);
        }
        
        public async Task<IEnumerable<RecipeModel>> GetManyRecipiesByIds(IEnumerable<string> ids)
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("GetByIds/", content);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(responseStream, options);
        }

        public async Task<RecipeModel> CreateRecipe(RecipeModel model)
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Create/", content);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RecipeModel>(responseStream, options);
        }
    }
}
