using Microsoft.AspNetCore.Http;

namespace STS.Domain.Core.Dtos.ApplicationUser;
public class ApplicationUserDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime RegisteredAt { get; set; }
    public string? Role { get; set; }
    public int RoleId { get; set; }
    public string? CardNumber { get; set; }
    public string? Address { get; set; }
    public string? ImgProfilePath { get; set; }
    public double Balance { get; set; } 
    public string? City { get; set; }
    public int CityId { get; set; }
    public string? Password { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public bool LockoutEnable { get; set; }
    public List<int>? SkillIds { get; set; }     
}