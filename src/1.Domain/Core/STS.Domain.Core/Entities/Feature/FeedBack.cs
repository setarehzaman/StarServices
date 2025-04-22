using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Entities.Feature;

public class FeedBack
{
    #region Properties
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public bool IsAccepted { get; set; } = false;
    public int ExpertId { get; set; }
    public int ClientId { get; set; }
    public bool SoftDelete { get; set; }
    #endregion

    #region NavigationProperties
    public Expert Expert { get; set; }
    public Client Client { get; set; }
    #endregion
}
