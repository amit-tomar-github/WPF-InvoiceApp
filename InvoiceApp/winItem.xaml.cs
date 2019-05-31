using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class winItem : Window
    {
        public winItem()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblMsg.Content = "";
            txtItem.Focus();
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblMsg.Content = "";
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    lblMsg.Content = "Enter item";
                    txtItem.Focus();
                    return;
                }

                //Item Item = new Item();
                //Item.ItemDesc = txtItem.Text.Trim();

                if (new Item() { ItemDesc = txtItem.Text.Trim() }.SaveItem() == "OK")
                {
                    lblMsg.Content = "Saved successfully!!";
                    txtItem.Text = "";
                    txtItem.Focus();
                }
                else
                    lblMsg.Content = "Item could not be saved or aleady exist!!";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
