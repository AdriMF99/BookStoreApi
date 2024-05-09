using BookStoreApi.StaticClasses;
using MongoDB.Driver;
using System.Security.Claims;

namespace BookStoreApi.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validateToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estás enviando un token válido.",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                UserM user = DBCollections.userCollection.Find(t => t.Id == id).FirstOrDefault();

                return new
                {
                    success = true,
                    message = "Éxito",
                    result = user
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
