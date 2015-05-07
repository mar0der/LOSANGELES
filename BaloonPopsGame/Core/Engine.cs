namespace BalloonsPops
{
    using System;
    using Core;
    using Data;
    using Exceptions;
    using Utilities;

    public class Engine
    {
        private bool isGameRunning = true;
        private ConsoleRenderer consoleRenderer;
        private Player player;
        private TopPlayers topPlayers;
        private DataRepository dataRepository;

        public GameBoard GameBoard { get; set; }

        public EntityPopper EntityPopper { get; set; }

        /// <summary>
        /// The Game starting loop
        /// </summary>
        public void Run()
        {
            this.topPlayers = TopPlayers.Instance;
            this.dataRepository = new DataRepository();
            this.topPlayers.Load(this.dataRepository);
            this.player = new Player();
            this.StartNewGame();
            this.consoleRenderer = new ConsoleRenderer();

            while (this.isGameRunning)
            {
                this.ExecuteLoop();
            }
        }

        /// <summary>
        /// Method that controll all operations in the game loop
        /// </summary>
        protected virtual void ExecuteLoop()
        {
            try
            {
                this.UpdateConsole();
                this.ExecuteCommand(this.ReadCommand());
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key and try again.");
                Console.ReadKey();
            }
        }

        private void UpdateConsole()
        {
            Console.Clear();
            this.consoleRenderer.PrintStaticText("Welcome to \"Balloons Pops\" game. Please try to pop the balloons. Use 'top' to view the top scoreboard,'restart' to start a new game and 'exit' to quit the game.");
            this.consoleRenderer.PrintCurrentScore(this.player.CurrentMoves);
            this.consoleRenderer.RenderGameBoard(this.GameBoard);
        }

        /// <summary>
        /// Reads current command from the console
        /// </summary>
        /// <returns>Returns clear command</returns>
        private string ReadCommand()
        {
            Console.Write("Enter a row and column (space separated): ");
            string command = Console.ReadLine();
            return command.Trim();
        }

        /// <summary>
        /// Parse the command and calls the appropriate method
        /// </summary>
        /// <param name="command">Entered command</param>
        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "exit":
                    this.isGameRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;
                case "top":
                    this.consoleRenderer.PrintTopPlayers(this.topPlayers.PlayersMoves);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "restart":
                    this.StartNewGame();
                    break;
                default:
                    int[] cordinates = CoordinateParser.Parse(command);
                    this.Shoot(cordinates);
                    break;
            }
        }

        /// <summary>
        /// Checks if the game is over
        /// </summary>
        /// <returns>return the current stage of the game</returns>
        private bool IsGameOver()
        {
            var isGameOver = true;
            for (int row = 0; row < this.GameBoard.Entities.GetLength(0); row++)
            {
                for (int col = 0; col < this.GameBoard.Entities.GetLength(1); col++)
                {
                    if (this.GameBoard.Entities[row, col].Symbol != ".")
                    {
                        isGameOver = false;
                    }
                }
            }

            return isGameOver;
        }

        private void StartNewGame()
        {
            this.player.CurrentMoves = 0;
            var gameBoardArray = GameBoardGenerator.GenerateGameBoard(Config.GameBoardHeight, Config.GameBoardWidth, Config.MaxColorCount);
            this.GameBoard = new GameBoard(gameBoardArray);
            this.EntityPopper = new EntityPopper(this.GameBoard.Entities);
        }

        /// <summary>
        /// Shoot the baloon from the current coordinates and take them down :):D(:
        /// </summary>
        /// <param name="coordinates">entard coordinates</param>
        private void Shoot(int[] coordinates)
        {
            var gameBoard = this.GameBoard.Entities;
            if (gameBoard[coordinates[0], coordinates[1]].Symbol == ".")
            {
                throw new ApplicationException("Illegal move: cannot pop missing ballon!");
            }

            this.EntityPopper.PopUp(coordinates);
            this.EntityPopper.PopDown(coordinates);
            this.EntityPopper.PopLeft(coordinates);
            this.EntityPopper.PopRight(coordinates);
            this.EntityPopper.Pop(coordinates);
            this.GameBoard.Drop();
            this.player.CurrentMoves++;
            if (this.IsGameOver())
            {
                this.UpdateConsole();
                if (this.topPlayers.IsTopResult(this.player.CurrentMoves))
                {
                    this.SetPlayerName();
                    this.topPlayers.AddScore(this.player.Name, this.player.CurrentMoves);
                    this.dataRepository.Save(this.topPlayers.PlayersMoves, Config.TopPlayerFile);
                    this.consoleRenderer.PrintTopPlayers(this.topPlayers.PlayersMoves);
                    Console.WriteLine("Goodbye!");
                    Console.ReadKey();
                    this.isGameRunning = false;
                }
                else
                {
                    this.StartNewGame();
                }
            }
        }

        private void SetPlayerName()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Congratulations! You popped all baloons in {0} moves.", this.player.CurrentMoves);
                    Console.Write("Please enter your name for the top scoreboard: ");
                    var name = Console.ReadLine();
                    this.player.Name = name;
                    break;
                }
                catch (InvalidCommand e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
