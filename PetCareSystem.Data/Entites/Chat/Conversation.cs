namespace PetCareSystem.Data.Entites
{
    public class Conversation
    {
        public int Id { get; set; }

        // Navigation properties
        public int Participant1Id { get; set; }
        public User Participant1 { get; set; }

        public int Participant2Id { get; set; }
        public User Participant2 { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}