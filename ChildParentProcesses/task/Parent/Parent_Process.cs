using System;
using System.Diagnostics;

namespace SystemProgrammingTasks
{
    class Parent_Process
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers and an operation (+, -, *, /) for the child process:");
            string input = Console.ReadLine();

            input = input.Replace(",", " ");

            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid input. Expected format: <number1> <number2> <operation>");
                return;
            }

            string childExecutable = "C:\\Users\\maksu\\RiderProjects\\SystemProgrammingTasks\\Child\\bin\\Debug\\net9.0\\Child.exe";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = childExecutable,
                Arguments = string.Join(" ", parts),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.EnableRaisingEvents = true;

                process.Exited += (sender, args) =>
                {
                    Console.WriteLine($"Child process finished with exit code: {process.ExitCode}");
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        Console.WriteLine(args.Data);
                    }
                };


                process.Start();
                process.BeginOutputReadLine();

                Console.WriteLine("Do you want to wait for the child process to finish? (y/n)");
                string waitForExit = Console.ReadLine();

                if (waitForExit?.ToLower() == "y")
                {
                    process.WaitForExit();
                }
                else
                {
                    Console.WriteLine("Press any key to terminate the child process...");
                    Console.ReadKey();
                    if (!process.HasExited)
                    {
                        process.Kill();
                        Console.WriteLine("Child process was terminated.");
                    }
                }

                Console.WriteLine("Program is done.");
            }
        }
    }
}
