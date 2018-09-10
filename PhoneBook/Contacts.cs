using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    // This is blueprint for a contact
    public class Contacts
    {

        private const string TEXT_FILE_NAME = "ContactTextFile.txt";
        //properties defined for each contact

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public async static Task<ICollection<Contacts>> GetContacts()
        {
            var contacts = new List<Contacts>();
            var FileContent = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);
            var lines = FileContent.Split(new char[] { '\r', '\n' });
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var lineparts = line.Split(',');
                var contact = new Contacts
                {
                    Name = lineparts[0],
                    PhoneNumber = lineparts[1]
                };
                contacts.Add(contact);
            }

            return contacts;
        }

        public static async Task WriteContact(Contacts contact)
        {
            var contactData = $"{contact.Name},{contact.PhoneNumber}";
            var allContacts = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);
            allContacts = $"{allContacts}\r\n{contactData}";
            await FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, allContacts);
        }

        public static async Task<Contacts> FindContactByName(string name)
        {
            var contactList = await GetContacts();
            foreach (var contact in contactList)
            {
               if (name == contact.Name)
                {
                    return contact;
                }
            }
            return null;
        }

        public static async Task DeleteContactByName(string name)
        {
            var contactList = await GetContacts();
            string newContacts = string.Empty;

            foreach (var contact in contactList)
            {
                if (name != contact.Name)
                {
                    var contactData = $"{contact.Name},{contact.PhoneNumber}";
                    newContacts = $"{newContacts}\n{contactData}";
                }
            }

            await FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, newContacts);
        }

    }
}
