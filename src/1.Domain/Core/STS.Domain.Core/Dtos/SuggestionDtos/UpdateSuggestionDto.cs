namespace STS.Domain.Core.Dtos.SuggestionDtos;

public class UpdateSuggestionDto
{
    public int Id { get; set; }
    public int SuggestedPrice { get; set; }
    public DateTime DoAt { get; set; }
    public bool IsAccepted { get; set; } = false;

}

