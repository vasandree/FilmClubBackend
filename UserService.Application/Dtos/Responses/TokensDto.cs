using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Responses;

public class TokensDto(string accessToken, string? refreshToken)
{
    [Required]
    [MinLength(1)]
    public string AccessToken { get; set; } = accessToken;
    
    public string? RefreshToken { get; set; } = refreshToken;
}