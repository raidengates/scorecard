using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scorecard.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Scorecard.Business.Utilitys
{

    public static class CommonUtility
    {
        /// <summary>
        /// Free
        /// Pro
        /// Enterprise
        /// </summary>
        public static string FreePermission = "FREE";
        public static string ProPermission = "PRO";
        public static string EnterprisePermission = "ENTERPRISE";
        public static string AdminPermission = "ADMIN";
        public static string SaPermission = "SUPERACCOUNT";
        private static readonly RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
        static string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
        public static string PolicyPrefix = "REVERSEPROXY.PERMISSION";


        public static User CreateUser(this string Username, string Password, string Email, bool isAdmin = false)
        {
            string CreatedBy = Username;
            DateTime CreatedDateTime = DateTime.Now;
            string LastUpdatedBy = Username;
            DateTime LastUpdatedDateTime = DateTime.Now;

            var result = new User
            {
                Username = Username,
                Password = BC.HashPassword(Password),
                Email = Email,
                CreatedBy = CreatedBy,
                CreatedDateTime = CreatedDateTime,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDateTime = LastUpdatedDateTime,
            };
            var Permissions = new List<Permission>
            {
                new Permission() {
                    Role=Role.User,
                    Name=Role.User.ToString(),
                    CreatedBy = CreatedBy,
                    CreatedDateTime = CreatedDateTime,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDateTime = LastUpdatedDateTime,
                },

            };
            if (isAdmin)
            {
                Permissions.AddRange(new List<Permission>
            {
                new Permission() {
                    Role=Role.Admin,
                    Name=Role.Admin.ToString(),
                    CreatedBy = CreatedBy,
                    CreatedDateTime = CreatedDateTime,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDateTime = LastUpdatedDateTime,
                },
            });
            }
            result.Permissions = Permissions;
            return result;
        }

        //// Generate Token

        public static string generateJwtToken(this string Secret, string id, int expiresMinutes = 5)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id) }),
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// Get random value
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxExclusiveValue"></param>
        /// <returns></returns>
        public static int Next(int minValue, int maxExclusiveValue)
        {
            if (minValue == maxExclusiveValue) return minValue;

            if (minValue > maxExclusiveValue)
            {
                throw new ArgumentOutOfRangeException($"{nameof(minValue)} must be lower than {nameof(maxExclusiveValue)}");
            }

            var diff = (long)maxExclusiveValue - minValue;
            var upperBound = uint.MaxValue / diff * diff;

            uint ui;
            do
            {
                ui = GetRandomUInt();
            } while (ui >= upperBound);
            return (int)(minValue + (ui % diff));
        }
        private static uint GetRandomUInt()
        {
            var randomBytes = GenerateRandomBytes(sizeof(uint));
            return BitConverter.ToUInt32(randomBytes, 0);
        }
        private static byte[] GenerateRandomBytes(int bytesNumber)
        {
            var buffer = new byte[bytesNumber];
            csp.GetBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 10,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };


            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(Next(0, chars.Count),
                    randomChars[0][Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(Next(0, chars.Count),
                    randomChars[1][Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(Next(0, chars.Count),
                    randomChars[2][Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(Next(0, chars.Count),
                    randomChars[3][Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[Next(0, randomChars.Length)];
                chars.Insert(Next(0, chars.Count),
                    rcs[Next(0, rcs.Length)]);
            }
            return new string(chars.ToArray());
        }

        static long counter; //store and load the counter from persistent storage every time the program loads or closes.

        public static string CreateRandomString(int length)
        {
            long count = System.Threading.Interlocked.Increment(ref counter);
            int PasswordLength = length;
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                while (randomBytes[i] > byte.MaxValue - (byte.MaxValue % allowedCharCount))
                {
                    byte[] tmp = new byte[1];
                    rng.GetBytes(tmp);
                    randomBytes[i] = tmp[0];
                }
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }
            byte[] buf = new byte[8];
            buf[0] = (byte)count;
            buf[1] = (byte)(count >> 8);
            buf[2] = (byte)(count >> 16);
            buf[3] = (byte)(count >> 24);
            buf[4] = (byte)(count >> 32);
            buf[5] = (byte)(count >> 40);
            buf[6] = (byte)(count >> 48);
            buf[7] = (byte)(count >> 56);
            return Convert.ToBase64String(buf) + new string(chars);
        }

        public static string RandomGenerator(int length = 128)
        {
            string g = CreateRandomString(length);
            string GuidString = Convert.ToBase64String(Encoding.ASCII.GetBytes(g));
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            GuidString = GuidString.Replace("\\", "");
            return GuidString;
        }


    }

}
