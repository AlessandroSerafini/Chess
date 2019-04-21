using System;
using System.Collections.Generic;
using Chess.Models;

namespace Chess.Components
{
    public class Chessboard
    {
        private Piece[,] piece; // Single pieces composed by column and row position coordinates
        private List<Piece> whitePieces, blackPieces; // List of white and black pieces on chessboard
        private bool lastMoveColor = false; // Last color move: true for white, false for black
        private bool isWhiteKingCheckmated = false, isBlackKingCheckmated = false; // Tells if kings are on check 
        private bool isWhiteKingBeaten = false, isBlackKingBeaten = false; // Tells if kings are died

        public Chessboard() {
            piece = new Piece[8, 8];
        }

        public void Init()
        {
            Console.Clear();
            UserMessage.Info("\n[\"C# CHESS GAME\"]");
            UserMessage.Info("\nMake the first move with white player choosing the piece you want to move by entering coordinates in (x, y) format, then your black opponent will have to do the same.\nThe game ends if a player with checkmated king fails to release it in a single move.");
            LoadPieces(); // Initialization of the chessboard loading pieces for both teams.
            Draw(false); // Draw the playing field.
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

            int startRow = isWhite ? 0 : 6;
            int endRow = isWhite ? 2 : 8;

            for (int currentCol = 0; currentCol < 8; currentCol++)
            {
                for (int currentRow = startRow; currentRow < endRow; currentRow++)
                {
                    if ((isWhite && currentRow == startRow) || (!isWhite && currentRow != startRow))
                    {
                        switch (currentCol)
                        {
                            case 0:
                            case 7:
                                {
                                    piece[currentCol, currentRow] = new Rook(isWhite, currentCol, currentRow, piece);
                                }
                                break;
                            case 1:
                            case 6:
                                {
                                    piece[currentCol, currentRow] = new Knight(isWhite, currentCol, currentRow, piece);
                                }
                                break;
                            case 2:
                            case 5:
                                {
                                    piece[currentCol, currentRow] = new Bishop(isWhite, currentCol, currentRow, piece);
                                }
                                break;
                            case 3:
                                {
                                    piece[currentCol, currentRow] = new Queen(isWhite, currentCol, currentRow, piece);
                                }
                                break;
                            case 4:
                                {
                                    piece[currentCol, currentRow] = new King(isWhite, currentCol, currentRow, piece);
                                }
                                break;
                        }
                        pieces.Add(piece[currentCol, currentRow]);
                    }
                    else
                    {
                        piece[currentCol, currentRow] = new Pawn(isWhite, currentCol, currentRow, piece);
                        pieces.Add(piece[currentCol, currentRow]);
                    }
                }
            }

            return pieces;
        }

