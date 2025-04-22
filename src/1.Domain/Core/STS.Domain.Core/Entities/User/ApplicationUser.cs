using Microsoft.AspNetCore.Identity;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Enums;
namespace STS.Domain.Core.Entities.User;
public class ApplicationUser : IdentityUser<int>
{
    #region Properties
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CardNumber { get; set; }
    public string? Address { get; set; }
    public int CityId { get; set; }
    public string? ImgProfilePath { get; set; }
    public double Balance { get; set; } 
    public DateTime RegisteredAt { get; set; }
    public bool SoftDelete { get; set; }
    #endregion

    #region NavigationProperties
    public City City { get; set; }
    public Expert? Expert { get; set; }
    public Client? Client { get; set; }
    #endregion
}
