using System;

namespace DesafioFinal.Models
{
    public class Subscription
    {
        public Subscription(int id, int userId, int statusId)
        {
            Id = id;
            UserId = userId;
            StatusId = statusId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public EventHistory EventHistory { get; set; }
        public User User { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
