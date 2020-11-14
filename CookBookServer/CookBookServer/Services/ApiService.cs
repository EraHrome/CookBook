using System.Collections.Generic;
using System.Threading.Tasks;
using Mongo.Models.Recipe;
using System.Text.Json;
using System.Net.Http;
using System.Text;

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
            var response = await _client.GetAsync($"Recipe/Get");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(responseStream, options);
        }

        public async Task<RecipeModel> GetRecipeById(string id)
        {            
            var response = await _client.GetAsync($"Recipe/Get/{id}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RecipeModel>(responseStream, options);
        }
        
        public async Task<IEnumerable<RecipeModel>> GetManyRecipiesByIds(IEnumerable<string> ids)
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ids ?? new List<string>()), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Recipe/GetByIds/", content);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(responseStream, options);
        }

        public async Task<RecipeModel> CreateRecipe(RecipeModel model)
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Recipe/Create/", content);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RecipeModel>(responseStream, options);
        }
    }
}
