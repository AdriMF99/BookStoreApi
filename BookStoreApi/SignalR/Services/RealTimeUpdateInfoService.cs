using Microsoft.AspNetCore.SignalR;
using ProjectTracker.SignalR.Interfaces;
using ProjectTracker.SignalR.Request;
using ProjectTracker.SignalR.Services.Hubs;

namespace ProjectTracker.SignalR.Services
{
    /// <summary>
    /// Servicio para gestionar actualizaciones en tiempo real.
    /// </summary>
    public class RealTimeUpdateInfoService : IRealTimeUpdateInfoService
    {
        private readonly IHubContext<RealTimeUpdateHub> _hubContext;
        //private readonly IUserService _userService;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="hubContext">Contexto del hub de SignalR.</param>
        /// <param name="userService"></param>
        public RealTimeUpdateInfoService(IHubContext<RealTimeUpdateHub> hubContext/*, IUserService userService*/)
        {
            _hubContext = hubContext;
            //_userService = userService;
        }



        /// <summary>
        /// Recibe mensajes del cliente y envía actualizaciones.
        /// </summary>
        /// <param name="messageUpdateItem">El mensaje recibido del cliente.</param>
        /// <param name="connectionId">El ID de conexión del cliente.</param>
        public async void RecibeMessageUpdateInfoFromClienteAndSendInfoToUpdate(MessageUpdateItemRequest messageUpdateItem, string connectionId)
        {
            //if (messageUpdateItem != null && messageUpdateItem.Token != null)
            //{
            //    var response = await _userService.RenewToken(messageUpdateItem.Token);

            //    RespondToTheCustomerWithUpdatedInformationBySignalR(response, connectionId);
            //}
        }


        /// <summary>
        /// Envía un mensaje a todos los clientes conectados a SignalR en ese momento.
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns>El mensaje enviado.</returns>
        public async Task NotifyClient(MessageUpdateItemRequest itemRequest)
        {
            await _hubContext.Clients.All.SendAsync("NotifyClient", itemRequest);
        }

        /// <summary>
        /// Envía a los clientes el código generado en otra función.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>El código enviado.</returns>
        public async Task SendCode(MessageUpdateItemRequest code)
        {
            string verificationCode = await GenerateVerificationCode(code);
            await _hubContext.Clients.All.SendAsync("NotifyClient", verificationCode);
        }

        /// <summary>
        /// Genera un código de verificación de 9 de largo.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>El código generado.</returns>
        public async Task<string> GenerateVerificationCode(MessageUpdateItemRequest code)
        {
            int codeLength = 9;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";

            var random = new Random();

            string verificationCode = new string(Enumerable.Repeat(chars, codeLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return verificationCode;
        }

        /// <summary>
        /// Envía un mensaje a un cliente en específico.
        /// </summary>
        /// <param name="codeUpdateRequest"></param>
        /// <param name="connectionId"></param>
        public async void ResponseCode(MessageUpdateItemRequest codeUpdateRequest, string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("NotifyClient", codeUpdateRequest);
        }
    }
}
