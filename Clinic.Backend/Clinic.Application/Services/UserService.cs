using Clinic.Application.Interfaces.Auth;
using Clinic.Core.Enums;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Register(
        string firstName,
        string lastName,
        string fatherName,
        DateOnly dateOfBirth,
        string email,
        string phoneNumber,
        string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var userResult = User.Create(
            Guid.NewGuid(),
            firstName,
            lastName,
            fatherName,
            phoneNumber,
            dateOfBirth,
            null,
            email,
            null,
            hashedPassword);

        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error);
        }

        await _usersRepository.Add(userResult.Value);
        return Result.Success();
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);

        if (user == null)
        {
            return Result.Failure<string>("User not found");
        }

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
        {
            return Result.Failure<string>("Invalid password");
        }

        var token = _jwtProvider.Generate(user);

        return Result.Success(token);
    }

    public async Task<Result> Update(
        Guid id,
        string firstName,
        string lastName,
        string fatherName,
        DateOnly dateOfBirth,
        Guid? addressId,
        Guid? imageId)
    {
        await _usersRepository.Update(id, firstName, lastName, fatherName, dateOfBirth, addressId, imageId);
        return Result.Success();
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var result = await _usersRepository.GetUserPermissions(userId);

        return result;
    }
}