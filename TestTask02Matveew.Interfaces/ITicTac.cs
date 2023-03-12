using TestTask02Matveew.Domain;

namespace TestTask02Matveew.Interfaces
{
    public interface ITicTac
    {
        Task<string> AddCoordinate(Coordinate coordinate);
        Task<string> GetAllCoordinates();
    }
}
