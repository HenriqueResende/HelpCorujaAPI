using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLCriptografia : IBLCriptografia
    {
        public readonly IConfiguration _configuration;
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private int Interations;

        public BLCriptografia(IConfiguration configuration)
        {
            _configuration = configuration;

            Interations = _configuration.GetValue<int>("Iterations");
        }

        #region Hash
        /// <summary>
        /// Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Interations,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Interations}.{salt}.{key}";
            }
        }
        #endregion

        #region Check
        /// <summary>
        /// Check
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }
        #endregion

        #region CreateToken
        /// <summary>
        /// CreateToken
        /// </summary>
        /// <param name="ra"></param>
        /// <returns></returns>
        public string CreateToken(string ra)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, ra)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
