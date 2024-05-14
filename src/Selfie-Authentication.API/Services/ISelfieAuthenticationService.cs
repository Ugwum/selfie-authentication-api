using Selfie_Authentication.API.Model;

namespace Selfie_Authentication.API.Services
{
    public interface ISelfieAuthenticationService
    {
        Task<RequestResult> RegisterUserSelfie(string base64Image);

        Task<RequestResult> AuthenticateUserSelfie(string base64Image);
    }
}
