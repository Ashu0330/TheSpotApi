using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtistHub.Presentation.Helper
{
    public class Utilities
    {
        public static class PasswordHasher
        {
            private const int SaltSize = 16;
            private const int KeySize = 32;
            private const int Iterations = 210_000;

            private static readonly Regex PasswordRegex =
                new(@"^(?=.*[A-Z])(?=.*[\W_]).{8,}$", RegexOptions.Compiled);

            public static string Pepper { get; set; } = string.Empty;

            public static bool IsPasswordValid(string password)
                => PasswordRegex.IsMatch(password);

            public static string HashPassword(string password)
            {
                if (!IsPasswordValid(password))
                    throw new Exception("Password must be minimum 8 chars, 1 capital, 1 special char.");

                byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
                byte[] hash = new byte[KeySize];

                Span<byte> pwdBytes = Encoding.UTF8.GetBytes(password + Pepper);

                Rfc2898DeriveBytes.Pbkdf2(
                    pwdBytes,
                    salt,
                    hash,
                    Iterations,
                    HashAlgorithmName.SHA256);

                return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
            }

            public static bool VerifyPassword(string password, string storedHash)
            {
                var parts = storedHash.Split('.');
                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] originalHash = Convert.FromBase64String(parts[2]);

                byte[] newHash = new byte[originalHash.Length];
                Span<byte> pwdBytes = Encoding.UTF8.GetBytes(password + Pepper);

                Rfc2898DeriveBytes.Pbkdf2(
                    pwdBytes,
                    salt,
                    newHash,
                    iterations,
                    HashAlgorithmName.SHA256);

                return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
            }
        }
    }
}