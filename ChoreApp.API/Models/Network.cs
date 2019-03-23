namespace ChoreApp.API.Models
{
    public class Network
    {
        public int NetworkerId { get; set; }
        public int NetworkeeId { get; set; }
        public User Networker { get; set; }
        public User Networkee { get; set; }
    }
}