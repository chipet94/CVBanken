namespace CVBanken.Data.Models.Requests.Auth
{
    public class UpdateUserRequest
    {
        public bool? Private { get; set; }
        public bool? Searching { get; set; }
        public string? Description { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? ProgrammeId { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
    }
}