namespace STS.Domain.Core.Dtos.ApplicationUser
{
    public class RegisterInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public int RoleId { get; set; }
    }
}