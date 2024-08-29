﻿using Clinic.Application.Interfaces.Auth;
using Clinic.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clinic.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(User user)
        {
            // Создание списка claims
            var claims = new[]
            {
                new Claim(CustomClaims.UserId, user.Id.ToString())
            };

            // Создание объекта SigningCredentials с использованием секретного ключа
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            // Создание JWT-токена
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,  // Добавлен Issuer
                audience: _options.Audience,  // Добавлена Audience
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
                signingCredentials: signingCredentials);

            // Преобразование токена в строку
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
