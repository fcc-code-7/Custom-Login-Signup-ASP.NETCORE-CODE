using Altivix.Entities;
using Altivix.Web.Data;
using Altivix.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altivix.Services
{
    public class ClientServices
    {
       private readonly ApplicationDbContext _context;
        public ClientServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AppUser>> GetClientsAsync()
        {
            var clientRoleId = await _context.Roles
                .Where(r => r.Name == "Client")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            var clients = await _context.Users
                .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == clientRoleId))
                .ToListAsync();

            return clients;
        }
        public async Task<AppUser> GetClientByIdAsync(string clientId)
        {
            return await _context.Users.FindAsync(clientId);
        }

        public async Task<bool> UpdateClientAsync(AppUser updatedClient)
        {
            var existingClient = await _context.Users.FindAsync(updatedClient.Id);
            if (existingClient == null)
            {
                return false;
            }

            // Update client details
            existingClient.UserName = updatedClient.UserName;
            existingClient.Email = updatedClient.Email;
            existingClient.PhoneNumber = updatedClient.PhoneNumber;
            // Add any other properties that need to be updated

            _context.Users.Update(existingClient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
