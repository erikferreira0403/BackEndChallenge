using System;

namespace DesafioFinal.Models
{
    public class Subscription
    {
        public Subscription()
        {
       
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public EventHistory EventHistory { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
