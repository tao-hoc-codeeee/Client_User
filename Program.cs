using System;
using Client_User;

class program
{
    static void Main(string[] args)
    {
        try
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            MenuLoginActivity menuLoginActivity = new MenuLoginActivity();
            menuLoginActivity.MenuLogin();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry!\nSomething went wrong, please try again in a few minutes!" + ex.Message);
        }

    }
}