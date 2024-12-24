using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace DotnetBoilerPlate.Application.Extensions;

public static class Pbkdf2HashExtension
{
    public static string Hash(string input, string salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: input!,
            salt: Encoding.ASCII.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashed;
    }
}