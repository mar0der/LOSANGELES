namespace BalloonsPops.Utilities
{
    using System.Text.RegularExpressions;
    using BalloonsPops.Data;
    using BalloonsPops.Exceptions;

    public static class CoordinateParser
    {
        /// <summary>
        /// Checks and parse the coordinates
        /// </summary>
        /// <param name="stringCoordinates">Entered cooradinates from user</param>
        /// <returns>A valid coordinates</returns>
        public static int[] Parse(string stringCoordinates)
        {
            var coordinatesStringArray = Regex.Split(stringCoordinates, @"\s+");

            if (coordinatesStringArray.Length < 2)
            {
                throw new InvalidCommand("Invalid command.");
            }

            int row;
            int col;
            if (int.TryParse(coordinatesStringArray[0], out row) && int.TryParse(coordinatesStringArray[1], out col))
            {
                if (row < 0 || row > Config.GameBoardHeight - 1 || col < 0 || col > Config.GameBoardWidth - 1)
                {
                    throw new InvalidCoordinates("Invalid coordinates.");
                }

                return new int[] { row, col };
            }

            throw new InvalidCoordinates("Invalid coordinates.");
        }
    }
}
