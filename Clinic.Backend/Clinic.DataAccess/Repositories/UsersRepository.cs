using Clinic.Core.Enums;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ClinicDbContext _context;

    public UsersRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        var roleEntity = await _context.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
            ?? throw new InvalidOperationException("Role not found.");

        var userEntity = new UserEntity()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FatherName = user.FatherName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = user.PasswordHash,
            Roles = new List<RoleEntity> { roleEntity }
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new Exception("User not found.");

        var user = User.Create(
            userEntity.Id,
            userEntity.FirstName,
            userEntity.LastName,
            userEntity.FatherName,
            userEntity.PhoneNumber,
            userEntity.DateOfBirth,
            userEntity.ImageId,
            userEntity.Email,
            userEntity.Description,
            userEntity.PasswordHash).Value;

        return user;
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToListAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }

    public async Task Update(
         Guid id,
         string firstName,
         string lastName,
         string? fatherName,
         DateOnly dateOfBirth,
         Guid? addressId,
         Guid? imageId)
    {
        await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.FirstName, firstName)
                .SetProperty(x => x.LastName, lastName)
                .SetProperty(x => x.FatherName, fatherName)
                .SetProperty(x => x.DateOfBirth, dateOfBirth)
                .SetProperty(x => x.AddressId, addressId)
                .SetProperty(x => x.ImageId, imageId));
    }
    public async Task<User?> GetById(Guid id)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null)
        {
            return null; 
        }

        var user = User.Create(
            userEntity.Id,
            userEntity.FirstName,
            userEntity.LastName,
            userEntity.FatherName,
            userEntity.PhoneNumber,
            userEntity.DateOfBirth,
            userEntity.ImageId,
            userEntity.Email,
            userEntity.Description,
            userEntity.PasswordHash).Value;

        return user;
    }

}