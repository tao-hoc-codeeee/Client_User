using System;
using Client_User;

class program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Clear();
        MenuLoginActivity menuLoginActivity = new MenuLoginActivity();
        menuLoginActivity.MenuLogin();
    }
}