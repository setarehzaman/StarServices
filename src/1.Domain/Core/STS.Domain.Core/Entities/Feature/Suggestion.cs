using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Entities.Feature;
public class Suggestion
{
    #region Properties
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ExpertId { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime DoAt { get; set; }
    public double SuggestedPrice { get; set; }
    public bool IsAccepted { get; set; } = false;
    #endregion

    #region NavigationProperties
    public Expert Expert { get; set; }
    public Order Order { get; set; }
    #endregion
}

