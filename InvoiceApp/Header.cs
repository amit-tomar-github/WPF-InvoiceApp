using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp
{
    public class Header
    {
        public string HeaderId { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public int SaveHeader()
        {
            StringBuilder sbQry = new StringBuilder();
            clsDB oDB = new clsDB();
            try
            {
                oDB.Connect();
                string sMsg = "OK";
                sbQry.Append("Select Count(*) From mHeader Where Id = '" + HeaderId + "'");
                int iCount = (int)oDB.ExecuteScalar(sbQry.ToString());
                sbQry.Length = 0;
                //If already exist then update otherwise insert
                if (iCount > 0)
                    sbQry.Append("Update mHeader Set Header1='" + Header1 + "',Header2='" + Header2 + "',Header3='" + Header3 + "',Header4='" + Header4 + "'  Where Id = '" + HeaderId + "'");
                else
                    sbQry.Append("Insert Into mHeader(Id,Header1,Header2,Header3,Header4)  Values('" + HeaderId + "','" + Header1 + "','" + Header2 + "','" + Header3 + "','" + Header4 + "')");

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
