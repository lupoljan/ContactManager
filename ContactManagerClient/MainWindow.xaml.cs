using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace ContactManagerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Contact").Result;

            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<IEnumerable<Contact>>().Result;

                contactgrid.ItemsSource = contacts;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }

        private bool ValidateInput(string text)
        {
            if (text == String.Empty)
            {
                return false;
            }
            return true;
        }
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var contact = new Contact();

       //   contact.Id = Int32.Parse(txtId.Text); should be incremented
            contact.FirstName = txtFirst.Text;
            contact.LastName = txtLast.Text;
            contact.Email = txtEmail.Text;
            contact.PhoneNumber = txtPhone.Text;
            contact.DateOfBirth = DateTime.Parse(txtBirthDate.Text);

            if (ValidateInput(contact.FirstName) && ValidateInput(contact.LastName))
            {
                var response = client.PostAsJsonAsync("api/Contact", contact).Result;
                //  MessageBox.Show(response.ToString());
                MessageBox.Show("Contact Added");
                txtId.Text = "";
                txtFirst.Text = "";
                txtLast.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                txtBirthDate.Text = "";
                GetData();
            }
            else
            {
                MessageBox.Show("First Name and Last Name are required");
            }
       }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var contact = new Contact();

            contact.Id = Int32.Parse(txtId.Text);
            contact.FirstName = txtFirst.Text;
            contact.LastName = txtLast.Text;
            contact.Email = txtEmail.Text;
            contact.PhoneNumber = txtPhone.Text;
            if (txtBirthDate.Text != String.Empty)
            {
                contact.DateOfBirth = DateTime.Parse(txtBirthDate.Text);
            }

            if (ValidateInput(contact.FirstName) && ValidateInput(contact.LastName))
            {
                HttpResponseMessage response = client.PutAsJsonAsync("api/Contact/" + contact.Id, contact).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Contact Edited");
                    txtId.Text = "";
                    txtFirst.Text = "";
                    txtLast.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtBirthDate.Text = "";
                    GetData();
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }

            else
            {
                MessageBox.Show("First Name and Last Name are required");
            }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var id = txtSearch.Text.Trim();

            var url = "api/Contact/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<Contact>().Result;
                MessageBox.Show("Contact Found : " + contacts.FirstName + " " + contacts.LastName);
                txtSearch.Text = "";
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void Button_SearchName(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var firstName = txtSearchName.Text.Trim();
            var url = "api/Contact?Name=" + firstName;
            var response = client.GetStringAsync(url).Result;

            if (response != "[]")
            {
                List<Contact> contact = JsonConvert.DeserializeObject<List<Contact>>(response);
                //if there is only one contact found
                if (contact.Count == 1)
                {
                    MessageBox.Show("Contact Found : Id=" + contact.First().Id + " " + contact.First().FirstName + " " + contact.First().LastName);
                }
                //if there are more contacts found
                else if (contact.Count > 1)
                {
                    string showMessage = "Contacts Found:\n";
                    foreach (var item in contact)
                    {
                        showMessage += "Id=" + item.Id + " " + item.FirstName + " " + item.LastName + "\n";
                    }
                    MessageBox.Show(showMessage);
                }
                txtSearch.Text = "";
            }
            else
            {
                MessageBox.Show("Contact Not Found");
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:16700/");

            var id = txtDelete.Text.Trim();

            var url = "api/Contact/" + id;

            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Contact Deleted");
                txtDelete.Text = "";
                GetData();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void contactgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact rowSelected = (Contact)contactgrid.SelectedItem;

            if (rowSelected != null)
            {
                txtId.Text = rowSelected.Id.ToString();
                txtFirst.Text = rowSelected.FirstName;
                txtLast.Text = rowSelected.LastName;
                txtBirthDate.Text = rowSelected.DateOfBirth.ToShortDateString();
                txtEmail.Text = rowSelected.Email;
                txtPhone.Text = rowSelected.PhoneNumber;

                txtDelete.Text = rowSelected.Id.ToString();
            }

        }
    }
}
