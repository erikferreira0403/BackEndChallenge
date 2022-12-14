using System;

namespace DesafioFinal.Models
{
    public class User
    {
        public User()
        {

        }
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
