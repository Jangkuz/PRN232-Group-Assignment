namespace AirWaterStore.API.Dtos;

public class MessageDto
{
    public int MessageId { get; set; }

    public int ChatRoomId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? SentAt { get; set; } = DateTime.UtcNow;
}
