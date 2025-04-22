using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Entities.User;
public class Client
{
    public int Id { get; set; }
    public int ApplicationUserId { get; set; }

    #region NavigationProperties
    public ApplicationUser ApplicationUser { get; set; }
    public List<Order> Orders { get; set; }
    public List<FeedBack> FeedBacks { get; set; }
    #endregion
}