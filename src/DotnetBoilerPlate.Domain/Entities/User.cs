using Microsoft.Extensions.Configuration;
using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Domain.Extentions;
using DotnetBoilerPlate.Shared.Extentions;
using DotnetBoilerPlate.Shared.Types;
using System;
using System.Collections.Generic;

namespace DotnetBoilerPlate.Domain.Entities;

public class User : Entity<UserId>
{
    private readonly IConfiguration _config;
    public override UserId Id { get; set; } = Guid.CreateVersion7();

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string NationalCode { get; set; }

    public string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; }


    public string Type { get; set; }

    public string? Status { get; set; }

    public int? Level { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? CityId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? BlockedAt { get; set; }
    
    public virtual City? City { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public User(string firstname, string lastname, string nationalCode, string phoneNumber, string? email,
        string password, string passwordSalt, DateOnly? birthday, int? cityId, string? address, string? postalCode)
    {
        Firstname = firstname;
        Lastname = lastname;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
        Email = string.IsNullOrEmpty(email) ? null : email;
        Password = Pbkdf2HashExtension.Hash(password, passwordSalt);
        Type = UserType.Customer.ToString();
        Status = UserStatus.Unauthorized.ToString();
        Level = (int) UserLevel.L1;
        Birthday = birthday;
        CityId = IdValidator.IsValid(CityId) ? cityId : null;
        Address = string.IsNullOrEmpty(address) ? null : address;
        PostalCode = string.IsNullOrEmpty(address) ? null : address;
        CreatedAt = DateTime.Now;
    }

    public User() { }

    public bool IsBlocked()
    {
        return Status == UserStatusHelper.GetStatusTypeString(UserStatus.Blocked);
    }
}