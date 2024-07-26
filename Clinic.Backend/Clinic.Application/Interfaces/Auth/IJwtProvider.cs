using Clinic.Core.Models;

namespace Clinic.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}