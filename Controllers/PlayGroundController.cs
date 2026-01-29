using Microsoft.AspNetCore.Mvc;
using PlayGroundLib;

namespace PlayGroundLibController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayGroundController : ControllerBase
    {
        private readonly PlayGroundRepository _repo;

        public PlayGroundController(PlayGroundRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PlayGround>> Get()
        {
            if (_repo == null)
            {
                return NotFound();
            }
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlayGround> GetById(int id)
        {
            var playGround = _repo.GetById(id);
            if (playGround == null)
            {
                return NotFound();
            }
            return Ok(playGround);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<PlayGround> Post([FromBody] PlayGround playGround)
        {
            if (playGround == null)
            {
                return BadRequest();
            }
            var createdPlayGround = _repo.Add(playGround);
            return CreatedAtAction(nameof(Get), new { id = createdPlayGround.Id }, createdPlayGround);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlayGround> Put(int id, [FromBody] PlayGround playGround)
        {
            if (playGround == null || id != playGround.Id)
            {
                return BadRequest("Id not right");
            }
            var updatedPlayGround = _repo.Update(playGround);

            if (updatedPlayGround == null)
            {
                return NotFound();
            }
            return Ok(updatedPlayGround);
        }




    }
}