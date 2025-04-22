using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Entities.Category;

public class SubCategory : BaseEntity
{
    #region Properties
    public int MainCategoryId { get; set; }
    #endregion

    #region NavigationProperties
    public MainCategory MainCategory { get; set; }
    public List<TaskItem>? Tasks { get; set; }   
    #endregion
}