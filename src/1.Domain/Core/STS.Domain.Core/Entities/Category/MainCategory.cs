using STS.Domain.Core.Entities.BaseEntities;
namespace STS.Domain.Core.Entities.Category;

public class MainCategory : BaseEntity
{
    #region Properties
    public string ImgPath { get; set; }
    #endregion

    #region NavigationProperties
    public List<SubCategory> SubCategories { get; set; }
    #endregion
}