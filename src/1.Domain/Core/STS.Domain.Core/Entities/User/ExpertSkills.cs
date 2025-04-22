using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Entities.User;
public class ExpertSkills
{
    #region Properties
    public int ExpertId { get; set; }
    public int TaskId { get; set; }
    #endregion

    #region NavigationProperties
    public Expert Expert { get; set; }
    public TaskItem Task { get; set; }
    #endregion
}