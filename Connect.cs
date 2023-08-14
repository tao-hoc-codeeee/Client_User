using System;
using MySql.Data.MySqlClient;


namespace Client_User
{
    public class Connection
    {
        public static MySqlConnection GetConnection()
        {
            MySqlConnection myconnection = new MySqlConnection
            {
                ConnectionString = @"server=localhost;user id=root;password=nguyenthequan305;port=3306;database=StudentManagement1;charset=utf8mb4;"
            };
            myconnection.Open();
            return myconnection;
        }
    }

    //@"server=localhost;user id=root;password=nhatdo25;port=3306;database=StudentManagement1;"
    //@"server=localhost;user id=root;password=nguyenthequan305;port=3306;database=StudentManagement1;"
}