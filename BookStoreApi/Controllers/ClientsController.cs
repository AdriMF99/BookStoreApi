using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService) =>
            _clientService = clientService;

        [HttpGet]
        public async Task<List<Client>> Get() =>
             await _clientService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            var client = await _clientService.GetAsync(id);

            if (client == null)
            {
                return NotFound();
            } else
            {
                //Console.WriteLine("Adrian");
                return client;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client newClient)
        {
            await _clientService.CreateAsync(newClient);

            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Client updatedClient)
        {
            var client = await _clientService.GetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            updatedClient.Id = client.Id;

            await _clientService.UpdateAsync(id, updatedClient);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _clientService.GetAsync(id);

            if (client == null) { return NotFound(); }

            await _clientService.RemoveAsync(id);

            return NoContent();
        }
    }
}
