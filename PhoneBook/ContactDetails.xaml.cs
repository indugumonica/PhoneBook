using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhoneBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactDetails : Page
    {
        public ContactDetails()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var name = (string)e.Parameter;
            var contact = await Contacts.FindContactByName(name);

            var contactsList = new List<Contacts>();
            contactsList.Add(contact);

            DataContext = contactsList;
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Delete_Button(object sender, RoutedEventArgs e)
        {
            var contacts =  (List<Contacts>) DataContext;
            await Contacts.DeleteContactByName(contacts[0].Name);

            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
