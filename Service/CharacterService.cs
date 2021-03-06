using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaidPlannerClient.Model;

namespace RaidPlannerClient.Service {
    public class CharacterService : ICharacterService {
        private readonly HttpClient httpClient;

        public CharacterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Character> AddCharacter(Player player, Character character) {
            Console.WriteLine("CharacterService::AddCharacter");

            var json = JsonConvert.SerializeObject(character);
            Console.WriteLine($"POSTing to rest/players/{player.Id}/characters");
            var result = await httpClient.PostAsync($"rest/players/{player.Id}/characters", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();

            var resultJson = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Character>(resultJson);
        }

        public async void UpdateCharacter(Player player, Character character) {
            Console.WriteLine("CharacterService::UpdateCharacter");

            var json = JsonConvert.SerializeObject(character);
            Console.WriteLine($"PUTing to rest/players/{player.Id}/characters/{character.Id}");
            var result = await httpClient.PutAsync($"rest/players/{player.Id}/characters/{character.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }
        public async void DeleteCharacter(Player player, Character character) {
            Console.WriteLine("CharacterService::DeleteCharacter");

            Console.WriteLine($"DELETEing to rest/players/{player.Id}/characters/{character.Id}");
            var result = await httpClient.DeleteAsync($"rest/players/{player.Id}/characters/{character.Id}");
            result.EnsureSuccessStatusCode();
        }
    }
}