        /* Draw the playing field highlighting the boxes with the characteristic
        "checkerboard" colors till complete a matrix of 8 columns and 8 rows. */
        private void Draw(bool clear = true)
        {
            if(clear) {
                Console.Clear();
            }
            for (int j = 7; j >= 0; j--)
            {
                Console.Write("\n" + (j + 1) + "   ");
                for (int i = 0; i < 8; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;   
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
            char pieceToDraw = piece[i, j] != null ? piece[i, j].Symbol : ' ';
            Console.Write(" " + pieceToDraw + "  ");
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
                int positionX = int.Parse(coordinates[0]) - 1;
                int positionY = int.Parse(coordinates[1]) - 1;

                UserMessage.WhatPieceYouWantMove(isWhite);
                
                /* Interpreting the final coordinates in the (X,Y) format
                and separating the values into two variables. */
                coordinates = Console.ReadLine().Split(',');
                int targetPositionX = int.Parse(coordinates[0]) - 1;
                int targetPositionY = int.Parse(coordinates[1]) - 1;

                
                if (Move(positionX, positionY, targetPositionX, targetPositionY, isWhite)) {

                    result = true;

                    Draw();

                    /* Check and eventually communicate if the
                    respective kings are in check position. */
                    if (isWhiteKingCheckmated || isBlackKingCheckmated)
                    {
                        UserMessage.Info((isWhiteKingCheckmated ? "White" : "Black") + " king is checkmated");
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
        private bool IsInsideChessboard(int positionX, int positionY, int targetPositionX, int targetPositionY)
        {
            return (positionX >= 0 && positionX < 8) && (positionY >= 0 && positionY < 8) &&
                (targetPositionX >= 0 && targetPositionX < 8) && (targetPositionY >= 0 && targetPositionY < 8);
        }


        /* This method takes care of moving a piece from a position to another,
        taking care to check the validity of the move. Otherwise an error will be printed on screen. */
        public bool Move(int positionX, int positionY, int targetPositionX, int targetPositionY, bool isWhite)
        {
            bool result = false;

            isWhiteKingCheckmated = isBlackKingCheckmated = false;

            // Check if move is inside the chessboard.
            if (IsInsideChessboard(positionX, positionY, targetPositionX, targetPositionY)) {

                Piece pieceToMove = piece[positionX, positionY];
                Position targetPosition = new Position(targetPositionX, targetPositionY);

                //Check if start cell is not empty.
                if (pieceToMove != null) {

                    //Check if player is moving his own piece.
                    if (pieceToMove.IsWhite == isWhite) {

                        //Check if initial and final position are different.
                        if ((positionX != targetPositionX) || (positionY != targetPositionY)) {

                            // Check if this piece can do this move.
                            if (pieceToMove.CheckMove(targetPosition))
                            {
                                result = true;
                                lastMoveColor=pieceToMove.IsWhite;

                                // "Eat" the enemy piece.
                                if (piece[targetPositionX, targetPositionY] != null) {
                                    if (piece[targetPositionX, targetPositionY].IsWhite) {
                                        whitePieces.Remove(piece[targetPositionX, targetPositionY]);
                                    } else {
                                        blackPieces.Remove(piece[targetPositionX, targetPositionY]);
                                    }
                                }
                                    
                                // Move the piece, updating its position and clean piece's old cell.
                                pieceToMove.Position = targetPosition;
                                piece[targetPositionX, targetPositionY] = pieceToMove;
                                piece[positionX, positionY] = null;
                                pieceToMove = null;

                                // Check if checkmate was done at the end of the move.
                                if (IsCheckMate(piece[targetPositionX, targetPositionY].IsWhite))
                                {
                                    if (piece[targetPositionX, targetPositionY].IsWhite)
                                    {
                                        isWhiteKingBeaten = true;
                                    }
                                    else
                                    {
                                        isBlackKingBeaten = true;
                                    }
                                }
                                else
                                {
                                    // If checkmate wasn't done, verify if check was done.
                                    if (piece[targetPositionX, targetPositionY].IsWhite) {
                                        isBlackKingCheckmated = IsCheckMate(!piece[targetPositionX, targetPositionY].IsWhite);
                                    } else {
                                        isWhiteKingCheckmated = IsCheckMate(!piece[targetPositionX, targetPositionY].IsWhite);
                                    }
                                }
                            } else {
                                UserMessage.Error("This piece can't make this move!");
                            }

                        } else {
                            UserMessage.Error("Initial position and target position are the same!");
                        }
                    } else {
                        UserMessage.Error("This is an opponent's piece!");
                    }
                } else {
                    UserMessage.Error("There isn't any piece here!");
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
                    if (piece.CheckMove(getKingPosition())) 
                    {
                        result = true;
                    }
                }
            } else {
                foreach (Piece piece in whitePieces)
                {
                    if (piece.CheckMove(getKingPosition(false)))
                    {
                        result = true;
                    }
                }
            }
          
            return result;
        }

        /* This method checks whether checkmate has been done based on color:
        true for white king checkmate, false for black king checkmate */
        public bool getCheckMate(bool isWhite = true)
        {
            return isWhite ? isWhiteKingBeaten : isBlackKingBeaten;
        }

        // This method return King position by color: true for wite king, false for black king
        private Position getKingPosition(bool isWhite = true)
        {
            Position kingPosition = new Position(0,0);

            foreach (Piece piece in isWhite ? whitePieces : blackPieces)
            {
                kingPosition = piece.IsKing ? piece.Position : kingPosition;
            }
            return kingPosition;
        }

        // This method tells me if i can do another turn, checking checkmates.
        public bool CanIDoAnotherTurn {
            get
            {
                return !getCheckMate(false) && !getCheckMate();
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
    }
}
