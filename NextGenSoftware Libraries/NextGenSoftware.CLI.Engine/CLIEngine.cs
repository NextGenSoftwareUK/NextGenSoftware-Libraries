﻿using System;
using System.Drawing;
using NextGenSoftware.ErrorHandling;
using NextGenSoftware.Utilities.ExtentionMethods;

namespace NextGenSoftware.CLI.Engine
{
    public static class CLIEngine
    {
        private static bool _nextMessageOnSameLine = false;
        public static Spinner Spinner = new Spinner();
        // public static Colorful.Console ColorfulConsole;

        public static ConsoleColor SuccessMessageColour { get; set; } = ConsoleColor.Green;
        public static ConsoleColor ErrorMessageColour { get; set; } = ConsoleColor.Red;
        public static ConsoleColor MessageColour { get; set; } = ConsoleColor.Yellow;
        public static ConsoleColor WorkingMessageColour { get; set; } = ConsoleColor.Yellow;
        public static bool SupressConsoleLogging { get; set; } = false;

        //public static ErrorHandlingBehaviour ErrorHandlingBehaviour { get; set; } = ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent;
        public static ErrorHandlingBehaviour ErrorHandlingBehaviour
        {
            get
            {
                return ErrorHandling.ErrorHandling.ErrorHandlingBehaviour;
            }
            set
            {
                ErrorHandling.ErrorHandling.ErrorHandlingBehaviour = value;
            }
        }

        public delegate void Error(object sender, CLIEngineErrorEventArgs e);
        public static event Error OnError;

        public static void WriteAsciMessage(string message, Color color)
        {
            Colorful.Console.WriteAscii(message, color);
        }

        public static void ShowColoursAvailable()
        {
            try
            {
                ShowMessage("", false);
                ConsoleColor oldColour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" Sorry, that colour is not valid. Please try again. The colour needs to be one of the following: ");

                string[] values = Enum.GetNames(typeof(ConsoleColor));

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != "Black")
                    {
                        PrintColour(values[i]);

                        if (i < values.Length - 2)
                            Console.Write(", ");

                        else if (i == values.Length - 2)
                            Console.Write(" or ");
                    }
                }

