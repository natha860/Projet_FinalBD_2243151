using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BD_Final_Project.Data;
using BD_Final_Project.Models;
using Microsoft.Data.SqlClient;

namespace BD_Final_Project.Controllers
{
    public class EquipesController : Controller
    {
        private readonly footballContext _context;

        public EquipesController(footballContext context)
        {
            _context = context;
        }

        // GET: Equipes
        public async Task<IActionResult> AncienIndex(int id)
        {
            Equipe equipe = await _context.Equipes.FindAsync(id);
            if(equipe == null) { return NotFound(); }
            string query = "EXEC Equipes.usp_stadeAppartennance @nomEquipe";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@nomEquipe", Value = equipe.Nom}

            };
            List<VwGestionDesEquipe> footballVW = await _context.VwGestionDesEquipes.FromSqlRaw(query, parameters.ToArray()).ToListAsync();
            return View(footballVW);
        }

        // GET: Equipes
        public async Task<IActionResult> Index()
        {
            var footballContext = _context.Equipes.Include(e => e.Championnat);
            return View(await footballContext.ToListAsync());
        }

        // GET: Equipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equipes == null)
            {
                return NotFound();
            }

            var equipe = await _context.Equipes
                .Include(e => e.Championnat)
                .FirstOrDefaultAsync(m => m.EquipeId == id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // GET: Equipes/Create
        public IActionResult Create()
        {
            ViewData["ChampionnatId"] = new SelectList(_context.Championnats, "ChampionnatId", "ChampionnatId");
            return View();
        }

        // POST: Equipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,AnneeDeFondation,Ville,ChampionnatId")] Equipe equipe)
        {

            if (ModelState.IsValid)
            {
                _context.Add(equipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChampionnatId"] = new SelectList(_context.Championnats, "ChampionnatId", "ChampionnatId", equipe.ChampionnatId);
            return View(equipe);
        }

        // GET: Equipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equipes == null)
            {
                return NotFound();
            }

            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe == null)
            {
                return NotFound();
            }
            ViewData["ChampionnatId"] = new SelectList(_context.Championnats, "ChampionnatId", "ChampionnatId", equipe.ChampionnatId);
            return View(equipe);
        }

        // POST: Equipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipeId,Nom,AnneeDeFondation,Ville,ChampionnatId")] Equipe equipe)
        {
            if (id != equipe.EquipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipeExists(equipe.EquipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChampionnatId"] = new SelectList(_context.Championnats, "ChampionnatId", "ChampionnatId", equipe.ChampionnatId);
            return View(equipe);
        }

        // GET: Equipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equipes == null)
            {
                return NotFound();
            }

            var equipe = await _context.Equipes
                .Include(e => e.Championnat)
                .FirstOrDefaultAsync(m => m.EquipeId == id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equipes == null)
            {
                return Problem("Entity set 'footballContext.Equipes'  is null.");
            }
            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe != null)
            {
                _context.Equipes.Remove(equipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipeExists(int id)
        {
          return (_context.Equipes?.Any(e => e.EquipeId == id)).GetValueOrDefault();
        }
    }
}
