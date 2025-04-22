using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;
using System.Globalization;
using STS.Domain.Core.Dtos.SuggestionDtos;

namespace STS.Domain.Core.Dtos.OrderDtos;

public class OrderDto
{
    public int Id { get; set; }
    public double OrderedPrice { get; set; }
    public DateTime DoAt { get; set; }
    public int TaskId { get; set; }
    public int ClientId { get; set; }
    public bool IsAccepted { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }
    public string ClientName { get; set; }
    public string TaskName { get; set; }
    public int SuggestionsCount { get; set; }
    public List<string>? ImagesPath { get; set; }
    public List<SuggestionDto> Suggestions { get; set; }
    public bool HasAcceptedSuggestion { get; set; }
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; }
    public string? MainCategoryImgAddress { get; set; }
    public bool AlreadyHasSuggestion { get; set; }
}
