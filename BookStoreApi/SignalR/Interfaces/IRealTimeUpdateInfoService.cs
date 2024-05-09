using ProjectTracker.SignalR.Request;

namespace ProjectTracker.SignalR.Interfaces
{
    public interface IRealTimeUpdateInfoService
    {
        Task NotifyClient(MessageUpdateItemRequest itemRequest);

        void RecibeMessageUpdateInfoFromClienteAndSendInfoToUpdate(MessageUpdateItemRequest messageUpdateItem, string connectionId);

        public Task SendCode(MessageUpdateItemRequest code);


        public void ResponseCode(MessageUpdateItemRequest codeUpdateRequest, string connectionId);


        public Task<string> GenerateVerificationCode(MessageUpdateItemRequest code);

    }
}
