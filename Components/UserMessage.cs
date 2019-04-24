using System;
namespace Chess.Components
{
    public static class UserMessage
    {

        /* This method takes care to ask the user of a specific color to select
        the piece with which makes the move by printing a message on the screen. */
        public static void WhatPieceYouWantMove(bool isWhite)
        {
            string player = (isWhite ? "WHITE" : "BLACK") + " PLAYER";
            Console.Write(player + ": What piece you want to move? (x,y): ");
        }

        /* This method takes care to ask the user of a specific color to select
        the position where to move the piece by printing a message on the screen. */
        public static void WhereYouWantMovePiece(bool isWhite)
        {
            string player = (isWhite ? "WHITE" : "BLACK") + " PLAYER";
            Console.Write(player + ": And where do you want to place it? (x,y): ");
        }

        /* This method takes care to communicate with the user by printing a message on the screen. */
        public static void Info(string message) 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /* This method takes care to alert the user that the game is ended with the winning
        of a specific team by printing a message on the screen. */
        public static void UserWins(bool isWhite)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string player = (isWhite ? "WHITE" : "BLACK") + " PLAYER";
            Console.WriteLine("[CHECK MATE] " + player + " won the game!");
            Console.ResetColor();
        }

        /* This method takes care to communicate an error to the user by printing a message on the screen. */
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR: " + message);
            Console.ResetColor();
        }
    }
}
