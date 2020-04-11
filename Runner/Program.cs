using Microsoft.Win32.TaskScheduler;
using System;
namespace Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Program p = new Program();
            // p.RunApp(args);
            p.CreateTask(args);
        }

        private void CreateTask(string[] args)
        {
            Console.Write(args != null && args.Length >= 1 ? args[0] : "");
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Elevate";
                td.Principal.LogonType = TaskLogonType.ServiceAccount;
                td.Principal.UserId = @"NT AUTHORITY\LOCALSERVICE";
                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(CustomTrigger.CreateTrigger(TaskTriggerType.Registration));// DailyTrigger { DaysInterval = 2 });

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
                //td.Settings.AllowDemandStart = true;
                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@"Test", td);
                //ts.Execute("Test");

                Console.ReadKey();
                // Remove the task we just created
                ts.RootFolder.DeleteTask("Test");
            }
        }
        /*
        private void RunApp(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string a in args)
            {
                sb.Append(a + ' ');
            }
            string arguments = sb.ToString();
            Process p = new Process();

            ProcessStartInfo info = new ProcessStartInfo("cmd.exe", "/K ECHO " + arguments + " && whoami")
            {
                UseShellExecute = false,
                WorkingDirectory = @"C:\Windows\System32",
                LoadUserProfile = true,
                UserName = "Administrator"
            };
            System.Security.SecureString password = new System.Security.SecureString();
            string passwordStr = "blabla";

            foreach (char c in passwordStr)
            {
                password.AppendChar(c);
            }

            info.Password = password;
            p.StartInfo = info;
            p.Start();
            p.WaitForExit();
            Console.WriteLine("Program ended.");
            Console.ReadKey();
        }*/
    }
}
