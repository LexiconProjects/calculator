/**
 * Console application that prints a menu for a simple calculator, takes user input and performs:
 *  - Basic math operations (Addition, Subtraction, Division, Multiplication, To Power) with 2 numbers
 *  - Basic math operations (Addition, Substraction) with more than 2 numbers
 *  Looping the menu allows the user to continue solving equations. The application stops running when the user exits the menu.
 **/

using System;

namespace BestCalculator
{
    class Program
    {
        //Declaring global variables
        static double num1, num2, result;
        static string operand;
        static bool validDivision = true;
        static bool printResult = true;

        static string menuOption;

        static void Main(string[] args)
        {
            bool run = true;
            Console.WriteLine("**************** Welcome to Calculator ****************");

            //The menu will be available until the user chooses to exit.
            while (run)
            {
                PrintMenu();

                //Storing the chosen type of equation
                menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "e":
                        Console.WriteLine("\n" + "*********************** Goodbye! ***********************");
                        run = false;
                        break;

                    case "1":
                        //For a simple operation, the user is prompted to introduce the numbers and desired single operation

                        try
                        {
                            Console.Write("Write your first number:     ");
                            num1 = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Write operation(+ - * / ^):  ");
                            operand = Console.ReadLine();

                            Console.Write("Write your second number:    ");
                            num2 = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Invalid input, please try again.");
                            Console.ResetColor();
                            break;
                        }


                        //Check if the equation is valid
                        if (num2 == 0 && operand == "/")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cannot divide by 0.");
                            Console.ResetColor();
                        }
                        else
                        {
                            printResult = true;

                            //performing operation with the input
                            PerformSimpleOperation();

                            //Prints the result in case the equation had the supported operations
                            if (printResult)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"_____________________________= ");
                                Console.WriteLine($"                             {result}");
                                Console.ResetColor();

                            }

                        }

                        result = 0;
                        break;


                    case "2":
                        //For a complex operation, the user is expected to write an equation only containing addition and substraction 

                        PrintComplexOperationRequirements(); //print and emphasis the supported operations (+,-)

                        Console.WriteLine("Write your equation separating each element with a space: \"1 + 1 =\"");

                        //temporary variables for storing operators and the numbers from the equation
                        string numb2, op;

                        //Storing the equation and converting it to an array of equation elements
                        string equation = Console.ReadLine();
                        string[] e = new string[(equation.Split(' ').Length)];
                        e = equation.Split(' ');

                        //Result is updated as the equation is evaluated
                        try
                        {
                            result = Convert.ToDouble(e[0]);

                            //The array of equation elements is traversed and for each itteration the result value is updated
                            for (int i = 1; i < (e.Length) - 1; i += 2)
                            {
                                //Storing the next operand and number of the equation
                                op = e[i];
                                numb2 = e[i + 1];

                                //Updating the result of the equation based on the number and operand found
                                switch (op)
                                {
                                    case "+":
                                        result = Add(result, Convert.ToDouble(numb2));
                                        break;
                                    case "-":
                                        result = Substract(result, Convert.ToDouble(numb2));
                                        break;
                                    default:
                                        //In case an invalid operand is found, the calculation of the result is stopped
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Wrong operand.");
                                        PrintComplexOperationRequirements();
                                        Console.ResetColor();

                                        i = e.Length;
                                        printResult = false;
                                        break;
                                }
                            }

                            //Prints the result in case the equation had the supported operations
                            if (printResult)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{equation} {result}");
                                Console.ResetColor();
                            }


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid equation. \nMake sure to only include numbers, spaces, operands and only one equal. ");
                            break;
                        }

                        result = 0;
                        break;

                    default:
                        //The user is prompted to try again in case an unexisting option is typed
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        break;
                }


                //After each equation, the user is able to exit the programe directly without returning to the main menu.
                if (run)
                {
                    Console.WriteLine("To return to menu, press \"m\". To exit the calculator, press \"e\".");
                    menuOption = Console.ReadLine();

                    if (Equals(menuOption, "e"))
                    {
                        Console.WriteLine("\n" + "*********************** Goodbye! ***********************");
                        run = false;
                    }
                    else
                        if (!Equals(menuOption, "m"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. You will be returned to main menu.");
                        Console.ResetColor();

                    }

                }
            }


            Console.ReadLine();
        }

        //Method that prints the current supported operators for complex equations
        private static void PrintComplexOperationRequirements()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*** Please note that only + and - are supported. ***");
            Console.ResetColor();
        }

        //Method that asks for the desired calculation from the user and uses previously requested numbers to perform it 
        private static void PerformSimpleOperation()
        {

            switch (operand)
            {
                case "+":
                    result = Add(num1, num2);
                    break;
                case "-":
                    result = Substract(num1, num2);
                    break;
                case "*":
                    result = Multiply(num1, num2);
                    break;
                case "/":
                    if (validDivision)
                    {
                        result = Divide(num1, num2);
                    }
                    else
                    {
                        validDivision = false;
                    }

                    break;
                case "^":
                    result = ToPower(num1, num2);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please try again. Invalid operand.");
                    Console.ResetColor();
                    printResult = false;
                    break;
            }
        }

        //Methods that prints all the possible equations in the form of a menu
        private static void PrintMenu()
        {
            Console.WriteLine("\n Please select the type of equation:");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1 Simple  - If you want to solve an equation with one operation.");
            Console.WriteLine("2 Complex - If you want to solve an equation with multiple operations.");
            Console.ResetColor();
            Console.WriteLine("To exit press \"e\" .");

        }

        //Methods used to perform basic math operation with user given input
        private static double Add(double a, double b)
        {
            return (a + b);
        }

        private static double Substract(double a, double b)
        {
            return (a - b);
        }

        private static double Multiply(double a, double b)
        {
            return (a * b);
        }

        private static double Divide(double a, double b)
        {
            return (a / b);
        }

        private static double ToPower(double a, double b)
        {
            return Math.Pow(a, b);
        }

    }
}
