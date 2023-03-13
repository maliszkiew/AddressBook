using System.Collections.Generic;
using System.IO;

namespace AddressBook.Models
{
    public class Constants
    {
        public const string CONTACTS_JSON = @"AppData\contacts.json";
        public static string IMAGE_PLACEHOLDER = Path.GetFullPath("Media/person-placeholder.jpg");
        public static string IMAGE_PATH = Path.GetFullPath("Media/");
        public static List<string> Groups = new List<string> {"None", "Friends", "Family", "Work"};
    }
}
