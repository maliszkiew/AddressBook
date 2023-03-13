
namespace AddressBook.Models
{
    public class ContactProfile
    {
        public ContactProfile()
        {
            Id = -1;
            firstName = string.Empty;
            lastName = string.Empty;
            phoneNumber = string.Empty;
            email = string.Empty;
            address = string.Empty;
            group = string.Empty;
            imagePath = string.Empty;
        }
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string group { get; set; }
        public string imagePath { get; set; }
    }
}
