using System;

namespace SystemProgrammingTasks
{
    class Child_Process
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid arguments. Expected format: <number1> <number2> <operation>");
                return;
            }

            try
            {
                int number1 = int.Parse(args[0]);
                int number2 = int.Parse(args[1]);
                string operation = args[2];

                int result;
                if (operation == "+")
                {
                    result = number1 + number2;
                }
                else if (operation == "-")
                {
                    result = number1 - number2;
                }
                else if (operation == "*")
                {
                    result = number1 * number2;
                }
                else if (operation == "/")
                {
                    if (number2 == 0)
                    {
                        throw new InvalidOperationException("Division by zero is not allowed.");
                    }
                    result = number1 / number2;
                }
                else
                {
                    throw new InvalidOperationException("Unknown operation");
                }


                Console.WriteLine($"Arguments: {number1} {operation} {number2} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}