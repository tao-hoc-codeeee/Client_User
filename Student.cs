using MySql.Data.MySqlClient;
public class Students
{
       public static string StudentNo { get; private set; }

    public static void SetStudentNo(string studentNo)
    {
        StudentNo = studentNo;
    }
}

