using Microsoft.AspNetCore.Mvc;
using TestTask02Matveew.Domain;
using TestTask02Matveew.Interfaces;

namespace TestTask02Matveew.Controllers
{
    [Route("api/tictac")]
    [ApiController]
    public class TicTacController : ControllerBase
    {
        private readonly ITicTac _TicTac;
        public TicTacController(ITicTac TicTac)
        {
            _TicTac = TicTac;
        }

        ///<summary>Получение всех координат</summary>
        ///<returns>Список всех координат</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coordinates = await _TicTac.GetAllCoordinates();
            return Ok(coordinates);
        }

        /// <summary>
        /// Добавление координаты
        /// </summary>
        /// <param name="coordinate">Координата</param>
        /// <returns>Ответ</returns>///<summary
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Coordinate coordinate)
        {
            var response = await _TicTac.AddCoordinate(coordinate);
            return Ok(response);
        }
    }
}
