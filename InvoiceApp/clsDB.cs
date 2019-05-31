using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
public class clsDB
{
    SqlConnection conn;
    SqlTransaction tran;
    SqlCommand cmd;

    #region DataBase Connection Property

    public string ConnString { get; set; }
  
    #endregion

    void ReadSetting()
    {
        try
        {
            ConnString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public clsDB()
    {
        try
        {
            conn = new SqlConnection();
            ReadSetting();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Connect To DataBase
    /// </summary>
    public void Connect()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = ConnString;
                conn.Open();
                cmd = new SqlCommand();
                cmd = conn.CreateCommand();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// DisConnect From DataBase
    /// </summary>
    public void DisConnect()
    {
        try
        {
            if (conn.State == ConnectionState.Open && conn != null)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Return Data If transaction start then within transaction if tran is not start then without transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query</param>
    /// <returns>DataSet</returns>
    public DataSet GetDataSet(string sQry) //can be called in transaction
    {
        try
        {
            cmd.CommandText = sQry;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Dispose();
            da.Dispose();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Return Data If transaction start then within transaction if tran is not start then without transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query</param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(string sQry) //this method can be called in transaction also
    {
        try
        {
            cmd.CommandText = sQry;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Dispose();
            da.Dispose();
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Insert,Update,Delete Data Wihin or Without Transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query For Inserion,Updation,Deletion</param>
    /// <returns>Affected No Of Rows</returns>
    public int ExecuteNonQuery(string sQry)
    {
        try
        {
            int iCount = 0;
            cmd.CommandText = sQry;
            iCount = cmd.ExecuteNonQuery();
            return iCount;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public object ExecuteScalar(string sQry)
    {
        try
        {
            cmd.CommandText = sQry;
            var vObj = cmd.ExecuteScalar();
            return vObj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Transaction

    /// <summary>
    /// Begin Transaction
    /// </summary>
    public void BeginTran()
    {
        try
        {
            Connect();
            tran = conn.BeginTransaction();
            cmd.Transaction = tran;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Commit the transaction 
    /// </summary>
    public void CommitTran()
    {
        try
        {
            if (tran != null)
            {
                tran.Commit();
                tran.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// RollBackTran the transaction
    /// </summary>
    public void RollBackTran()
    {
        try
        {
            if (tran != null)
            {
                tran.Rollback();
                tran.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}

