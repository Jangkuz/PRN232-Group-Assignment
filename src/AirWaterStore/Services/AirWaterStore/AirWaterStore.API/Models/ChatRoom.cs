﻿namespace AirWaterStore.API.Models;

public class ChatRoom
{
    public int ChatRoomId { get; set; }

    public int CustomerId { get; set; }

    public int? StaffId { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? Staff { get; set; }
}
