namespace STS.Domain.Core.Entities.BaseEntities;
public class BaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public bool SoftDelete { get; set; }
    #endregion
}