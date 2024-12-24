using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Configuration;
using DotnetBoilerPlate.Application.Dto.Auth.Register;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Extentions;
using DotnetBoilerPlate.Shared.Statics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Auth;

public class RegisterHandler : IRequestHandler<RegisterRequestDto, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IConfiguration _config;

    public RegisterHandler(ICityRepository cityRepository, IUserRepository userRepository, IConfiguration config)
    {
        _cityRepository = cityRepository;
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<Result> Handle(RegisterRequestDto registerRequestDto,
        CancellationToken cancellationToken)
    {
        if (IdValidator.IsValid(registerRequestDto.CityId))
        {
            try
            {
                City? city = await _cityRepository.GetByIdAsync(registerRequestDto.CityId);
                if (city is null)
                {
                    return Result.NotFound("شهر یافت نشد");
                }
            }
            catch (Exception e)
            {
                return CustomError.DbError;
            }
        }

        try
        {
            User? existUser =
                await _userRepository.GetByNationalCodeAsync(registerRequestDto.NationalCode);
            if (existUser != null)
            {
                return Result.Conflict("این کد ملی قبلا ثبت شده است");
            }

            existUser = await _userRepository.GetByPhoneNumberAsync(registerRequestDto.PhoneNumber);
            if (existUser != null)
            {
                return Result.Conflict("این شماره همراه قبلا ثبت شده است");
            }
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }

        User newUser = new(
            registerRequestDto.Firstname,
            registerRequestDto.Lastname,
            registerRequestDto.NationalCode,
            registerRequestDto.PhoneNumber,
            registerRequestDto.Email,
            registerRequestDto.Password,
            _config["Hashing:Salt"],
            registerRequestDto.Birthday,
            registerRequestDto.CityId,
            registerRequestDto.Address,
            registerRequestDto.PostalCode
        );
        
        try
        {
            _userRepository.Add(newUser);
            _userRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }
        
        return Result.Success();
    }
}