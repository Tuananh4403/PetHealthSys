namespace PetCareSystem.Data.Entites.Chat
{
    public class Message
    {
        public int Id { get; set; }
    
    // Navigation properties
    public int SenderId { get; set; }
    public User Sender { get; set; }
    
    public int RecipientId { get; set; }
    public User Recipient { get; set; }
    
    public string Text { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

