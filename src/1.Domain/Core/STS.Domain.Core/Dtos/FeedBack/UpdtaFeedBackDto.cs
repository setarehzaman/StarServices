namespace STS.Domain.Core.Dtos.FeedBack;

public class UpdtaFeedBackDto
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public bool IsAccepted { get; set; } = false;
}
