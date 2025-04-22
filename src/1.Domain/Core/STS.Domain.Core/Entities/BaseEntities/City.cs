using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Entities.BaseEntities;
public class City : BaseEntity
{
    #region NavigationProperties
    public List<ApplicationUser> Users { get; set; }
    #endregion
}