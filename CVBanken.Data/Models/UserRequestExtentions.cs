using API_CVPortalen.Models.Auth;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public static class UserRequestExtentions
    {
        public static User ToUser(this UserRequest.Register request)
        {
            return UserBuilder.NewUser(request.Email, request.Password, request.FirstName,request.LastName,request.ProgrammeId, Role.User);
        }
        public static User ToUser(this UserRequest.Update request)
        {
            return new User {FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, ProgrammeId = request.ProgrammeId};
        }
    }
}