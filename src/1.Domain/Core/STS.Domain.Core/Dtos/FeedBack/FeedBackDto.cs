using Microsoft.AspNetCore.Identity;
using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Dtos.FeedBack;
public class FeedBackDto
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public int ExpertId { get; set; }
    public int ClientId { get; set; }
    public bool SoftDelete { get; set; }
    public bool IsAccepted  { get; set; }
    public string? ClientName { get; set; }
    public string? ExpertName { get; set; }
    public string? ClientImgPath { get; set; }      
}