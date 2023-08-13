using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;


namespace Client_User
{
    public class ChangePassword
    {
        public int j;


        public void DisplayChangePassword()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            string StudentNo = "123456789";
            string OldPassword;
            string NewPassword;
            string ReNewPassword;
            // try
            // {
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==========================================================");
                Console.WriteLine("----------------------Change Password---------------------");
                Console.WriteLine("==========================================================");
                OldPassword = ReadPassword("Enter your old password: ");
                NewPassword = ReadPassword("Enter your new password: ");
                if (NewPassword.Length > 20)
                {
                    Console.WriteLine("Password should be maximum 20 characters.");
                    Console.ReadKey();
                    continue;
                }
                ReNewPassword = ReadPassword("Re-enter your new password: ");

                if (AuthenticatePassword(StudentNo, OldPassword) && NewPassword.Equals(ReNewPassword))
                {

                    changePassword(StudentNo, NewPassword);
                    Console.ReadKey();
                    break;

                }
                else
                {
                    Console.WriteLine("password does not exist!\nPlease check the information again.");
                    Console.ReadKey();
                }

            } while (true);
            //}
            // catch
            // {
            //     Console.WriteLine("Sorry!\nSomething went wrong, please try again in a few minutes!");
            // }

        }

        /// <summary>
        /// hàm cho phép nhập và ẩn mật khẩu nhập vào từ bàn phím bằng các dấu *.
        /// </summary>
        /// <param name="label">tên tiêu đề</param>
        /// <returns></returns>
        static string ReadPassword(string label)
        {
            Console.Write(label);
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
                    else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Delete && key.Key != ConsoleKey.Escape)
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

        /// <summary>
        /// hàm kiểm tra xem mật khẩu hiện tại có đúng với tài khoản này không
        /// </summary>
        /// <param name="studentNo"> student no của học sinh</param>
        /// <param name="oldPassword"> password hiện tại của học sinh</param>
        /// <returns></returns>
        static bool AuthenticatePassword(string studentNo, string oldPassword)
        {
            MySqlConnection connection = Connection.GetConnection();
            string StoredProcedure = $"SELECT COUNT(*) FROM students WHERE student_no = @StudentNo AND password = @Password";
            using (var command = new MySqlCommand(StoredProcedure, connection))
            {

                command.Parameters.AddWithValue("@StudentNo", studentNo);
                command.Parameters.AddWithValue("@Password", oldPassword);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;

            }
        }

        /// <summary>
        /// hàm lấy thông tin student id trong mysql bằng việc truyền vào student no của học sinh
        /// </summary>
        /// <param name="student_no">student no của học sinh</param>
        /// <returns></returns>
        public static int GetStudentId(string student_no)
        {
            int StudentId = -1;

            // Kết nối đến cơ sở dữ liệu
            MySqlConnection connection = Connection.GetConnection();
            // Tạo command để thực thi thủ tục lưu trữ
            string StoredProcedure = "sp_GetStudentId"; // ghi tên procedure ở đây
            using (MySqlCommand command = new MySqlCommand(StoredProcedure, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentNo", student_no); // ghi tên student no ở đây (cái student no mà đặt tên trong StoredProcedure)
                command.Parameters.Add("@StudentId", MySqlDbType.Int32).Direction = System.Data.ParameterDirection.Output;  // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                command.ExecuteNonQuery();

                if (command.Parameters["@StudentId"].Value != DBNull.Value) // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                {
                    StudentId = Convert.ToInt32(command.Parameters["@StudentId"].Value); // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                }

                return StudentId;
            }
        }

        /// <summary>
        /// hàm đổi mật khẩu của user
        /// </summary>
        /// <param name="studentNo"> student no của học sinh</param>
        /// <param name="newPaswword"> mật khẩu mới của học sinh đã nhập trên hàm display</param>
        static void changePassword(string studentNo, string newPaswword)
        {
            // try
            // {
            int studentId = GetStudentId(studentNo);
            MySqlConnection connection = Connection.GetConnection();
            string StoredProcedure = "update students set password = @NewPassword where student_id = @StudentId;";
            using (var command = new MySqlCommand(StoredProcedure, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters["@StudentId"].Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@NewPassword", newPaswword);
                command.Parameters["@NewPassword"].Direction = ParameterDirection.Input;

                Int32 recordsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Change password successfully!");
            }
            // }
            // catch
            // {
            //     Console.WriteLine("Sorry!\nSomething went wrong, please try again in a few minutes!");
            // }
        }
    }
}