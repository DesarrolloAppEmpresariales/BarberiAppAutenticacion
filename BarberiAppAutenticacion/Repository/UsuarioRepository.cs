using BarberiAppAutenticacion.Models;
using BarberiAppAutenticacion.Interface;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppAutenticacion.Repository
{
    public class UsuarioRepository : IUsuario
    {
        readonly DatabaseContext _dbContext = new();

        public UsuarioRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Usuario> ObtenerListaUsuarios()
        {
            try
            {
                return _dbContext.Usuario.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            try
            {
                Usuario? Usuario = _dbContext.Usuario.Find(id);
                if (Usuario != null)
                {
                    return Usuario;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void CrearUsuario(Usuario Usuario)
        {
            try
            {
                _dbContext.Usuario.Add(Usuario);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarUsuario(Usuario Usuario)
        {
            try
            {
                _dbContext.Entry(Usuario).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Usuario EliminarUsuario(int id)
        {
            try
            {
                Usuario? Usuario = _dbContext.Usuario.Find(id);

                if (Usuario != null)
                {
                    _dbContext.Usuario.Remove(Usuario);
                    _dbContext.SaveChanges();
                    return Usuario;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool ValidarUsuario(int id)
        {
            return _dbContext.Usuario.Any(e => e.UsuarioID == id);
        }
    }
}
