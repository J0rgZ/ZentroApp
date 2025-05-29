namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Rol")]
    public partial class Rol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rol()
        {
            Miembro_Proyecto = new HashSet<Miembro_Proyecto>();
        }

        [Key]
        public int Id_rol { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(1000)]
        public string Permisos { get; set; }

        [Required]
        [StringLength(1)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Proyecto> Miembro_Proyecto { get; set; }

        // Listar todos los roles
        public List<Rol> Listar()
        {
            var lista = new List<Rol>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Rol
                              .Include("Miembro_Proyecto")
                              .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        // Obtener un rol por su ID
        public Rol Obtener(int id)
        {
            Rol rol = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    rol = db.Rol
                            .Include("Miembro_Proyecto")
                            .FirstOrDefault(x => x.Id_rol == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rol;
        }

        // Guardar o actualizar un rol
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_rol > 0)
                    {
                        db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = System.Data.Entity.EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Eliminar un rol
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

        // Buscar roles por nombre, descripción o permisos
        public List<Rol> Buscar(string criterio)
        {
            var resultados = new List<Rol>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Rol
                        .Include("Miembro_Proyecto")
                        .Where(x => x.Nombre.Contains(criterio)
                                 || x.Descripcion.Contains(criterio)
                                 || x.Permisos.Contains(criterio))
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultados;
        }
    }
}
