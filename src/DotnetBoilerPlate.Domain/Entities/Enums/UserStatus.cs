
using System;

namespace DotnetBoilerPlate.Domain.Entities.Enums;

public enum UserStatus
{
    Normal = 0,
    Unauthorized = 1,
    Blocked = 2
}

public static class UserStatusHelper
{
    public static string GetStatusTypeString(UserStatus status)
    {
        return status switch
        {
            UserStatus.Normal => "Normal",
            UserStatus.Unauthorized => "Unauthorized",
            UserStatus.Blocked => "Blocked",
            _ => "Undefined"
        };
    }
}
