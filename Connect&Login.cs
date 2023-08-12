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


    public class LoginUI
    {
        public int i = -1;
        public string pass = "";
        public string? ID = "";

        //123456789
        //12345
        public void Login()
        {
            string ID;
            string password;
            // đoạn này sẽ xóa sau khi xong các hàm bên trong 
            // Console.WriteLine("\t\t\t\tLogin\t\t\t\t");
            // Console.Write("Enter your ID :");
            // ID = Console.ReadLine() ?? "".TrimEnd(' ');
            // Console.Write("Enter your password :");
            // string password = ReadPassword().TrimEnd(' ');

            //Đăng nhập + xác minh tài khoản để sau dùng h xài cái trên cho tiện
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==========================================================");
                Console.WriteLine("---------------------------Login--------------------------");
                Console.WriteLine("==========================================================");
                Console.Write("Enter your ID :");
                ID = Console.ReadLine() ?? "".TrimEnd(' ');
                Console.Write("Enter your password :");
                password = ReadPassword().TrimEnd(' ');

                if(AuthenticateUser(ID,password))
                {
                    Console.WriteLine("Login Succsesfull!");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Error ID or Password!");
                    Console.ReadKey();
                }
            }while(true);
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Nếu phím không phải là Enter, xử lý xóa ký tự
                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        // Xóa ký tự cuối cùng trong mật khẩu và di chuyển con trỏ về trước
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Delete)
                    {
                        // Thêm ký tự vào mật khẩu và hiển thị dấu *
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Xuống dòng sau khi người dùng nhấn Enter
            return password;
        }

        // hàm xác thực thông tin người dùng dựa vào csdl
        static bool AuthenticateUser(string StudentNo, string password)
        {

            MySqlConnection connection = Connection.GetConnection();
            string StoredProcedure = $"SELECT COUNT(*) FROM students WHERE student_no = @StudentNo AND password = @Password";
            using (var command = new MySqlCommand(StoredProcedure, connection))
            {
                command.Parameters.AddWithValue("@StudentNo", StudentNo);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }

            // count lớn hơn 0 trả về true = 0 trả về false(nếu sinh viên tồn tại nó sẽ trả về student id của sinh viên đó mặc định sẽ lớn hơn 0 nếu có tồn tại).
        }
    }

}

