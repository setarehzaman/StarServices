namespace STS.Domain.Core.Dtos.SuggestionDtos;

public class AddSuggestionDto
{
    public int OrderId { get; set; }
    public int ExpertId { get; set; }
    public DateTime DoAt { get; set; }
    public double SuggestedPrice { get; set; }
}

