using System.Collections.Generic;

namespace AddressBook.Models
{
    public class Funcs
    {
        public void sortData(List<ContactProfile> list)
        {
            list.Sort(new FirstNameComparer());
        }
    }
}
