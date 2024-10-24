using BrokerAPI.Models.Views; // Importing models for data transfer objects
using BrokerAPI.Services; // Importing the service layer for business logic
using Microsoft.AspNetCore.Mvc; // Importing MVC components for API development
using System.Collections.Generic; // Importing collections for list handling
using System.Threading.Tasks; // Importing for asynchronous programming

namespace BrokerAPI.Controllers
{
    [ApiController] // Indicates that this class is an API controller
    [Route("api/[controller]")] // Defines the base route for the controller
    public class BrokersController : ControllerBase // Inheriting from ControllerBase for API behavior
    {
        private readonly IBrokerService _service; // Dependency injection for broker service

        // Constructor to initialize the broker service
        public BrokersController(IBrokerService service)
        {
            _service = service; // Assigning the injected service to the private field
        }

        // GET api/brokers
        [HttpGet] // Attribute for handling GET requests
        public async Task<ActionResult<List<BrokerDto>>> GetAll()
        {
            // Asynchronously retrieve all brokers from the service
            var brokers = await _service.GetAllBrokersAsync();
            return Ok(brokers); // Return the list of brokers with a 200 OK response
        }

        // GET api/brokers/{id}
        [HttpGet("{id}")] // Attribute for handling GET requests with an ID parameter
        public async Task<ActionResult<BrokerDto>> GetById(int id)
        {
            // Asynchronously retrieve a broker by its ID from the service
            var broker = await _service.GetBrokerByIdAsync(id);
            if (broker == null) return NotFound(); // Return 404 if the broker is not found
            return Ok(broker); // Return the broker with a 200 OK response
        }

        // POST api/brokers
        [HttpPost] // Attribute for handling POST requests
        public async Task<ActionResult<BrokerDto>> Create([FromBody] BrokerDto brokerDto)
        {
            // Asynchronously add a new broker using the provided DTO
            var createdBroker = await _service.AddBrokerAsync(brokerDto);
            // Return 201 Created with the location of the newly created broker
            return CreatedAtAction(nameof(GetById), new { id = createdBroker.BrokerId }, createdBroker);
        }

        // PUT api/brokers/{id}
        [HttpPut("{id}")] // Attribute for handling PUT requests with an ID parameter
        public async Task<ActionResult<BrokerDto>> Update(int id, [FromBody] BrokerDto brokerDto)
        {
            // Asynchronously update the broker's information
            var updatedBroker = await _service.UpdateBrokerAsync(id, brokerDto);
            if (updatedBroker == null) return NotFound(); // Return 404 if the broker is not found
            return Ok(updatedBroker); // Return the updated broker with a 200 OK response
        }

        // DELETE api/brokers/{id}
        [HttpDelete("{id}")] // Attribute for handling DELETE requests with an ID parameter
        public async Task<IActionResult> Delete(int id)
        {
            // Asynchronously delete the broker by its ID
            var result = await _service.DeleteBrokerAsync(id);
            if (!result) return NotFound(); // Return 404 if the broker is not found
            return NoContent(); // Return 204 No Content for a successful deletion
        }

        // POST api/brokers/login
        [HttpPost("login")] // Attribute for handling login requests
        public async Task<ActionResult<BrokerDto>> Login([FromBody] LoginDto loginDto)
        {
            // Asynchronously authenticate the broker using the provided login DTO
            var user = await _service.LoginAsync(loginDto);
            return user == null ? Unauthorized() : Ok(user); // Return 401 Unauthorized if failed, else 200 OK
        }
    }
}
