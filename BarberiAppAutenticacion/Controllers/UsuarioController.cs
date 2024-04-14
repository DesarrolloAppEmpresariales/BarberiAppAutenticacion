using BarberiAppAutenticacion.Interface;
using BarberiAppAutenticacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BarberiAppAutenticacion.Controllers
{
    [Authorize]
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuario _IUsuario;

        public UsuarioController(IUsuario IUsuario)
        {
            _IUsuario = IUsuario;
        }

        //Roles (1 'SU') (2 'Admin') (3 'Barbero') (4 'Cliente')    
        // GET: CitaController
        [HttpGet]
        [Authorize(Roles = "1,2,3")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await Task.FromResult(_IUsuario.ObtenerListaUsuarios());
        }

        // GET: CitaController/Details/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var cita = await Task.FromResult(_IUsuario.ObtenerUsuarioPorId(id));
            if (cita == null)
            {
                return NotFound();
            }
            return cita;
        }

        // POST: CitaController/Create
        [HttpPost]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            _IUsuario.CrearUsuario(usuario);
            return await Task.FromResult(usuario);
        }

        // POST: CitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitaController/Edit/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Usuario>> Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return BadRequest();
            }
            try
            {
                _IUsuario.ActualizarUsuario(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(usuario);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var cita = _IUsuario.EliminarUsuario(id);
            return await Task.FromResult(cita);
        }

        private bool CitaExists(int id)
        {
            return _IUsuario.ValidarUsuario(id);
        }
    }
}
