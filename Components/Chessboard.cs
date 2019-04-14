using System;
using System.Collections.Generic;
using Chess.Models;

namespace Chess.Components
{
    public class Chessboard
    {
        private Piece[,] piece;
        private Position whiteKing, blackKing;
        private List<Piece> whitePieces, blackPieces;

        private bool lastMoveColor = false;

        private bool isWhiteKingOnCheck = false, isBlackKingOnCheck = false;
        private bool isWhiteKingMatechecked = false, isBlackKingMatechecked = false;

        public Chessboard() {
            piece = new Piece[8, 8];
        }

        public void Init()
        {
            // Initialization of the chessboard loading pieces for both teams.
            LoadPieces();
            // Draw the playing field.
            Draw();
        }

        /* Initialization of the chessboard: this method takes care of drawing all the pieces for
        both teams on chessboard in order to obtain a vail game field. */
        private void LoadPieces()
        {
            whitePieces = LoadPieces(true);
            blackPieces = LoadPieces(false);
        }
        

        /* Adds the pieces to the chessboard by placing a specific piece based on cell.
        This method accepts the color type (default is the same color as white). */
        private List<Piece> LoadPieces(bool isWhite)
        {
            List<Piece> pieces = new List<Piece>();

            int colStart = isWhite ? 0 : 6;
            int colEnd = isWhite ? 2 : 8;

            for (int i = 0; i < 8; i++)
            {
                for (int j = colStart; j < colEnd; j++)
                {
                    if ((isWhite && j == colStart) || (!isWhite && j != colStart))
                    {
                        switch (i)
                        {
                            case 0:
                            case 7:
                                {
                                    piece[i, j] = new Rook(isWhite, i, j, piece);
                                }
                                break;
                            case 1:
                            case 6:
                                {
                                    piece[i, j] = new Knight(isWhite, i, j, piece);
                                }
                                break;
                            case 2:
                            case 5:
                                {
                                    piece[i, j] = new Bishop(isWhite, i, j, piece);
                                }
                                break;
                            case 3:
                                {
                                    piece[i, j] = new Queen(isWhite, i, j, piece);
                                }
                                break;
                            case 4:
                                {
                                    piece[i, j] = new King(isWhite, i, j, piece);

                                    if (isWhite)
                                    {
                                        whiteKing = piece[i, j].Start;
                                    }
                                    else
                                    {
                                        blackKing = piece[i, j].Start;
                                    }

                                }
                                break;
                        }
                        pieces.Add(piece[i, j]);
                    }
                    else
                    {
                        piece[i, j] = new Pawn(isWhite, i, j, piece);
                        pieces.Add(piece[i, j]);
                    }
                }
            }

            return pieces;
        }

        /* Draw the playing field highlighting the boxes with the characteristic
        "checkerboard" colors till complete a matrix of 8 columns and 8 rows. */
        private void Draw()
        {
            Console.Clear();
            for (int j = 7; j >= 0; j--)
            {
                Console.Write("\n" + (j + 1) + "   ");
                for (int i = 0; i < 8; i++)
                {
                    Console.ForegroundColor = IsWhiteColor(i, j) ? ConsoleColor.DarkGreen : ConsoleColor.Blue;   
                    Console.BackgroundColor = i%2==0 ? j%2==0 ? ConsoleColor.DarkGray : ConsoleColor.Gray : j%2==0 ? ConsoleColor.Gray : ConsoleColor.DarkGray ; 
                    DrawPiece(i, j);
                    Console.ResetColor();
                }
            }
            Console.Write("\n\n   ");
            for (int k = 0; k < 8; k++) {
                Console.Write("  " + (k + 1) + " ");
            }
            Console.Write("\n\n");
        }

        // This method simply takes care of drawing the piece.
        public void DrawPiece(int i, int j)
        {
            char pieceToDraw = piece[i, j] != null ? piece[i, j].Identifier : ' ';
            Console.Write(" " + pieceToDraw + "  ");
        }

        /* This method returns the color of the piece in the given position
        (true for white piece, false for black piece). */
        public bool IsWhiteColor(int i, int j)
        {
            return piece[i, j] != null ? piece[i, j].IsWhite : false;
        }

        /* This method takes care of reading the position of the move that
        the user is about to perform by accepting the coordinates as input.
        Coordinates will be validated and in case of error,
        a message will be printed on screen. */
        public bool GetMoveCoordinates(bool isWhite)
        {
            bool result = false;
            try
            {
                string[] coordinates;

                /* Interpreting the starting coordinates in the (X,Y) format
                and separating the values into two variables. */
                coordinates = Console.ReadLine().Split(',');
                int startX = int.Parse(coordinates[0]) - 1;
                int startY = int.Parse(coordinates[1]) - 1;

                UserMessage.WhatPieceYouWantMove(isWhite);
                
                /* Interpreting the final coordinates in the (X,Y) format
                and separating the values into two variables. */
                coordinates = Console.ReadLine().Split(',');
                int endX = int.Parse(coordinates[0]) - 1;
                int endY = int.Parse(coordinates[1]) - 1;

                
                if (Move(startX, startY, endX, endY, isWhite)) {

                    result = true;

                    Draw();



                    /* Check and eventually communicate if the
                    respective kings are in check position. */
                    if (isWhiteKingOnCheck)
                    {
                        UserMessage.Info("White King is on check", ConsoleColor.Green);
                    }

                    if (isBlackKingOnCheck)
                    {
                        UserMessage.Info("Black King is on check", ConsoleColor.Green);
                    }
                }

            }
            catch (System.FormatException)
            {
                UserMessage.Error("Wrong input format, input must be: X,Y");
            }
            catch (System.IndexOutOfRangeException)
            {
                UserMessage.Error("Wrong input format, input must be: X,Y");
            }

            return result;
        }

