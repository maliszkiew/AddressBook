using System;
using System.Collections.Generic;

namespace AddressBook.Models
{
    class FirstNameComparer : IComparer<ContactProfile>
    {
        public int Compare(ContactProfile x, ContactProfile y)
        {
            return string.Compare(x.firstName, y.firstName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
