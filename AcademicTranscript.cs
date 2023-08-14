using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Client_User
{
    public class AcademicTranscript
    {
        public void DisplayAcademicTranscript()
        {
            string StudentNo = Students.StudentNo;
            Console.Clear();
            Console.WriteLine();
            int studentId = GetStudentId(StudentNo);
            Show_AcademicTranscript(studentId);
            Console.ReadKey();
        }


        /// <summary>
        /// hàm hiện thị bảng điểm môn của học sinh
        /// </summary>
        /// <param name="student_id">id tương ứng của student trong mysql</param>
        public void Show_AcademicTranscript(int student_id)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            MySqlConnection connection = Connection.GetConnection();

            string storedProcedure = "";
            MySqlCommand command = new MySqlCommand(storedProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StudentId", student_id);

            MySqlDataReader reader = command.ExecuteReader();

            var table = new List<List<string>>();

            //thêm tiêu đề bảng
            var header = new List<string> { };
            table.Add(header);

            // Tính toán chiều dài tối đa của mỗi cột
            int[] columnWidths = new int[header.Count];
            for (int i = 0; i < header.Count; i++)
            {
                columnWidths[i] = header[i].Length;
            }

            while (reader.Read())
            {
                // ... Lấy dữ liệu từ cơ sở dữ liệu như bạn đã làm



                // in ra các dòng
                var row = new List<string>
                {

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
            string infoLine = "Academic Transcript";
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