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
    public partial class winHeader : Window
    {
        public winHeader()
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
                if (string.IsNullOrEmpty(txtHeader1.Text))
                {
                    MessageBox.Show("Enter address1");
                    return;
                }

                if (new Header() { HeaderId = txtId.Text.Trim(), Header1 = txtHeader1.Text.Trim(), Header2 = txtHeader2.Text.Trim(), Header3 = txtHeader3.Text.Trim(), Header4 = txtHeader4.Text.Trim() }.SaveHeader() > 0)
                {
                    MessageBox.Show("Saved/Updated successfully!!");
                    txtId.Text = "";
                    txtId.Focus();
                    txtHeader1.Text = "";
                    txtHeader2.Text = "";
                    txtHeader3.Text = "";
                    txtHeader4.Text = "";
                }
                else
                    MessageBox.Show("Address could not be saved/updated !!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
