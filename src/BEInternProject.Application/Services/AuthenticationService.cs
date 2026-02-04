using AutoMapper;
using BEInternProject.Application.DTOs;
using BEInternProject.Application.Interfaces;
using BEInternProject.Domain.Entities;

namespace BEInternProject.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public AuthenticationService(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IJwtTokenGenerator jwtTokenGenerator, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists");
        }

        var user = _mapper.Map<User>(registerDto);
        user.PasswordHash = _passwordHasher.Hash(registerDto.Password);
        user.Role = "User"; // Default role

        var userId = await _userRepository.CreateAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(userId, user.Username, user.Role);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null || !_passwordHasher.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new Exception("Invalid credentials");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Role);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username
        };
    }
}
