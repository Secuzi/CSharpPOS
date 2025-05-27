using Ecommerce.Core.Services;
using FinalOutput.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that displays view for authorization
    /// </summary>
    public class AuthorizationView : MenuView
    {
        /// <summary>
        /// Displays view in console.
        /// </summary>
        public override void Display()
        {
            // Initialization
            Console.Title = "Authorization";
            Console.CursorVisible = false;
            Console.Clear();
            string[] options = { "Login", "Exit" };
            Console.SetWindowSize(98, 40);
            AsciiArt.PrintAuthorizationAsciiArt(16, 4);


            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {options[i]}");
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            // Logic for handling user key input.
            switch (key.KeyChar)
            {
                case '1':
                    PromptLogin:
                    try 
                    { 
                        LoginVM loginVM = new LoginVM(new AccountAuthenticationService());
                        loginVM.PromptUserLogin();
                        Console.Clear();
                        var userType = loginVM.GetUserType();

                        if(userType == eUserType.Admin)
                        {
                            Menu.SetDisplayView(new AdminMenuView());                           
                        }
                        else 
                        {
                            Menu.SetDisplayView(new CashierMenuView()); 
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("\nWrong username or password!");
                        Console.Write("Do you want to try again? (Y:N): ");
                        string response = Console.ReadLine().ToString().ToLower();
                        if(response == "y")                        
                            goto PromptLogin;
                        
                    }
                    break;

                case '2':
                    Menu.SetDisplayOff();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    Thread.Sleep(1200);
                    break;

            }

            
        }
    }
}

