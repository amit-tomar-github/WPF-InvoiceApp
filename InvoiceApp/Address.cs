using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp
{
   public class Address
    {
        public string AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public int SaveAddress()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                string sMsg = "OK";
                sbQry.Append("Select Count(*) From mAddress Where Id = '" + AddressId + "'");
                int iCount = (int)oDB.ExecuteScalar(sbQry.ToString());
                sbQry.Length = 0;
                //If already exist then update otherwise insert
                if (iCount > 0)
                    sbQry.Append("Update mAddress Set Address1='" + Address1 + "',Address2='" + Address2 + "',Address3='" + Address3 + "',Address4='" + Address4 + "'  Where Id = '" + AddressId + "'");
                else
                    sbQry.Append("Insert Into mAddress(Id,Address1,Address2,Address3,Address4)  Values('" + AddressId + "','" + Address1 + "','" + Address2 + "','" + Address3 + "','" + Address4 + "')");

                iCount = oDB.ExecuteNonQuery(sbQry.ToString());
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
    }
}
