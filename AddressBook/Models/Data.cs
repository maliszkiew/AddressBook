using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace AddressBook.Models
{
    public class Data
    {
        public List<ContactProfile> importedProfiles { get; set; }
        public Data()
        {

            importedProfiles = ImportData();

        }
        public List<ContactProfile> ImportData(bool temp = false)
        {
            if (!temp) //If importing data is not necessary in this instance, it's skipped
            {
                try
                {
                    string json = File.ReadAllText(Constants.CONTACTS_JSON);
                    List<ContactProfile> returnObjects = JsonConvert.DeserializeObject<List<ContactProfile>>(json);
                    return returnObjects;
                }
                catch (FileNotFoundException)
                {
                    return new List<ContactProfile>();
                }
                catch (UnauthorizedAccessException)
                {
                    return new List<ContactProfile>();
                }
            }
            return new List<ContactProfile>();
        }
        public static void sortData(List<ContactProfile> list)
        {
            list.Sort(new FirstNameComparer());

        }
        private readonly EventAggregator _eventAggregator = new EventAggregator();
        public void ExportData()
        {
            string json = JsonConvert.SerializeObject(this.importedProfiles);
            File.WriteAllText(Constants.CONTACTS_JSON, json);
            _eventAggregator.Publish(new JsonChangedMessage());
        }
        public static void DeleteProfile(int _pID)
        {
            string json = File.ReadAllText(Constants.CONTACTS_JSON);
            List<ContactProfile> objects = JsonConvert.DeserializeObject<List<ContactProfile>>(json);
            objects.RemoveAll(p => p.Id == _pID);
            json = JsonConvert.SerializeObject(objects);
            File.WriteAllText(Constants.CONTACTS_JSON, json);
        }

        public static void GarbageCleaner()
        {
            string json = File.ReadAllText(Constants.CONTACTS_JSON);
            List<ContactProfile> objects = JsonConvert.DeserializeObject<List<ContactProfile>>(json);
            for (int i = 0; i <= objects.Count(); i++)
            {
                var List = objects.Where(p => p.Id == i).ToList();
                if (List.Count == 0)
                {
                    if (File.Exists(Constants.IMAGE_PATH + i + ".jpeg")) File.Delete(Constants.IMAGE_PATH + i + ".jpeg");
                }
            }
        }

    }
}
