﻿using System;
namespace Application.Features.Auth.Constants;


public static class AuthMessages
{
    public const string EmailAuthenticatorDontExists = "Email authenticator don't exists.";
    public const string OtpAuthenticatorDontExists = "Otp authenticator don't exists.";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Already verified otp authenticator is exists.";
    public const string EmailActivationKeyDontExists = "Email Activation Key don't exists.";
    public const string UserDontExists = "User don't exists.";
    public const string UserHaveAlreadyAAuthenticator = "User have already a authenticator.";
    public const string RefreshDontExists = "Refresh don't exists.";
    public const string RefreshTokenShouldNotBeExpired = "Refresh token should not be expired";
    public const string InvalidRefreshToken = "Invalid refresh token.";
    public const string UserMailAlreadyExists = "User mail already exists.";
    public const string PasswordDontMatch = "Password don't match.";
    public const string EmailOrPasswordShouldNotBeInvalid = "Email or password should not be invalid";
    public const string EmailAddressShouldBeValid = "Email address should be valid";
}
