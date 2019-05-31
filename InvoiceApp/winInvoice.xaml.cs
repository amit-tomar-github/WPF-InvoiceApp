using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoiceApp
{
    public partial class winInvoice : Window
    {
        Item Item = new Item();
        public winInvoice()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                gbPanTin.Visibility = Visibility.Hidden;
                txtBillNo.Focus();
                Item.ItemObject = new ObservableCollection<Item>();
                dg.ItemsSource = Item.ItemObject;
                BindItem();
                GetPanTin();
                GetAddress();
                GetHeader();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (Item.ItemObject.Any(x => x.ItemDesc == cmbItem.SelectedItem.ToString()))
                    {
                        MessageBox.Show(cmbItem.SelectedItem.ToString() + " item already added if you want to change the qty then first select item then remove and again add");
                        return;
                    }
                    Item.ItemObject.Add
                    (
                        new Item
                        {
                            ItemDesc = cmbItem.SelectedItem.ToString(),
                            ItemQty = Convert.ToInt32(txtItemQty.Text),
                            Rate = Convert.ToDouble(txtItemRate.Text),
                            Amount = Convert.ToInt32(txtItemQty.Text) * Convert.ToDouble(txtItemRate.Text)
                        }
                      );
                    btnItemCount.Content = Item.ItemObject.Count.ToString();
                    CalculateAmount();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void CalculateAmount()
        {
            try
            {
                txtTotal.Text = Math.Round(Item.ItemObject.Sum(x => x.Amount), 2).ToString();
                txtVatTotal.Text = Math.Round(((Convert.ToDouble(txtTotal.Text) * Convert.ToDouble(txtVat.Text)) / 100), 2).ToString();
                txtGrandTotal.Text = Math.Round((Convert.ToDouble(txtTotal.Text) + Convert.ToDouble(txtVatTotal.Text)), 2).ToString();
                if (!string.IsNullOrEmpty(txtGrandTotal.Text))
                {
                    double dGrandTotal = Convert.ToDouble(txtGrandTotal.Text);
                    string sValue = "";
                    long rupee = long.Parse(dGrandTotal.ToString().Split('.')[0]);
                    sValue = ConvertNumbertoWords(rupee) + " Rupees";
                    if (dGrandTotal.ToString().Split('.').Length == 2)
                    {
                        long paisa = long.Parse(dGrandTotal.ToString().Split('.')[1]);
                        sValue += " And " + ConvertNumbertoWords(paisa) + " Paise";
                    }
                    txtWords.Text = sValue;
                }
                else
                    txtWords.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg.SelectedItem != null)
                {
                    if (MessageBox.Show("Do you really want to remove item " + (dg.SelectedItem as Item).ItemDesc, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Item.ItemObject.Remove(dg.SelectedItem as Item);
                        btnItemCount.Content = Item.ItemObject.Count.ToString();
                        CalculateAmount();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (txtTotal.Text == "" || txtTotal.Text=="0") { MessageBox.Show("Total not found"); txtTotal.Focus(); return ; }
                    if (txtVatTotal.Text == "" || txtVatTotal.Text == "0") { MessageBox.Show("Vat total not found"); txtVatTotal.Focus(); return; }
                    if (txtGrandTotal.Text == "" || txtGrandTotal.Text == "0") { MessageBox.Show("Grand total not found"); txtGrandTotal.Focus(); return; }
                    if (txtWords.Text == "") { MessageBox.Show("Please enter grand total in words"); txtWords.Focus(); return; }

                    Item.TinNo = txtTinNo.Text.Trim();
                    Item.PoNo = txtPoNo.Text.Trim();
                    Item.BillNo = txtBillNo.Text.Trim();
                    Item.PanNo = txtPanNo.Text.Trim();
                    Item.Vat = txtVat.Text.Trim();

                    Address address = (cmbAddress.SelectedItem as ComboBoxItem).Tag as Address;
                    Item.Address = address.Address1 + " " + address.Address2 + " " + address.Address3 + " " + address.Address4;

                    Header header = (cmbHeader.SelectedItem as ComboBoxItem).Tag as Header;

                    Item.Total = Math.Round(Convert.ToDouble(txtTotal.Text.Trim()), 2);
                    Item.VatTotal = Math.Round(Convert.ToDouble(txtVatTotal.Text.Trim()), 2);
                    Item.GrandTotal = Math.Round(Convert.ToDouble(txtGrandTotal.Text.Trim()), 2);
                    Item.GrandTotalInWords = txtWords.Text.Trim();

                    Item.SaveInvoice();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SNo", typeof(int));
                    dt.Columns.Add("Item", typeof(string));
                    dt.Columns.Add("Qty", typeof(int));
                    dt.Columns.Add("Rate", typeof(double));
                    dt.Columns.Add("Amount", typeof(double));
                    dt.Columns.Add("VatPer", typeof(string));
                    dt.Columns.Add("TinNo", typeof(string));
                    dt.Columns.Add("PanNo", typeof(string));
                    dt.Columns.Add("BillNo", typeof(string));
                    dt.Columns.Add("Address", typeof(string));
                    dt.Columns.Add("PoNo", typeof(string));
                    dt.Columns.Add("Total", typeof(string));
                    dt.Columns.Add("VatTotal", typeof(string));
                    dt.Columns.Add("GrandTotal", typeof(string));
                    dt.Columns.Add("TotalInWords", typeof(string));
                    dt.Columns.Add("Address1", typeof(string));
                    dt.Columns.Add("Address2", typeof(string));
                    dt.Columns.Add("Address3", typeof(string));
                    dt.Columns.Add("Address4", typeof(string));
                    dt.Columns.Add("Header1", typeof(string));
                    dt.Columns.Add("Header2", typeof(string));
                    dt.Columns.Add("Header3", typeof(string));
                    dt.Columns.Add("Header4", typeof(string));
                    int i = 1;
                    foreach (var vItem in Item.ItemObject)
                    {
                        dt.Rows.Add(i, vItem.ItemDesc, vItem.ItemQty, vItem.Rate, vItem.Amount, txtVat.Text + "%",Item.TinNo,Item.PanNo,Item.BillNo,Item.Address,Item.PoNo,Item.Total,Item.VatTotal,Item.GrandTotal,Item.GrandTotalInWords,address.Address1, address.Address2, address.Address3, address.Address4,header.Header1, header.Header2, header.Header3, header.Header4);
                        i++;
                    }
                    //add few extra blank lines
                    for (int j = i; j <= 10; j++)
                    {
                        dt.Rows.Add(null, null, null, null, null, txtVat.Text + "%", Item.TinNo, Item.PanNo, Item.BillNo, Item.Address, Item.PoNo, Item.Total, Item.VatTotal, Item.GrandTotal, Item.GrandTotalInWords, address.Address1, address.Address2, address.Address3, address.Address4, header.Header1, header.Header2, header.Header3, header.Header4);
                    }
                    winReportViewer2 oWin = new winReportViewer2(dt, Item);
                    oWin.ShowDialog();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BindItem()
        {
            try
            {
                //Bind Item
                cmbItem.Items.Clear();
                cmbItem.Items.Add("--Selct Item--");
                DataTable dt = Item.GetItem();
                foreach (DataRow row in dt.Rows)
                    cmbItem.Items.Add(row["ItemDesc"].ToString());

                dt = null;
                cmbItem.SelectedIndex = 0;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GetPanTin()
        {
            try
            {
                txtPanNo.Text = "";
                txtTinNo.Text = "";
                DataTable dt = Item.GetPanTin();
                if (dt.Rows.Count > 0)
                {
                    txtPanNo.Text = dt.Rows[0]["PanNo"].ToString();
                    txtTinNo.Text = dt.Rows[0]["TinNo"].ToString();
                }
                else
                {
                    MessageBox.Show("Pan no/Tin no not found,please enter and restart app again");
                }
                dt = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GetAddress()
        {
            try
            {
                //Bind Address
                cmbAddress.Items.Clear();
                cmbAddress.Items.Add("--Select Address--");
                DataTable dt = Item.GetAddress();
                foreach (DataRow row in dt.Rows)
                {
                    ComboBoxItem citem = new ComboBoxItem();
                    citem.Content = row["Id"].ToString();
                    citem.Tag = new Address() { AddressId = row["Id"].ToString(), Address1 = row["Address1"].ToString(), Address2 = row["Address2"].ToString(), Address3 = row["Address3"].ToString(), Address4 = row["Address4"].ToString() };
                    cmbAddress.Items.Add(citem);
                }
                dt = null;
                cmbAddress.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GetHeader()
        {
            try
            {
                //Bind Header
                cmbHeader.Items.Clear();
                DataTable dt = Item.GetHeader();
                foreach (DataRow row in dt.Rows)
                {
                    ComboBoxItem citem = new ComboBoxItem();
                    citem.Content = row["Id"].ToString();
                    citem.Tag = new Header() { HeaderId = row["Id"].ToString(), Header1 = row["Header1"].ToString(), Header2 = row["Header2"].ToString(), Header3 = row["Header3"].ToString(), Header4 = row["Header4"].ToString() };
                    cmbHeader.Items.Add(citem);
                }

                dt = null;
                cmbHeader.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtVat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void txtItemRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void txtItemQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtBillNo.Text = "";
                txtPoNo.Text = "";
                txtVat.Text = "";
                cmbAddress.SelectedIndex = 0;
                cmbHeader.SelectedIndex = 0;
                cmbItem.SelectedIndex = 0;
                txtItemRate.Text = "";
                txtItemQty.Text = "";
                txtTotal.Text = "";
                txtGrandTotal.Text = "";
                txtVatTotal.Text = "";
                txtWords.Text = "";
                Item.ItemObject.Clear();
                btnItemCount.Content = Item.ItemObject.Count;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool IsValid()
        {
            try
            {
                if (txtTinNo.Text == "") { MessageBox.Show("Enter tin no"); txtTinNo.Focus(); return false; }
                if (txtPanNo.Text == "") { MessageBox.Show("Enter pan no"); txtPanNo.Focus(); return false; }
                if (txtBillNo.Text == "") { MessageBox.Show("Enter bill no"); txtBillNo.Focus(); return false; }
                if (txtPoNo.Text == "") { MessageBox.Show("Enter po no"); txtPoNo.Focus(); return false; }
                if (txtVat.Text == "") { MessageBox.Show("Enter tVat"); txtVat.Focus(); return false; }
                if (cmbAddress.SelectedIndex<=0) { MessageBox.Show("select address"); cmbAddress.Focus(); return false; }
                if (cmbHeader.SelectedIndex < 0) { MessageBox.Show("select header"); cmbHeader.Focus(); return false; }
                if (cmbItem.SelectedIndex <=0) { MessageBox.Show("select item"); cmbItem.Focus(); return false; }
                if (txtItemRate.Text == "") { MessageBox.Show("Rate not found"); txtItemRate.Focus(); return false; }
                if (txtItemQty.Text == "") { MessageBox.Show("Enter qty"); txtItemQty.Focus(); return false; }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        private string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "Zero";
            if (number < 0) return ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " Lakh ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "") words += "";
                var unitsMap = new[]
                {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };
                var tensMap = new[]
                {
            "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        private void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winItem oWin = new winItem();
                oWin.ShowDialog();
                BindItem();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnReportDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               SummaryReport oWin = new SummaryReport();
                oWin.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnNewAddress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winAddress oWin = new winAddress();
                oWin.ShowDialog();
                GetAddress();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnNewHeader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winHeader oWin = new winHeader();
                oWin.ShowDialog();
                GetHeader();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
