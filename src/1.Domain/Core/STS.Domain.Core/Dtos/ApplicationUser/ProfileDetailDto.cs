namespace STS.Domain.Core.Dtos.ApplicationUser
{
    public class ProfileDetailDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
