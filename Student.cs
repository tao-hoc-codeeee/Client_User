interface Students
{
    string Students_name { get; set; }
    string Students_email { get; set; }
    string Students_phone { get; set; }
    string Students_no {get; set; }
    string Students_address { get; set; }
    string Students_gender { get; set; }
    DateTime Students_birthday { get; set; }
}
//Class thong tin hoc sinh
public class Studentsinfo : Students
{
    public  string name_of_student;
    public  string students_mail;
    public  string students_phonenumber;
    public  string studentsno;
    public  string studentsaddress;
    public  string students_sex;
    public DateTime students_birth;
    public string Students_name
    {
        get { return name_of_student;}
        set { name_of_student = value;}
    }
    public DateTime Students_birthday
    {
        get { return students_birth; }
        set { students_birth = value;}
    }
    public string Students_email
    {
        get { return students_mail; }
        set { students_mail = value; }
    }
    public string Students_phone
    {
        get { return students_phonenumber;}
        set { students_phonenumber = value; }
    }
    public string Students_no
    {
        get {return studentsno;}
        set {studentsno = value;}
    }
    public string Students_address
    {
        get { return studentsaddress;}
        set { studentsaddress = value;}
    }
    public string Students_gender
    {
        get { return students_sex;}
        set { students_sex = value;}
    }
}