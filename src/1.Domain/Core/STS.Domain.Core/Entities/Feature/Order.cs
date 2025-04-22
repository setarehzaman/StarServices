using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;

public class Order
{
    #region Properties
    public int Id { get; set; }
    public double OrderedPrice { get; set; }
    public int TaskId { get; set; }
    public int ClientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DoAt { get; set; }
    public bool IsAccepted { get; set; } = false;
    public string? Description { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }

    #endregion

    #region NavigationProperties
    public List<Suggestion> Suggestions { get; set; }
    public TaskItem Task { get; set; }
    public Client Client { get; set; }
    public List<Picture> Pictures { get; set; }
    #endregion
}
