using Microsoft.Extensions.Options;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDapperRepository _userDapperRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenSettings _tokenSettings;

        public AuthService(
            IUserDapperRepository userDapperRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IOptions<TokenSettings> tokenSettings)
        {
            _userDapperRepository = userDapperRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<LoginResponseDTO> LoginAsync(string email, string password)
        {
            var user = await _userDapperRepository.GetSensitiveByEmail(email);

            if (user == null)
                throw new UnauthorizedException("Email ou senha inválidos");

            if (!user.Confirmed)
                throw new UnauthorizedException("Essa conta ainda não foi confirmada");

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new UnauthorizedException("Email ou senha inválidos");

            return await GenerateTokensAsync(user);
        }

        public async Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedToken == null || !storedToken.IsActive)
                return null;

            var user = await _userDapperRepository.GetById(storedToken.UserId);
            if (user == null) return null;

            // Revogar token usado
            storedToken.Revoke("Used for refresh");
            await _refreshTokenRepository.UpdateAsync(storedToken);

            // Gerar novos tokens
            return await GenerateTokensAsync(user);
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedToken?.IsActive == true)
            {
                storedToken.Revoke("Manual revocation");
                await _refreshTokenRepository.UpdateAsync(storedToken);
            }
        }

        public async Task RevokeAllUserTokensAsync(int userId)
        {
            await _refreshTokenRepository.RevokeAllByUserIdAsync(userId);
        }

        private async Task<LoginResponseDTO> GenerateTokensAsync(User user)
        {
            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshTokenValue = _tokenGenerator.GenerateSecureToken();

            var refreshToken = RefreshToken.Create(
                refreshTokenValue,
                user.Id,
                TimeSpan.FromDays(_tokenSettings.RefreshTokenExpiryDays));

            await _refreshTokenRepository.AddAsync(refreshToken);

            return new LoginResponseDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessTokenExpiryMinutes),
                RefreshTokenExpiry = refreshToken.ExpiryDate
            };
        }
    }
}
