using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Genero { get; set; }
        public string FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string FotoPerfil { get; set; }

        public Usuario(string nombre, string apellido, string cedula, string genero, string fechaNacimiento, string direccion, string fotoPerfil)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Genero = genero;
            FechaNacimiento = fechaNacimiento;
            Direccion = direccion;
            FotoPerfil = fotoPerfil;
        }
    }
}
