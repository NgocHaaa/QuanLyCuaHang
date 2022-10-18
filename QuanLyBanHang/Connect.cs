using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    internal class Connect
    {
        SqlConnection conn;
        string str_conn;
        public Connect()
        {
            str_conn = @"SERVER = NGOCHA\SQLEXPRESS02; DATABASE = QuanLyBanHang; Integrated Security = SSPI";

        }
        public SqlConnection ConnectDB()
        {
            conn = new SqlConnection(str_conn);
            return conn;
        }
    }
}
