﻿using H_Api.Data;
using H_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly AppDbContext _context;

    public GuestsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Guests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
    {
        return await _context.Guests
            .Include(g => g.Reservations)
            .ToListAsync();
    }

    // GET: api/Guests/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Guest>> GetGuest(int id)
    {
        var guest = await _context.Guests
            .Include(g => g.Reservations)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (guest == null)
        {
            return NotFound();
        }
        return guest;
    }

    // POST: api/Guests
    [HttpPost]
    public async Task<ActionResult<Guest>> PostGuest(Guest guest)
    {
        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGuest", new { id = guest.Id }, guest);
    }

    // PUT: api/Guests/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGuest(int id, Guest guest)
    {
        if (id != guest.Id)
        {
            return BadRequest();
        }

        _context.Entry(guest).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GuestExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    // DELETE: api/Guests/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuest(int id)
    {
        var guest = await _context.Guests.FindAsync(id);
        if (guest == null)
        {
            return NotFound();
        }

        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GuestExists(int id)
    {
        return _context.Guests.Any(e => e.Id == id);
    }
}