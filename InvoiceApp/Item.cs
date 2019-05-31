using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp
{
   public class Item
    {
        public ObservableCollection<Item> ItemObject { get; set; }
        public Address AddressCls { get; set; }
        public Header Header { get; set; }
        public string ItemDesc { get; set; }
        public int ItemQty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string TinNo { get; set; }
        public string PanNo { get; set; }
        public string BillNo { get; set; }
        public string PoNo { get; set; }
        public string Vat { get; set; }
        public string Address { get; set; }
        public double Total { get; set; }
        public double VatTotal { get; set; }
        public double GrandTotal { get; set; }
        public string GrandTotalInWords { get; set; }
        public string SaveItem()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                string sMsg = "OK";
                sbQry.Append("Select Count(*) From Item Where ItemDesc = '" + ItemDesc + "'");
                int iCount = (int)oDB.ExecuteScalar(sbQry.ToString());
                sbQry.Length = 0;
                //If already exist then update otherwise insert
                if (iCount > 0)
                    sMsg = "Item already exist!!";
                else
                    sbQry.Append("Insert Into Item(ItemDesc)  Values('" + ItemDesc + "')");

                iCount = oDB.ExecuteNonQuery(sbQry.ToString());
                return sMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
        public int DeleteItem()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.Append("Delete From Item Where ItemDesc = '" + ItemDesc + "'");
                int iCount = oDB.ExecuteNonQuery(sbQry.ToString());
                return iCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
        public void SaveInvoice()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                oDB.BeginTran();
                foreach (Item ItemObj in this.ItemObject)
                {
                    sbQry.AppendLine("Insert Into Invoice(TinNo,PanNo,BillNo,InvoiceDate,InvoiceDateTime,Address,PoNo");
                    sbQry.AppendLine(",ItemDesc,Qty,Rate,Amount,Vat,GrandTotalWords)  Values('" + TinNo + "','" + PanNo + "'");
                    sbQry.AppendLine(",'" + BillNo + "','" + DateTime.Now.ToString("dd-MMM-yyyy") + "',Getdate(),'" + Address + "'");
                    sbQry.AppendLine(",'" + PoNo + "','" + ItemObj.ItemDesc + "'," + ItemObj.ItemQty + "," + ItemObj.Rate + "," + ItemObj.Amount + ",'" + Vat + "','" + GrandTotalInWords + "')");
                    oDB.ExecuteNonQuery(sbQry.ToString());
                    sbQry.Length = 0;
                }
                oDB.CommitTran();
            }
            catch (Exception ex)
            {
                oDB.RollBackTran();
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
        public DataTable GetItem()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.Append("Select ItemDesc From Item Order by ItemDesc");
                return oDB.GetDataTable(sbQry.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }

        public DataTable GetAddress()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.Append("Select Id,Address1,Address2,Address3,Address4 From mAddress Order by Id");
                return oDB.GetDataTable(sbQry.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
        public DataTable GetHeader()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.Append("Select Id,Header1,Header2,Header3,Header4 From mHeader Order by Id");
                return oDB.GetDataTable(sbQry.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
        public DataTable GetPanTin()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                sbQry.Append("Select Top(1) TinNo,PanNo From mPanNo");
                return oDB.GetDataTable(sbQry.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sbQry = null;
                oDB.DisConnect();
                oDB = null;
            }
        }
    }
}
