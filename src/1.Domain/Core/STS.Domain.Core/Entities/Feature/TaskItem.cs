using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Category;
using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Entities.Feature;
public class TaskItem : BaseEntity
{
    #region Properties
    public int? SubCategoryId { get; set; }
    public string? ImgPath { get; set; }
    public int BasePrice { get; set; }
    #endregion

    #region NavigationProperties
    public List<Order> Orders { get; set; }
    public SubCategory SubCategory { get; set; }
    public List<ExpertSkills> Experts { get; set; }
    #endregion
}