using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cashtransaction
/// </summary>
public class cashtransaction
{
    ConnectionClass ConnObj = new ConnectionClass();
	public cashtransaction()
	{
        SqlCommand cmd = new SqlCommand("sp_select_brands_master");
        cmd.Parameters.AddWithValue("@status", "1");
        ConnObj.GetDataSet(cmd);
        foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
        {
        SqlCommand cmd1 = new SqlCommand("sp_insert_brandcashTransactions");
        cmd1.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["id"]));
        ConnObj.ExecuteNonQuery(cmd1);
        }

	}
}