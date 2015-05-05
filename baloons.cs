//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BalloonsPops
//{

//    class Baloons
//    {

//        static void Main(string[] args)
//        {
//            GameBoard gameBoard = new GameBoard();
//            gameBoard.GenerateNewGame();
//            gameBoard.PrintGameBoard();
//            TopScore topScore = new TopScore();

//            topScore.OpenTopScoreList();

//            bool isCoordinates;
//            Coordinates coordinates = new Coordinates();
//            Command command = new Command();

//            //game Loop
//            while (gameBoard.RemainingBaloons > 0)
//            {
//                if (gameBoard.ReadInput(out isCoordinates, ref coordinates, ref command))
//                {
//                    if (isCoordinates)
//                    {
//                        gameBoard.Shoot(coordinates);
//                        gameBoard.PrintGameBoard();
//                    }
//                    else
//                    {
//                        switch (command.Value)
//                        {
//                            case "top":
//                                {
//                                    topScore.PrintScoreList();
//                                }
//                                break;
//                            case "restart":
//                                {
//                                    gameBoard.GenerateNewGame();
//                                    gameBoard.PrintGameBoard();
//                                }
//                                break;
//                            case "exit":
//                                {
//                                    return;
//                                }
//                        }
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Wrong Input!");
//                }
//            }
//            /////////

//            Person player = new Person();
//            player.Score = gameBoard.ShootCounter;

//            if (topScore.IsTopScore(player))
//            {
//                Console.WriteLine("Please enter your name for the top scoreboard: ");
//                player.Name = Console.ReadLine();
//                topScore.AddToTopScoreList(player);
//            }
//            topScore.SaveTopScoreList();
//        }
//    }
//}
