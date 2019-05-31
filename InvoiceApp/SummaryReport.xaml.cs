using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class SummaryReport : Window
    {
        public SummaryReport()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbFilter.Items.Add("--Select--");
                cmbFilter.Items.Add("TinNo");
                cmbFilter.Items.Add("PanNo");
                cmbFilter.Items.Add("BillNo");
                cmbFilter.Items.Add("PoNo");
                cmbFilter.Items.Add("ItemDesc");
                cmbFilter.SelectedIndex = 0;

                FromDate.Text = DateTime.Now.ToString();
                ToDate.Text = DateTime.Now.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.AppendLine("Select * From Invoice Where InvoiceDate >= '"+Convert.ToDateTime(FromDate.Text).ToString("dd-MMM-yyyy")+ "' And InvoiceDate <= '" + Convert.ToDateTime(ToDate.Text).ToString("dd-MMM-yyyy") + "'");
                if (cmbFilter.SelectedIndex > 0)
                {
                    sbQry.AppendLine(" And " + cmbFilter.SelectedItem.ToString() + " Like '%" + txtFilterValue.Text.Trim() + "%'");
                }
                dg.ItemsSource = oDB.GetDataTable(sbQry.ToString()).DefaultView;
                btnItemCount.Content = dg.Items.Count;
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToString();
                ToDate.Text = DateTime.Now.ToString();
                cmbFilter.SelectedIndex = 0;
                txtFilterValue.Text = "";
                dg.ItemsSource = null;
                btnItemCount.Content = dg.Items.Count;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
