using System.Text.RegularExpressions;
using UserIdentifierService.Models;
using UserIdentifierService.Repositories;

namespace UserIdentifierService.Services;

public class AvatarService
{
    private readonly AvatarRepository _repository;

    public AvatarService(AvatarRepository repository)
    {
        _repository = repository;
    }

    public AvatarProfile GetAvatarUrl(string userIdentifier)
    {
        string imageUrl = null;
        char lastChar = userIdentifier[^1];

        if ("6789".Contains(lastChar))
        {
            imageUrl = $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastChar}";
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