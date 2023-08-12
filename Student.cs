using MySql.Data.MySqlClient;
public class Students
{
    public string Students_name { get; set; }
    public string Students_email { get; set; }
    public string Students_phone { get; set; }
    public string Students_no {get; set; }
    public string Students_address { get; set; }
    public string Students_gender { get; set; }
    public DateTime Students_birthday { get; set; }
    public string Student_password { get; set; }
}
//Class thong tin hoc sinh
// public class Studentsinfo : Students
// {
//     public  string name_of_student;
//     public  string students_mail;
//     public  string students_phonenumber;
//     public  string studentsno;
//     public  string studentsaddress;
//     public  string students_sex;
//     public DateTime students_birth;
//     public string passwordrandom;
//     public string Students_name
//     {
//         get { return name_of_student;}
//         set { name_of_student = value;}
//     }
//     public DateTime Students_birthday
//     {
//         get { return students_birth; }
//         set { students_birth = value;}
//     }
//     public string Students_email
//     {
//         get { return students_mail; }
//         set { students_mail = value; }
//     }
//     public string Students_phone
//     {
//         get { return students_phonenumber;}
//         set { students_phonenumber = value; }
//     }
//     public string Students_no
//     {
//         get {return studentsno;}
//         set {studentsno = value;}
//     }
//     public string Students_address
//     {
//         get { return studentsaddress;}
//         set { studentsaddress = value;}
//     }
//     public string Students_gender
//     {
//         get { return students_sex;}
//         set { students_sex = value;}
//     }
//     public string Student_password
//     {
//         get { return passwordrandom;}
//         set { passwordrandom = value;}
//     }
// }
public class Studentconvert
{
    public static Students Convert(MySqlDataReader reader)
    {
        Students students = new Students();
        students.Students_name = reader.GetString("studentname");
        students.Students_email = reader.GetString("emailstduent");
        students.Students_phone = reader.GetString("studentphone");
        students.Students_gender = reader.GetString("genderstudent");
        students.Students_address = reader.GetString("studentaddress");
        students.Students_no = reader.GetString("studentUID");
        students.Students_birthday = reader.GetDateTime("birthdaystudent");
        students.Student_password = reader.GetString("passwords");
        return students;
    }
} 