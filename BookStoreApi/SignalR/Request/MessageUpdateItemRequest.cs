namespace ProjectTracker.SignalR.Request
{
    public record MessageUpdateItemRequest(
        string? ProjectId,
        string? ProjectName,
        string? Message,
        string? Code 
    );
}
