using System;
using System.Collections.Generic;
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

        private void Button_Add(object sender, RoutedEventArgs e)
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
         //   contact.DateOfBirth = DateTime.Parse(txtBirthDate.Text);  

            var response = client.PostAsJsonAsync("api/Contact", contact).Result;

            if (response.IsSuccessStatusCode)
            {
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
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
        }

    }
}
