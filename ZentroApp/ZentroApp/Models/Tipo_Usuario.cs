namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Tipo_Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Usuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int Id_tipousuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        // Metodos

        // Listar todos los tipos de usuario
        public List<Tipo_Usuario> Listar()
        {
            var tipos = new List<Tipo_Usuario>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    tipos = db.Tipo_Usuario.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipos;
        }

        // Obtener un tipo de usuario por ID
        public Tipo_Usuario Obtener(int id)
        {
            Tipo_Usuario tipo = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    tipo = db.Tipo_Usuario.FirstOrDefault(x => x.Id_tipousuario == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipo;
        }

        // Agregar un nuevo tipo de usuario
        public void Agregar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    db.Tipo_Usuario.Add(this);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Editar un tipo de usuario existente
        public void Editar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Eliminar este tipo de usuario
        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}