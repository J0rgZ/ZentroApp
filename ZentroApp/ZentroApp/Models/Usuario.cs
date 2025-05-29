namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        // Constantes para los estados de usuario
        public const string ESTADO_ACTIVO = "A";
        public const string ESTADO_INACTIVO = "I";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Miembro_Proyecto = new HashSet<Miembro_Proyecto>();
            Solicitud = new HashSet<Solicitud>();
            Solicitud_Cambios = new HashSet<Solicitud_Cambios>();
            Solicitud_Cambios1 = new HashSet<Solicitud_Cambios>();
        }

        [Key]
        public int Id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(150)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(1)]
        public string Estado { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public int Id_tipousuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Proyecto> Miembro_Proyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios1 { get; set; }

        public virtual Tipo_Usuario Tipo_Usuario { get; set; }

        //Metodos

        ModeloGestion db = new ModeloGestion();

        // Listar todos los usuarios con sus tipos
        public List<Usuario> Listar()
        {
            var usuarios = new List<Usuario>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    usuarios = db.Usuario.Include("Tipo_Usuario").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuarios;
        }

        // Obtener un usuario por ID
        public Usuario Obtener(int id)
        {
            Usuario usuario = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    usuario = db.Usuario.Include("Tipo_Usuario")
                                        .FirstOrDefault(x => x.Id_usuario == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }

        // Guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_usuario > 0)
                    {
                        db.Entry(this).State = EntityState.Modified; //existe
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added; //nuevo registro
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Eliminar este usuario
        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Autenticar por correo y contraseña
        public bool Autenticar()
        {
            try
            {
                return db.Usuario.Any(x => x.Correo == this.Correo && x.Password == this.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método mejorado para obtener usuario por credenciales
        public Usuario ObtenerUsuarioPorCredenciales(string username, string password)
        {
            Usuario usuario = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    // Trim para eliminar espacios en blanco
                    username = username?.Trim();
                    password = password?.Trim();

                    usuario = db.Usuario
                                .Include("Tipo_Usuario")
                                .FirstOrDefault(x => x.Username.Trim() == username &&
                                                   x.Password.Trim() == password);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }

        // Método auxiliar para verificar si el usuario está activo
        public bool EstaActivo()
        {
            return this.Estado == ESTADO_ACTIVO;
        }
    }
}
