﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jwt.Data;
using Jwt.Models;

namespace Jwt.Controllers
{
    [Produces("application/json")]
    [Route("api/States")]
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/States
        [HttpGet]
        public IEnumerable<State> GetStates()
        {
            return _context.States;
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var state = await _context.States.SingleOrDefaultAsync(m => m.Id == id);

            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // PUT: api/States/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState([FromRoute] int id, [FromBody] State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != state.Id)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/States
        [HttpPost]
        public async Task<IActionResult> PostState([FromBody] State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetState", new { id = state.Id }, state);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var state = await _context.States.SingleOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return Ok(state);
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.Id == id);
        }
    }
}