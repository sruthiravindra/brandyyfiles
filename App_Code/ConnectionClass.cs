using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using IchooseIT.DAL;
public class ConnectionClass : Common
{
    public SqlConnection conn = null;
    public DataTable DataTab = new DataTable();
    public DataSet DataSet = new DataSet();
    public int Result = 0;

    public void CheckConnection()
    {
        conn = SessionState._IchooseITConnection;
    }

    public void GetDataSet(SqlCommand cmd)
    {
        DataSet = new DataSet();
        try
        {
            CheckConnection();
            IsSuccess = false;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            conn.Open();
            adp.Fill(DataSet);
            conn.Close();
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            conn.Close();
            this.Message = ex.ToString();
        }
    }

    public void GetDataTab(SqlCommand cmd)
    {
        DataTab = new DataTable();
        try
        {
            CheckConnection();
            IsSuccess = false;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            conn.Open();
            adp.Fill(DataTab);
            conn.Close();
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            if (conn != null)
            {
                conn.Close();
            }
            this.Message = ex.ToString();
        }
    }

    public void ExecuteNonQuery(SqlCommand cmd)
    {
        try
        {
            IsSuccess = false;
            Result = 0;
            CheckConnection();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            Result = cmd.ExecuteNonQuery();
            conn.Close();
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            conn.Close();
            this.Message = ex.ToString();
        }
    }

    public void ExecuteNonQuery(string Query)
    {
        try
        {
            IsSuccess = false;
            Result = 0;
            CheckConnection();
            SqlCommand cmd = new SqlCommand(Query);
            cmd.Connection = conn;
            conn.Open();
            Result = cmd.ExecuteNonQuery();
            conn.Close();
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            conn.Close();
            this.Message = ex.ToString();
        }
    }

    public void ReleaseConnection()
    {
        if (conn != null)
        {
            conn.Close();
            //conn.Dispose();
            //conn = null;
        }
    }
}


