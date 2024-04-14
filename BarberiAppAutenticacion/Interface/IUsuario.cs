using BarberiAppAutenticacion.Models;

namespace BarberiAppAutenticacion.Interface
{
    public interface IUsuario
    {
        public List<Usuario> ObtenerListaUsuarios();
        public Usuario ObtenerUsuarioPorId(int id);
        public void CrearUsuario(Usuario usuario);
        public void ActualizarUsuario(Usuario usuario);
        public Usuario EliminarUsuario(int id);
        public bool ValidarUsuario(int id);
    }
}
