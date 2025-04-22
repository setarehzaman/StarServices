using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Entities.User;
public class Expert 
{
    #region Properties

    public int Id { get; set; }
    public string? Biography { get; set; }   
    public int Score { get; set; }
    public int ApplicationUserId { get; set; }
    #endregion

    #region NavigationProperties
    public ApplicationUser ApplicationUser { get; set; }    
    public List<ExpertSkills>? Skills { get; set; }
    public List<FeedBack>? FeedBacks { get; set; }
    public List<Suggestion> Suggestions { get; set; }
    #endregion
}