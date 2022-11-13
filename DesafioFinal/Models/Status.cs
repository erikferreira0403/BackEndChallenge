namespace DesafioFinal.Models
{
    public class Status
    {
        public Status(int id, string statusEnum)
        {
            Id = id;
            StatusEnum = statusEnum;
        }

        public int Id { get; set; }
        public string StatusEnum { get; set; }
    }
}