        // This method takes care of checking if the move is regularly inside the chessboard.
        private bool IsInsideChessboard(int startX, int startY, int endX, int endY)
        {
            return (startX >= 0 && startX < 8) && (startY >= 0 && startY < 8) &&
                (endX >= 0 && endX < 8) && (endY >= 0 && endY < 8);
        }


        /* This method takes care of moving a piece from a position to another,
        taking care to check the validity of the move. Otherwise an error will be printed on screen. */
        public bool Move(int startX, int startY, int endX, int endY, bool isWhite)
        {
            bool result = false;

            isWhiteKingOnCheck = isBlackKingOnCheck = false;

            // Check if move is inside the chessboard.
            if (IsInsideChessboard(startX, startY, endX, endY)) {

                Piece pieceToMove = piece[startX, startY];
                Position end = new Position(endX, endY);

                //Check if start cell is not empty.
                if (pieceToMove != null) {

                    //Check if player is moving his own piece.
                    if (pieceToMove.IsWhite == isWhite) {

                        //Check if initial and final position are different.
                        if ((startX != endX) || (startY != endY)) {

                            // Check if this piece can do this move.
                            if (pieceToMove.CheckMove(end))
                            {
                                result = true;
                                lastMoveColor=pieceToMove.IsWhite;
                                
                                // If you're moving one of two kings, update its position.
                                if (pieceToMove.Start == whiteKing) {
                                    whiteKing = end;
                                }
                                if (pieceToMove.Start == blackKing)
                                {
                                    blackKing = end;
                                }

                                // "Eat" the enemy piece.
                                if (piece[endX, endY] != null) {
                                    if (piece[endX, endY].IsWhite) {
                                        whitePieces.Remove(piece[endX, endY]);
                                    } else {
                                        blackPieces.Remove(piece[endX, endY]);
                                    }
                                }
                                    
                                // Move the piece, updating its position.
                                pieceToMove.Start = end;
                                piece[endX, endY] = pieceToMove;

                                // Clean piece's old cell.
                                piece[startX, startY] = null;
                                pieceToMove = null;

                                // Check if checkmate was done at the end of the move.
                                if (IsCheckMate(piece[endX, endY].IsWhite))
                                {
                                    if (piece[endX, endY].IsWhite)
                                    {
                                        isWhiteKingMatechecked = true;
                                    }
                                    else
                                    {
                                        isBlackKingMatechecked = true;
                                    }
                                }
                                else
                                {
                                    // If checkmate wasn't done, verify if check was done.
                                    if (piece[endX, endY].IsWhite) {
                                        isBlackKingOnCheck = IsCheckMate(!piece[endX, endY].IsWhite);
                                    } else {
                                        isWhiteKingOnCheck = IsCheckMate(!piece[endX, endY].IsWhite);
                                    }
                                }
                            } else {
                                UserMessage.Error("Invalid move!");
                            }

                        } else {
                            UserMessage.Error("Start position and end position must be different!");
                        }
                    } else {
                        UserMessage.Error("You can't move your opponent's piece!");
                    }
                } else {
                    UserMessage.Error("Selected position is empty!");
                }
            } else {
                UserMessage.Error("Selected position does not exists!");
            }
                           
            return result;
        }

        /* This method takes care of verifying if is possible to checkmate based on the color of the player,
        checking for each piece whether with a move it is possible to "eat" the opposing king. */
        private bool IsCheckMate(bool isWhite)
        {
            bool result = false;

            if (isWhite) 
            {
                foreach (Piece piece in blackPieces) {
                    if (piece.CheckMove(whiteKing)) 
                    {
                        result = true;
                    }
                }
            } else {
                foreach (Piece piece in whitePieces)
                {
                    if (piece.CheckMove(blackKing))
                    {
                        result = true;
                    }
                }
            }
          
            return result;
        }

        public bool CanIDoAnotherTurn {
            get
            {
                return !CheckMateBlack && !CheckMateWhite;
            }
        }

        // This method checks last move color (true for white, false for black).
        public bool CheckLastMoveColor
        {
            get
            {
                return lastMoveColor;
            }
        }

        // This method checks whether checkmate has been done against the white king.
        private bool CheckMateWhite
        {
            get
            {
                return isWhiteKingMatechecked;
            }
        }

        // This method checks whether checkmate has been done against the black king.
        public bool CheckMateBlack
        {
            get
            {
                return isBlackKingMatechecked;
            }
        }
    }
}
