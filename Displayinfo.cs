using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Client_User
{
    public class Displayinfo
    {
        public int j;

        public string StudentNo;
        public void Information()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==========================================================");
            Console.WriteLine("-------------------------Infomation-----------------------");
            Console.WriteLine("==========================================================");
            int studentId = GetStudentId(StudentNo);
            Show_Information(studentId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student_id"></param>
        public void Show_Information(int student_id)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            MySqlConnection connection = Connection.GetConnection();

            string StoredProcedure = "";
            MySqlCommand command = new MySqlCommand(StoredProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("", student_id);// student id tương ứng trong storedProcedure

            MySqlDataReader reader = command.ExecuteReader();

            var table = new List<List<string>>();

            //thêm tiêu đề bảng
            var header = new List<string> { "Student No", "Name", "Class", "Brith Date", "Gender" };
            table.Add(header);

            while (reader.Read())
            {
                string StudentNo = reader.GetString("");// Student no tương úng trong StoredProcedure
                string Name = reader.GetString("");  // Tương đối với name của bạn
                string Class = reader.GetString("");
                DateTime BirthDate = reader.GetDateTime("");
                string Gender = reader.GetString("");


                var row = new List<string>
                {
                    StudentNo,
                    Name,
                    Class,
                    BirthDate.ToString(),
                    Gender
                };

                table.Add(row);
            }

            reader.Close();
            connection.Close();


            //hiển thị bảng
            int columnCount = table[0].Count;
            int[] columnWidths = new int[columnCount];

            for (int i = 0; i < columnCount; i++)
            {
                columnWidths[i] = table.Max(Row => Row[i].Length);//
            }

            foreach (var row in table)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    Console.Write(row[i].PadRight(columnWidths[i] + 2));
                }
                Console.WriteLine();
            }

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