using BookStoreApi.StaticClasses;
using MongoDB.Driver;
using ProjectTracker.SignalR.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectTracker.SignalR.Helpers
{
    /// <summary>
    /// Ayudante para gestionar las conexiones SignalR y su interacción con MongoDB.
    /// </summary>
    public class SignalRHelper
    {

        /// <summary>
        /// Verifica y gestiona la conexión SignalR basada en el estado de la conexión.
        /// Si ya existe una conexión para el mismo usuario, la borra y crea una nueva.
        /// Si no existe, simplemente inserta una nueva conexión.
        /// </summary>
        /// <param name="statusConnectionSignalRRequest">Estado de la conexión de SignalR a gestionar.</param>
        /// <param name="connectionId">ID de la conexión SignalR.</param>
        public static void CheckAndMageConnection(StatusConnectionSignalR statusConnectionSignalRRequest, string connectionId)
        {
            /*var existingConnection = DBCollections.statusConnectionSignalRCollection
                .Find(t => t.IdUser == statusConnectionSignalRRequest.IdUser)
                .FirstOrDefault();

            if (existingConnection != null && existingConnection.Id != null)
            {
                DeleteConnection(existingConnection.Id);

                InsertNewConnection(statusConnectionSignalRRequest, connectionId);
            }
            else
            {
                InsertNewConnection(statusConnectionSignalRRequest, connectionId);
            }*/
        }


        /// <summary>
        /// Borrar una de las conexiones realizadas con SignalR.
        /// </summary>
        /// <param name="existingConnection"></param>
        private static void DeleteConnection(string existingConnection)
        {
            /*DBCollections.statusConnectionSignalRCollection
                .DeleteOne(t => t.Id == existingConnection);*/
        }

        /// <summary>
        /// Añadir una nueva conexión de SignalR.
        /// </summary>
        /// <param name="statusConnectionSignalRRequest"></param>
        /// <param name="connectionId"></param>
        private static void InsertNewConnection(StatusConnectionSignalR statusConnectionSignalRRequest, string connectionId)
        {
            /*DBCollections.statusConnectionSignalRCollection
                .InsertOne(new StatusConnectionSignalR
                {
                    ConnectionId = connectionId,
                    IsConnected = statusConnectionSignalRRequest.IsConnected,
                    IdUser = statusConnectionSignalRRequest.IdUser,
                });*/
        }

        /// <summary>
        /// Obtener el ID del usuario que ha hecho login para añadirlo a la bd de las conexiones.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "uId");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
            {
                throw new InvalidOperationException("El token JWT no contiene el claim del id del usuario.");
            }
        }
    }
}
