using System;
using Application.Features.Auth.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Auth.Rules;


public class AuthRules : BaseBusinessRules
{
    public Task UserMailAlreadyExists(AppUser? user)
    {
        if (user is not null) throw new BusinessException(AuthMessages.UserMailAlreadyExists);
        return Task.CompletedTask;
    }
    public Task EmailOrPasswordShouldNotBeInvalid(AppUser? user, bool checkPassword)
    {
        if (user is null || !checkPassword) throw new BusinessException(AuthMessages.EmailOrPasswordShouldNotBeInvalid);
        return Task.CompletedTask;
    }
    public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
    {
        if (expiryDate <= DateTime.Now) throw new BusinessException(AuthMessages.RefreshTokenShouldNotBeExpired);
        return Task.CompletedTask;
    }

    public Task UserDontExists(AppUser? user)
    {
        if (user is null) throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }




}