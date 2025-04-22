using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Entities.BaseEntities;
public class Picture
{
    #region Properties
    public int Id { get; set; }
    public string Path { get; set; }
    public int OrderId { get; set; }
    #endregion

    #region NavigationProperties
    public Order? Order { get; set; }
    #endregion
}