using AutoMapper;
using E_Commerce.API.Common;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<Results<UserDto>> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Results<UserDto>.Fail(Errors.NotFound("User.NotFound"));

            return Results<UserDto>.OK(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user)
            });
        }

        public async Task<Results<AddressDto>> GetUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            if (user == null) return Results<AddressDto>.Fail(Errors.NotFound("User.NotFound"));

            return Results<AddressDto>.OK(_mapper.Map<AddressDto>(user.Address));
        }

        public async Task<Results<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Results<UserDto>.Fail(Errors.Unauthorized("Login.InvalidCredentials"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Results<UserDto>.Fail(Errors.Unauthorized("Login.InvalidCredentials"));

            return Results<UserDto>.OK(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user)
            });
        }

        public async Task<Results<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExistsAsync(registerDto.Email))
            {
                return Results<UserDto>.Fail(Errors.Conflict("Register.EmailInUse"));
            }

            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return Results<UserDto>.Fail(Errors.Failure("Register.Failed"));

            return Results<UserDto>.OK(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user)
            });
        }

        public async Task<Results<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            if (user == null) return Results<AddressDto>.Fail(Errors.NotFound("User.NotFound"));

            user.Address = _mapper.Map<Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return Results<AddressDto>.Fail(Errors.Failure("Address.UpdateFailed"));

            return Results<AddressDto>.OK(_mapper.Map<AddressDto>(user.Address));
        }
    }
}
