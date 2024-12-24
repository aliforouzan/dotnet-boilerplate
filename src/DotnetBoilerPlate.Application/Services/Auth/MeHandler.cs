using Ardalis.Result;
using Mapster;
using MediatR;
using DotnetBoilerPlate.Application.Dto.Auth.Me;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Auth;

public class MeHandler : IRequestHandler<MeRequestDto, Result<MeResponseDto>>
{
    private readonly IUserRepository _userRepository;

    public MeHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<MeResponseDto>> Handle(MeRequestDto meRequestDto,
        CancellationToken cancellationToken)
    {
        User? user;

        try
        {
            user = await _userRepository.GetByIdAsync(meRequestDto.UserId);

            if (user is null)
            {
                return Result.NotFound("کاربر یافت نشد");
            }
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }

        try
        {
            return user.Adapt<MeResponseDto>();
        }
        catch (Exception e)
        {
            return CustomError.InternalServerError;
        }
    }
}