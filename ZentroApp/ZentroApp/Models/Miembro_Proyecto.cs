namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Miembro_Proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Miembro_Proyecto()
        {
            Miembro_Elemento = new HashSet<Miembro_Elemento>();
        }

        [Key]
        public int Id_miembro { get; set; }

        public int Id_usuario { get; set; }

        public int Id_rol { get; set; }

        public int Id_proyecto { get; set; }

        public DateTime Fecha_asignacion { get; set; }

        public DateTime? Fecha_desasignacion { get; set; }

        [Required]
        [StringLength(1)]
        public string Estado { get; set; }

        [StringLength(1000)]
        public string Responsabilidades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Elemento> Miembro_Elemento { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public virtual Rol Rol { get; set; }

        public virtual Usuario Usuario { get; set; }

        // Listar todos los miembros del proyecto
        public List<Miembro_Proyecto> Listar()
        {
            var lista = new List<Miembro_Proyecto>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Miembro_Proyecto
                              .Include("Usuario")
                              .Include("Rol")
                              .Include("Proyecto")
                              .Include("Miembro_Elemento")
                              .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        // Obtener un miembro por ID
        public Miembro_Proyecto Obtener(int id)
        {
            Miembro_Proyecto miembro = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    miembro = db.Miembro_Proyecto
                                .Include("Usuario")
                                .Include("Rol")
                                .Include("Proyecto")
                                .Include("Miembro_Elemento")
                                .FirstOrDefault(x => x.Id_miembro == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return miembro;
        }

        // Guardar o actualizar un miembro del proyecto
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_miembro > 0)
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

        // Eliminar un miembro del proyecto
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

        // Buscar miembros por nombre de usuario o proyecto
        public List<Miembro_Proyecto> Buscar(string criterio)
        {
            var resultados = new List<Miembro_Proyecto>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Miembro_Proyecto
                        .Include("Usuario")
                        .Include("Rol")
                        .Include("Proyecto")
                        .Include("Miembro_Elemento")
                        .Where(x => x.Usuario.Nombre.Contains(criterio)
                                 || x.Proyecto.Nombre.Contains(criterio))
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
