using System.Text.Json;
using System.Text.RegularExpressions;
using UserIdentifierService.Models;
using UserIdentifierService.Repositories;

namespace UserIdentifierService.Services;

public class AvatarService
{
    private readonly AvatarRepository _repository;
    private readonly HttpClient _httpClient;

    public AvatarService(AvatarRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    private async Task<string> GetImageUrlFromJsonApi(char lastChar)
    {
        string apiUrl = $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastChar - '0'}";

        try
        {
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve image URL from JSON API. Status code: {response.StatusCode}");
                return "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseContent);
            var imageUrl = jsonDocument.RootElement.GetProperty("url").GetString();

            return imageUrl ?? "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to retrieve image URL from JSON API: {ex.Message}");
            return "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        }
    }


    public async Task<AvatarProfile> GetAvatarUrlAsync(string userIdentifier)
    {
        string? imageUrl = null;
        char lastChar = userIdentifier[^1];

        if ("6789".Contains(lastChar))
        {
            imageUrl = await GetImageUrlFromJsonApi(lastChar);
        }
        else if ("12345".Contains(lastChar))
        {
            imageUrl = _repository.GetImageUrlById(lastChar - '0');
        }
        else if (Regex.IsMatch(userIdentifier, "[aeiouAEIOU]"))
        {
            imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";
        }
        else if (Regex.IsMatch(userIdentifier, "[^a-zA-Z0-9]"))
        {
            var random = new Random();
            int seed = random.Next(1, 6);
            imageUrl = $"https://api.dicebear.com/8.x/pixel-art/png?seed={seed}&size=150";
        }
        else
        {
            imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        }

        return new AvatarProfile { Url = imageUrl };
    }
}