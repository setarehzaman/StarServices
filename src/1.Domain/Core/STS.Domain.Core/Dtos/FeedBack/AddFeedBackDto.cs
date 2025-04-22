namespace STS.Domain.Core.Dtos.FeedBack;

public class AddFeedBackDto
{
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public int ExpertId { get; set; }
    public int ClientId { get; set; }
}
