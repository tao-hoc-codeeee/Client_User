using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Client_User
{
    public class Displayinfo
    {
        public void Information()
        {
            
        }
        public void Show_Information(string student_id)
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
                // Type enum của Gender chưa bt ghi thế nào nên sẽ ghi sau :)))


                var row = new List<string>
                {
                    StudentNo,
                    Name,
                    Class,
                    BirthDate.ToString(),
                    //ghi Gender ở đây
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

        public void GetStudentId(string StudentNo)
        {
            
        }
    }
}