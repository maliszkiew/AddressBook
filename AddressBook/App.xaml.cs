using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AddressBook
{
    public partial class App : Application
    {
        public EventAggregator EventAggregator { get;} = new EventAggregator();

    }
}
