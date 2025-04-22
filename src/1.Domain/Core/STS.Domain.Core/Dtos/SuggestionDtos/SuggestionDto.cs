using System.Reflection.PortableExecutable;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.Core.Dtos.SuggestionDtos;

public class SuggestionDto
{
    public int Id { get; set; }
    public double SuggestedPrice { get; set; }
    public DateTime DoAt { get; set; }
    public int OrderId { get; set; }
    public int ExpertId { get; set; }
    public string ExpertName { get; set; }
    public bool IsAccepted { get; set; }
    public string? ImageProfilePath { get; set; }
    public string? TaskName { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }

}

