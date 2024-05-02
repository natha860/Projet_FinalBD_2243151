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
using BD_Final_Project.ViewModel;

namespace BD_Final_Project.Controllers
{
    public class JoueursController : Controller
    {
        private readonly footballContext _context;

        public bool dechiffer;

        public JoueursController(footballContext context)
        {
            _context = context;
        }

        // GET: Joueurs
        public async Task<IActionResult> Index()
        {
            var footballContext = _context.Joueurs.Include(j => j.Equipe);
            return View(await footballContext.ToListAsync());
        }

        // GET: Joueurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            dechiffer = false;
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs
                .Include(j => j.Equipe)
                .FirstOrDefaultAsync(m => m.JoueurId == id);
            if (joueur == null)
            {
                return NotFound();
            }

            return View(joueur);
        }

        // GET: Joueurs/Create
        public IActionResult Create()
        {
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId");
            return View();
        }

        // POST: Joueurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JoueurId,Nom,DateNaissance,Position,Nationalite,EquipeId")] Joueur joueur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joueur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            return View(joueur);
        }

        // GET: Joueurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            return View(joueur);
        }

        // POST: Joueurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JoueurId,Nom,DateNaissance,Position,Nationalite,EquipeId")] Joueur joueur)
        {
            if (id != joueur.JoueurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joueur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoueurExists(joueur.JoueurId))
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
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            return View(joueur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNAS(int JoueurId, string Nas)
        {
            Joueur joueur = await _context.Joueurs.FindAsync(JoueurId);

            if (JoueurId != joueur.JoueurId || joueur == null)
            {
                return NotFound();
            }


          

            if (ModelState.IsValid)
            {
                try
                {
                    string query = "EXEC Equipes.USP_ChangeNasChiffrement @NAS, @JoueurId";
                    List<SqlParameter> parameters = new List<SqlParameter>
                     {
                         new SqlParameter{ParameterName = "@NAS", Value = Nas},
                         new SqlParameter{ParameterName = "@JoueurId", Value = JoueurId}

                          };
                    await _context.Database.ExecuteSqlRawAsync(query, parameters);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoueurExists(joueur.JoueurId))
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
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            return View("Edit",joueur);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> affiche(int JoueurId, string Nas)
        {
            Joueur joueur = await _context.Joueurs.FindAsync(JoueurId);

            if (JoueurId != joueur.JoueurId || joueur == null)
            {
                return NotFound();
            }




            string query = "EXEC Equipes.USP_DEChiffrement @NAS, @JoueurId, @AdminKey";
            List<SqlParameter> parameters = new List<SqlParameter>
                     {
                         new SqlParameter{ParameterName = "@NAS", Value = Nas},
                         new SqlParameter{ParameterName = "@JoueurId", Value = JoueurId},
                         new SqlParameter{ParameterName = "@AdminKey", Value = ""}
                          };
            JoueurRetour? joueurRetour = (await _context.JoueurRetours.FromSqlRaw(query, parameters.ToArray()).ToListAsync()).FirstOrDefault();
            
            JoueurDechiffrerVM joueurDechiffrerVM = new JoueurDechiffrerVM
            {
                joueurRetour = joueurRetour, Joueur = joueur

        };

            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);

            return View(joueurDechiffrerVM);
            
           
            
        }

        // GET: Joueurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs
                .Include(j => j.Equipe)
                .FirstOrDefaultAsync(m => m.JoueurId == id);
            if (joueur == null)
            {
                return NotFound();
            }

            return View(joueur);
        }

        // POST: Joueurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Joueurs == null)
            {
                return Problem("Entity set 'footballContext.Joueurs'  is null.");
            }
            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur != null)
            {
                _context.Joueurs.Remove(joueur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoueurExists(int id)
        {
          return (_context.Joueurs?.Any(e => e.JoueurId == id)).GetValueOrDefault();
        }
    }
}
