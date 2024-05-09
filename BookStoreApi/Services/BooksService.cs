using BookStoreApi.Models;
using BookStoreApi.StaticClasses;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApi.Services
{
    public class BooksService
    {
        public IConfiguration _configuration;
        public BooksService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<List<Book>> GetAsync() =>
            await DBCollections.bookCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(string autor) =>
            await DBCollections.bookCollection.Find(x => x.Author == autor).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) =>
            await DBCollections.bookCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Book updatedBook) =>
            await DBCollections.bookCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await DBCollections.bookCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<ErrorOr<dynamic>> Login(string username, string password)
        {
            var user = DBCollections.userCollection.Find(u => u.UserName == username).FirstOrDefault();
            if (user == null)
                throw new ValidationException("No coincide el username.");

            if (user.Status != "Verified")
                throw new ValidationException("Debes verificar tu cuenta antes de hacer login. :(");

            var pass = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!pass)
                throw new ValidationException("La contraseña no coincide");

            var authClaims = new List<Claim>
            {
                new("uId", user.Id),
                new("name", user.UserName),
                new("email", user.Email),
                new("status", user.Status),
                new("contrasena", password),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);
            var writeT = new JwtSecurityTokenHandler().WriteToken(token);

            return "Bearer " + writeT;
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
