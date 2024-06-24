using Altivix.Services;
using Altivix.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Altivix.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
       private readonly ClientServices _services;
        public ClientController(ClientServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> GetClients()
        {
            var clients = await _services.GetClientsAsync();
            return Json(clients); // Assuming you want JSON data for clients
        }
        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var client = await _services.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber")] AppUser updatedClient)
        {
            if (id != updatedClient.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _services.UpdateClientAsync(updatedClient);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to a suitable page
                }
                else
                {
                    return NotFound();
                }
            }
            return View(updatedClient);
        }
    }
}
