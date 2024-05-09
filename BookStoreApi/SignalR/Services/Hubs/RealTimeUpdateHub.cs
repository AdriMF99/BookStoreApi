using BookStoreApi.Models;
using BookStoreApi.StaticClasses;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using ProjectTracker.SignalR.Helpers;
using ProjectTracker.SignalR.Interfaces;
using ProjectTracker.SignalR.Models;
using ProjectTracker.SignalR.Request;
using System.Diagnostics;

namespace ProjectTracker.SignalR.Services.Hubs
{
    /// <summary>
    /// Representa un hub de SignalR para gestionar actualizaciones en tiempo real.
    /// </summary>
    public class RealTimeUpdateHub : Hub
    {

        private int connectionCounter = 0; // contador de conexiones.
        private readonly IRealTimeUpdateInfoService _realTimeUpdateInfoService;


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RealTimeUpdateHub"/> con un servicio especificado.
        /// </summary>
        /// <param name="realTimeUpdateInfoService">El  servicio para gestionar actualizaciones en tiempo real.</param>
        public RealTimeUpdateHub(IRealTimeUpdateInfoService realTimeUpdateInfoService)
        {
            _realTimeUpdateInfoService = realTimeUpdateInfoService;
        }

        /// <summary>
        /// Se invoca cuando un cliente se conecta al hub.
        /// Incrementa el contador de conexiones.
        /// </summary>
        /// <returns>Una tarea que representa el proceso asincrónico.</returns>
        public override async Task<Task> OnConnectedAsync()
        {
            connectionCounter++;
            string connectionId = Context.ConnectionId;
            string codeGenerated = Context?.GetHttpContext().Request?.Query["codeGenerate"].ToString();
            //string userId = Context.GetHttpContext().Request?.Query["userId"].ToString();
            var p = DBCollections.projectCollection.Find(c => c.ProjectName == "BookStoreAPI");

            if (p == null)
            {
                var projectModel = new ProjectModel
                {
                    ProjectName = "BookStoreAPI",
                    Description = "Una Api de manejo de libros y clientes"
                };

                await DBCollections.projectCollection.InsertOneAsync(projectModel);
            }

            var codeModel = new CodeModel
            {
                TVCode = codeGenerated,
                ConnectionId = connectionId,
            };

            // Insertar el documento en la base de datos
            await DBCollections.codeCollection.InsertOneAsync(codeModel);


            //Debug.WriteLine("SignalR server connected");
            Debug.WriteLine("API Conectada!");
            //Debug.WriteLine($"Usuarios Conectados: {connectionCounter} {codeGenerated}");

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Se invoca cuando un cliente se desconecta del hub.
        /// Disminuye el contador de conexiones.
        /// </summary>
        /// <param name="exception">Excepción en la conexión, si la hay.</param>
        /// <returns>Una tarea que representa el proceso asincrónico.</returns>
        public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;
            connectionCounter--;

            // Define un filtro para encontrar el documento por su ConnectionId
            var filter = Builders<CodeModel>.Filter.Eq(c => c.ConnectionId, connectionId);

            // Elimina el documento que coincide con el filtro
            await DBCollections.codeCollection.DeleteOneAsync(filter);

            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Inicializa la conexión y gestiona su estado en la base de datos.
        /// </summary>
        /// <param name="statusConnectionSignalRRequest">Estado de la conexión SignalR a gestionar.</param>
        /// <returns>Una tarea que representa el proceso asincrónico.</returns>
        [HubMethodName("Init")]
        public Task Init(StatusConnectionSignalR statusConnectionSignalRRequest)
        {
            SignalRHelper.CheckAndMageConnection(statusConnectionSignalRRequest, Context.ConnectionId);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Recibe un mensaje del cliente y envía las actualizaciones correspondientes.
        /// </summary>
        /// <param name="messageItem">El mensaje recibido del cliente.</param>
        [HubMethodName("ReceiveUserFromClient")]
        public void ReceiveUserFromClient(MessageUpdateItemRequest messageItem)
        {
            var connectionId = Context.ConnectionId;

            _realTimeUpdateInfoService.RecibeMessageUpdateInfoFromClienteAndSendInfoToUpdate(messageItem, connectionId);
        }

        [HubMethodName("NotifyClient")]
        public void NotifyClient(MessageUpdateItemRequest messageItem)
        {
            _realTimeUpdateInfoService.NotifyClient(messageItem);
        }

        public async Task SendMessageToUser(string connectionId, string mensaje)
        {
            await Clients.Client(connectionId).SendAsync("RecibirMensaje", mensaje);
        }

        public async Task SendMessageToAllClients(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
