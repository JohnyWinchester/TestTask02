using Microsoft.EntityFrameworkCore;
using System.Text;
using TestTask02Matveew.DAL.Context;
using TestTask02Matveew.Domain;
using TestTask02Matveew.Interfaces;

namespace TestTask02Matveew.Services
{
    public class SqlTicTac : ITicTac
    {
        private readonly TestTask02MatveewDB _db;
        public SqlTicTac(TestTask02MatveewDB db)
        {
            _db = db;
        }

        #region Add
        public async Task<string> AddCoordinate(Coordinate coordinate)
        {
            if (coordinate.CoordinateX > 3
               || coordinate.CoordinateY > 3
               || coordinate.CoordinateX < 1
               || coordinate.CoordinateY < 1) throw new IndexOutOfRangeException("Coordinates incorrectly");
 
            var allCoordinates = await _db.Coordinates.ToListAsync().ConfigureAwait(false);
            var existingCoordinate = allCoordinates.Any(x =>
                x.CoordinateX == coordinate.CoordinateX &&
                x.CoordinateY == coordinate.CoordinateY);

            //var existingCoordinate = allCoordinates.Where(x =>
            //    x.CoordinateX == coordinate.CoordinateX &&
            //    x.CoordinateY == coordinate.CoordinateY);

            if (existingCoordinate)
                return "This coordinate already exists";

            await _db.Coordinates.AddAsync(coordinate);
            await _db.SaveChangesAsync();

            var coordinates = await _db.Coordinates
                .Where(x => x.Client == coordinate.Client)
                .OrderBy(x => x.CoordinateX).ToListAsync();

            if (coordinates.Count > 2 && WinOrNot(coordinates))
            {
                //await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [TicTacDB]");
                return $"WIN USER - {coordinate.Client} !!!";
            }

            return "Next";
        }

        private bool WinOrNot(IEnumerable<Coordinate> coordinates)
        {
            for(int i = 1; i < 4; i++)
            {
                var coordinate = coordinates.Any(x => x.CoordinateX == i && x.CoordinateY == i);
                if (coordinate)
                {
                    var lineSecondCoordinate = coordinates.Any(x => x.CoordinateX == i && x.CoordinateY == 2);
                    var lineThirdCoordinate = coordinates.Any(x => x.CoordinateX == i && x.CoordinateY == 3);

                    if (lineSecondCoordinate
                        && lineThirdCoordinate) return true;
                }
            }

            for(int i = 1; i < 4; i++)
            {
                var coordinate = coordinates.Any(x => x.CoordinateX == i && x.CoordinateY == i);
                if (coordinate)
                {
                    var columnSecondCoordinate = coordinates.Any(x => x.CoordinateX == 2 && x.CoordinateY == i);
                    var columnThirdCoordinate = coordinates.Any(x => x.CoordinateX == 3 && x.CoordinateY == i);

                    if (columnSecondCoordinate
                        && columnThirdCoordinate) return true;
                }
            }

            var angleFirstCoordinate = coordinates.Any(x => x.CoordinateX == 1 && x.CoordinateY == 1);
            var angleSecondCoordinate = coordinates.Any(x => x.CoordinateX == 2 && x.CoordinateY == 2);
            var angleThirdCoordinate = coordinates.Any(x => x.CoordinateX == 3 && x.CoordinateY == 3);

            if (angleFirstCoordinate
                && angleSecondCoordinate
                && angleThirdCoordinate) return true;

            var angleFourCoordinate = coordinates.Any(x => x.CoordinateX == 1 && x.CoordinateY == 3);
            var angleFiveCoordinate = coordinates.Any(x => x.CoordinateX == 3 && x.CoordinateY == 1);

            if(angleFourCoordinate
               && angleSecondCoordinate
               && angleFiveCoordinate) return true;

            return false;
        }
        #endregion

        public async Task<string> GetAllCoordinates()
        {
            var coordinates = await _db.Coordinates.ToListAsync().ConfigureAwait(false);

            if (coordinates is not null)
            {
                StringBuilder coordinatesString = new StringBuilder();

                foreach (var coord in coordinates)
                {
                    coordinatesString.Append($"{coord.Client} - ");
                }
                return coordinatesString.ToString();
            }

            return "";
        }
    }
}
