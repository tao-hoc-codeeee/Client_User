using System;


namespace Client_User
{

    public class MenuLoginActivity
    {
        public int j = -1;
        public void MenuLogin()
        {
            // try
            // {
                LoginUI loginUI = new LoginUI();
                loginUI.Login();
                MenuUser menuAdministrator = new MenuUser();
                menuAdministrator.Mainmenu();
            // }
            // catch
            // {
            //     Console.WriteLine("Sorry!\nSomething went wrong, please try again in a few minutes!");
            // }
        }
    }

    public class MenuUser
    {
        public int j;
        public void Mainmenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==========================================================");
                Console.WriteLine("-----------------------Student Menu-----------------------");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. Show information.");
                Console.WriteLine("2. Academic Transcript.");
                Console.WriteLine("3. History Coures.");
                Console.WriteLine("4. Change Password.");
                Console.WriteLine("5. Information Class.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("==========================================================");
                Console.Write("Your Choice: ");
                j = Convert.ToInt32(Console.ReadLine() ?? "".TrimEnd(' '));

                switch(j)
                {
                    case 1:
                        Displayinfo display_1 = new Displayinfo();
                        display_1.Information();
                    break;
                    case 2:
                        AcademicTranscript display_2 = new AcademicTranscript();
                        display_2.DisplayAcademicTranscript();
                    break;
                    case 3:
                    break;
                    case 4:
                        ChangePassword display_4 = new ChangePassword();
                        display_4.DisplayChangePassword();
                    break;
                    case 5:
                        InfomationClass display_5 = new InfomationClass();
                        display_5.DisplayInfoClass();
                    break;
                    case 0:
                    break;
                    default:
                        Console.WriteLine("Please re-enter"); Console.ReadKey();
                    break;
                }

            } while (j != 0);
        }
    }
}