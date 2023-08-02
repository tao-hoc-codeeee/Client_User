using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client_User
{
    public class ChangePassword
    {
        public int j;
        public void DisplayChangePassword()
        {
            string OldPassword;
            string NewPassword;
            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("----------------------Change Password---------------------");
                    Console.WriteLine("==========================================================");
                    Console.Write("Enter old password: ");
                    OldPassword = Convert.ToString(Console.ReadLine() ?? "".TrimEnd(' '));
                    Console.Write("Enter new password: ");
                    NewPassword = Convert.ToString(Console.ReadLine() ?? "".TrimEnd(' '));

                } while (j != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex.Message);
            }

        }
    }
}