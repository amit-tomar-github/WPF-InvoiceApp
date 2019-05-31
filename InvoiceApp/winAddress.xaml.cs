using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceApp
{
    /// <summary>
    /// Interaction logic for winAddress.xaml
    /// </summary>
    public partial class winAddress : Window
    {
        public winAddress()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtId.Focus();
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Enter id");
                    return;
                }
                if (string.IsNullOrEmpty(txtAdress1.Text))
                {
                    MessageBox.Show("Enter address1");
                    return;
                }

                if (new Address() { AddressId = txtId.Text.Trim(), Address1 = txtAdress1.Text.Trim(), Address2 = txtAdress2.Text.Trim(), Address3 = txtAdress3.Text.Trim(), Address4 = txtAdress4.Text.Trim() }.SaveAddress() >0)
                {
                    MessageBox.Show("Saved/Updated successfully!!");
                    txtId.Text = "";
                    txtId.Focus();
                    txtAdress1.Text = "";
                    txtAdress2.Text = "";
                    txtAdress3.Text = "";
                    txtAdress4.Text = "";
                }
                else
                    MessageBox.Show("Address could not be saved/updated !!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
