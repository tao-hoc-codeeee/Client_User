using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using ConsoleTableExt;

namespace Client_User
{
    public class Displayinfo
    {
        public int j;

        public void Information()
        {
            string StudentNo = "123456789";
            Console.Clear();
            Console.WriteLine();
            int studentId = GetStudentId(StudentNo);
            Show_Information(studentId);
            Console.ReadKey();
        }

        /// <summary>
        /// hàm hiển thị thông tin cá nhân của student
        /// </summary>
        /// <param name="student_id"> id tương ứng của student trong mysql sẽ đc lấy ra bàng student no</param>
        public void Show_Information(int student_id)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            MySqlConnection connection = Connection.GetConnection();

            string StoredProcedure = "sp_UserDisplayBystudentNo";
            MySqlCommand command = new MySqlCommand(StoredProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StudentId", student_id);// student id tương ứng trong storedProcedure

            MySqlDataReader reader = command.ExecuteReader();

            var table = new List<List<string>>();

            //thêm tiêu đề bảng
            // var header = new List<string> { "Student No", "Name", "Class", "Brith Date", "Gender" };
            var header = new List<string> { "Student No", "Name", "Brith Date", "Gender" };
            table.Add(header);

            // Tính toán chiều dài tối đa của mỗi cột
            int[] columnWidths = new int[header.Count];
            for (int i = 0; i < header.Count; i++)
            {
                columnWidths[i] = header[i].Length;
            }

            while (reader.Read())
            {
                string StudentNo = reader.GetString("student_no");// Student no tương úng trong StoredProcedure
                string Name = reader.GetString("student_name");  // Tương đối với name của bạn
                //string Class = reader.GetString("");
                DateTime BirthDate = reader.GetDateTime("birth_date");
                string Gender = reader.GetString("gender");

                // Cập nhật chiều dài tối đa của mỗi cột dựa trên dữ liệu mới
                columnWidths[0] = Math.Max(columnWidths[0], StudentNo.Length);
                columnWidths[1] = Math.Max(columnWidths[1], Name.Length);
                columnWidths[2] = Math.Max(columnWidths[2], BirthDate.ToString().Length);
                columnWidths[3] = Math.Max(columnWidths[3], Gender.Length);


                var row = new List<string>
                {
                    StudentNo,
                    Name,
                    //Class,
                    BirthDate.ToString("dd/MM/yyyy"),
                    Gender
                };

                table.Add(row);
            }

            reader.Close();
            connection.Close();

            // Tính toán chiều dài của các dòng ngang
            int totalWidth = columnWidths.Sum() + (columnWidths.Length * 3) + 1; // Tổng số ký tự trong một dòng

            string horizontalLine = new string('=', totalWidth);
            string separatorLine = new string('-', totalWidth);

            // Tạo chuỗi dòng ngang cho phần thông tin
            string infoLine = "Infomation";
            int infoLinePadding = (totalWidth - infoLine.Length) / 2; // Số lượng dấu '-' mỗi bên

            // Hiển thị tiêu đề
            Console.WriteLine(horizontalLine);
            Console.WriteLine(infoLine.PadLeft(infoLinePadding + infoLine.Length).PadRight(totalWidth, ' '));
            Console.WriteLine(horizontalLine);

            // Thêm dấu " - " cho mỗi dòng
            var separator = new List<string>();
            for (int i = 0; i < header.Count; i++)
            {
                separator.Add(new string('-', columnWidths[i] + 2));
            }

            // Hiển thị bảng
            foreach (var row in table)
            {
                for (int i = 0; i < header.Count; i++)
                {
                    Console.Write("| " + row[i].PadRight(columnWidths[i]) + " ");
                }
                Console.WriteLine("|");
                if (row == header)
                {
                    Console.Write("+");
                    foreach (var width in columnWidths)
                    {
                        Console.Write(new string('-', width + 2) + "+");
                    }
                    Console.WriteLine();
                }
            }

            // Dòng " - " cuối cùng
            Console.Write("+");
            foreach (var width in columnWidths)
            {
                Console.Write(new string('-', width + 2) + "+");
            }
            Console.WriteLine();




            // int columnCount = table[0].Count;
            // int[] columnWidths = new int[columnCount];

            // for (int i = 0; i < columnCount; i++)
            // {
            //     columnWidths[i] = table.Max(Row => Row[i].Length);//
            // }

            //hiển thị bảng 
            // foreach (var row in table)
            // {
            //     for (int i = 0; i < columnCount; i++)
            //     {
            //         Console.Write(row[i].PadRight(columnWidths[i] + 2));
            //     }
            //     Console.WriteLine();
            // }

        }


        /// <summary>
        /// Hàm lấy id của student trong mysql dựa vào student No 
        /// </summary>
        /// <param name="StudentNo">student no cua hoc sinh</param>
        public int GetStudentId(string StudentNo)
        {
            int StudentId = -1;

            // Kết nối đến cơ sở dữ liệu
            MySqlConnection connection = Connection.GetConnection();
            // Tạo command để thực thi thủ tục lưu trữ
            string StoredProcedure = "sp_GetStudentId"; // ghi tên procedure ở đây
            using (MySqlCommand command = new MySqlCommand(StoredProcedure, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentNo", StudentNo); // ghi tên student no ở đây (cái student no mà đặt tên trong StoredProcedure)
                command.Parameters.Add("@StudentId", MySqlDbType.Int32).Direction = System.Data.ParameterDirection.Output;  // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                command.ExecuteNonQuery();

                if (command.Parameters["@StudentId"].Value != DBNull.Value) // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                {
                    StudentId = Convert.ToInt32(command.Parameters["@StudentId"].Value); // ghi tên student id ở đây (cái student id mà đặt tên trong StoredProcedure)
                }

                return StudentId;
            }

        }
    }
}