                ShowMessage("", false);
                Console.ForegroundColor = oldColour;
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ShowColoursAvailable method.", ex);
            }
        }

        public static void PrintColour(string colour)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colour);
            Console.Write(colour);
        }

        public static void ShowSuccessMessage(string message, bool lineSpace = true, bool noLineBreak = false, int intendBy = 1)
        {
            ShowMessage(message, SuccessMessageColour, lineSpace, noLineBreak, intendBy);
        }

        public static void ShowMessage(string message, bool lineSpace = true, bool noLineBreaks = false, int intendBy = 1)
        {
            ShowMessage(message, MessageColour, lineSpace, noLineBreaks, intendBy);
        }

        public static void ShowMessage(string message, ConsoleColor color, bool lineSpace = true, bool noLineBreaks = false, int intendBy = 1)
        {
            try
            {
                if (SupressConsoleLogging)
                    return;

                ConsoleColor existingColour = Console.ForegroundColor;
                Console.ForegroundColor = color;
                //ShowMessage(message, lineSpace, noLineBreaks, intendBy);
                bool wasSpinnerActive = false;

                if (Spinner.IsActive)
                {
                    Spinner.Stop();
                    wasSpinnerActive = true;
                    
                    if (!_nextMessageOnSameLine)
                        Console.WriteLine("");
                }

                _nextMessageOnSameLine = false;

                if (lineSpace)
                    Console.WriteLine(" ");

                string indent = "";
                for (int i = 0; i < intendBy; i++)
                    indent = string.Concat(indent, " ");

                if (noLineBreaks)
                    Console.Write(string.Concat(indent, message));
                else
                {
                    // if (wasSpinnerActive)
                    //     Console.WriteLine("");

                   
                    Console.WriteLine(string.Concat(indent, message));
                    //Console.WriteLine("");
                }

                Console.ForegroundColor = existingColour;
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ShowMessage method.", ex);
            }
        }

        public static void ShowWorkingMessage(string message, bool lineSpace = true, int intendBy = 1, bool nextMessageOnSameLine = false)
        {
            try
            {
                if (SupressConsoleLogging)
                    return;

                ShowMessage(message, WorkingMessageColour, lineSpace, true, intendBy);
                Spinner.Start();
                _nextMessageOnSameLine = nextMessageOnSameLine;
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ShowWorkingMessage method.", ex);
            }
        }

        public static void ShowWorkingMessage(string message, ConsoleColor color, bool lineSpace = true, int intendBy = 1, bool nextMessageOnSameLine = false)
        {
            try
            {
                if (SupressConsoleLogging)
                    return;

                ShowMessage(message, color, lineSpace, true, intendBy);
                Spinner.Start();
                _nextMessageOnSameLine = nextMessageOnSameLine;
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ShowWorkingMessage method.", ex);
            }
        }

        public static void BeginWorkingMessage(string message, ConsoleColor consoleColour)
        {
            ShowWorkingMessage(message, consoleColour, false, 1, true);
        }

        public static void BeginWorkingMessage(string message)
        {
            ShowWorkingMessage(message, WorkingMessageColour, false, 1, true);
        }

        public static void EndWorkingMessage(string message, ConsoleColor consoleColour)
        {
            ShowMessage(message, consoleColour, false, false, 0);
        }

        public static void EndWorkingMessage(string message)
        {
            ShowMessage(message, WorkingMessageColour, false, false, 0);
        }

        public static void ShowErrorMessage(string message, bool lineSpace = true, bool noLineBreak = false, int intendBy = 1)
        {
            try
            {
                ShowMessage(message, ErrorMessageColour, lineSpace, noLineBreak, intendBy);
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ShowErrorMessage method.", ex);
            }
        }

        public static string GetValidTitle(string message)
        {
            string title = GetValidInput(message).ToUpper();
            //string[] validTitles = new string[5] { "Mr", "Mrs", "Ms", "Miss", "Dr" };
            string validTitles = "MR,MRS,MS,MISS,DR";

            try
            {
                bool titleValid = false;
                while (!titleValid)
                {
                    if (!validTitles.Contains(title))
                    {
                        ShowErrorMessage("Title invalid. Please try again.");
                        title = GetValidInput(message).ToUpper();
                    }
                    else
                        titleValid = true;
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.GetValidTitle method.", ex);
            }

            return ExtensionMethods.ToPascalCase(title);
        }

        public static string GetValidInput(string message)
        {
            string input = "";

            try
            {
                while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    ShowMessage(string.Concat("", message), true, true);
                    input = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.GetValidInput method.", ex);
            }

            return input;
        }

        public static bool GetConfirmation(string message)
        {
            bool validKey = false;
            bool confirm = false;

            try
            {
                while (!validKey)
                {
                    //ShowMessage("", false);
                    ShowMessage(message, true, true);
                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Y)
                    {
                        confirm = true;
                        validKey = true;
                    }

                    if (key == ConsoleKey.N)
                    {
                        confirm = false;
                        validKey = true;
                    }
                }

            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.GetValidInput method.", ex);
            }

            return confirm;
        }

        /*
        public static string GetValidEmail(string message, bool checkIfEmailAlreadyInUse)
        {
            bool emailValid = false;
            string email = "";

            while (!emailValid)
            {
                ShowMessage(string.Concat("", message), true, true);
                email = Console.ReadLine();

                if (!ValidationHelper.IsValidEmail(email))
                    ShowErrorMessage("That email is not valid. Please try again.");

                else if (checkIfEmailAlreadyInUse)
                {
                    ShowWorkingMessage("Checking if email already in use...");

                    OASISResult<bool> checkIfEmailAlreadyInUseResult = STAR.OASISAPI.Avatar.CheckIfEmailIsAlreadyInUse(email);

                    if (checkIfEmailAlreadyInUseResult.Result)
                        ShowErrorMessage(checkIfEmailAlreadyInUseResult.Message);
                    else
                    {
                        emailValid = true;
                        Spinner.Stop();
                        ShowMessage("", false);
                    }
                }
                else
                    emailValid = true;
            }

            return email;
        }*/

        public static string GetValidPassword()
        {
            string password = "";
            string password2 = "";

            try
            {
                ShowMessage("", false);

                while ((string.IsNullOrEmpty(password) && string.IsNullOrEmpty(password2)) || password != password2)
                {
                    password = ReadPassword("What is the password you wish to use? ");
                    password2 = ReadPassword("Please confirm password: ");

                    if (password != password2)
                        ShowErrorMessage("The passwords do not match. Please try again.");
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.GetValidPassword method.", ex);
            }

            return password;
        }

        public static string ReadPassword(string message)
        {
            string password = "";
            ConsoleKey key;

            try
            {
                while (string.IsNullOrEmpty(password) && string.IsNullOrWhiteSpace(password))
                {
                    ShowMessage(string.Concat("", message), true, true);

                    do
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        key = keyInfo.Key;

                        if (key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            Console.Write("\b \b");
                            password = password[0..^1];
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            Console.Write("*");
                            password += keyInfo.KeyChar;
                        }
                    } while (key != ConsoleKey.Enter);

                    ShowMessage("", false);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.ReadPassword method.", ex);
            }

            return password;
        }

        public static void GetValidColour(ref ConsoleColor favColour, ref ConsoleColor cliColour)
        {
            try
            {
                bool colourSet = false;
                while (!colourSet)
                {
                    ShowMessage("What is your favourite colour? ", true, true);
                    string colour = Console.ReadLine();
                    colour = ExtensionMethods.ToPascalCase(colour);
                    //object colourObj = null;
                    // ConsoleColor colourObj;

                    //if (Enum.TryParse(typeof(ConsoleColor), colour, out colourObj))
                    if (Enum.TryParse(colour, out favColour))
                    {
                        // favColour = (ConsoleColor)colourObj;
                        Console.ForegroundColor = favColour;
                        ShowMessage("Do you prefer to use your favourite colour? :) ", true, true);

                        if (Console.ReadKey().Key != ConsoleKey.Y)
                        {
                            Console.WriteLine("");

                            while (!colourSet)
                            {
                                //Defaults
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Spinner.Colour = ConsoleColor.Green;

                                ShowMessage("Which colour would you prefer? ", true, true);
                                colour = Console.ReadLine();
                                colour = ExtensionMethods.ToPascalCase(colour);
                                //colourObj = null;

                                //if (Enum.TryParse(typeof(ConsoleColor), colour, out colourObj))
                                if (Enum.TryParse(colour, out cliColour))
                                {
                                    //cliColour = (ConsoleColor)colourObj;
                                    Console.ForegroundColor = cliColour;
                                    Spinner.Colour = cliColour;

                                    // ShowMessage("", false);
                                    ShowMessage("This colour ok? ", true, true);

                                    if (Console.ReadKey().Key == ConsoleKey.Y)
                                        colourSet = true;
                                    else
                                        ShowMessage("", false);
                                }
                                else
                                    ShowColoursAvailable();
                            }
                        }
                        else
                            colourSet = true;
                    }
                    else
                        ShowColoursAvailable();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occured in CLIEngine.GetValidColour method.", ex);
            }
        }

        //private static string WriteMessage(string message, bool lineSpace = true, int intendBy = 1)
        //{
        //    if (Spinner.IsActive)
        //    {
        //        Spinner.Stop();
        //        Console.WriteLine("");
        //    }

        //    ConsoleColor existingColour = Console.ForegroundColor;
        //    Console.ForegroundColor = ConsoleColor.Red;

        //    if (lineSpace)
        //        Console.WriteLine("");

        //    string indent = "";
        //    for (int i = 0; i < intendBy; i++)
        //        indent = string.Concat(indent, " ");

        //    Console.WriteLine(string.Concat(indent, message));
        //    Console.ForegroundColor = existingColour;
        //}

        private static void HandleError(string message, Exception exception)
        {
            message = string.Concat(message, exception != null ? $". Error Details: {exception}" : "");
            //Logging.Logging.Log(message, LogType.Error);

            OnError.Invoke(null, new CLIEngineErrorEventArgs { Reason = message, ErrorDetails = exception });

            switch (ErrorHandlingBehaviour)
            {
                case ErrorHandlingBehaviour.AlwaysThrowExceptionOnError:
                    throw new CLIEngineException(message, exception);

                case ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent:
                    {
                        if (OnError == null)
                            throw new CLIEngineException(message, exception);
                    }
                    break;
            }
        }
    }
}