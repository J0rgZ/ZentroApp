namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Proyecto")]
    public partial class Proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proyecto()
        {
            Cronograma = new HashSet<Cronograma>();
            Miembro_Proyecto = new HashSet<Miembro_Proyecto>();
            Solicitud_Cambios = new HashSet<Solicitud_Cambios>();
        }

        [Key]
        public int Id_proyecto { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        [StringLength(500)]
        public string Github_url { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaTermino { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public decimal? Progreso { get; set; }

        public int Id_metodologia { get; set; }

        public int Id_solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cronograma> Cronograma { get; set; }

        public virtual Metodologia Metodologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Proyecto> Miembro_Proyecto { get; set; }

        public virtual Solicitud Solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios { get; set; }

        // Listar todos los proyectos
        public List<Proyecto> Listar()
        {
            var lista = new List<Proyecto>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Proyecto
                              .Include("Cronograma")
                              .Include("Miembro_Proyecto")
                              .Include("Solicitud_Cambios")
                              .Include("Metodologia")
                              .Include("Solicitud")
                              .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        // Obtener un proyecto por su ID
        public Proyecto Obtener(int id)
        {
            Proyecto proyecto = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    proyecto = db.Proyecto
                                 .Include("Cronograma")
                                 .Include("Miembro_Proyecto")
                                 .Include("Solicitud_Cambios")
                                 .Include("Metodologia")
                                 .Include("Solicitud")
                                 .FirstOrDefault(x => x.Id_proyecto == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return proyecto;
        }

        // Guardar o actualizar un proyecto
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_proyecto > 0)
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

        // Eliminar un proyecto
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

        // Buscar proyectos por código, nombre o descripción
        public List<Proyecto> Buscar(string criterio)
        {
            var resultados = new List<Proyecto>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Proyecto
                        .Include("Cronograma")
                        .Include("Miembro_Proyecto")
                        .Include("Solicitud_Cambios")
                        .Include("Metodologia")
                        .Include("Solicitud")
                        .Where(x => x.Codigo.Contains(criterio)
                                 || x.Nombre.Contains(criterio)
                                 || x.Descripcion.Contains(criterio))
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
