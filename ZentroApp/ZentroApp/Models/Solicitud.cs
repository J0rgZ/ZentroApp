namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Solicitud")]
    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            Proyecto = new HashSet<Proyecto>();
            Solicitud_Cambios = new HashSet<Solicitud_Cambios>();
        }

        [Key]
        public int Id_solicitud { get; set; }

        [Required]
        [StringLength(255)]
        public string Requerimiento { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public int Id_usuario_solicitante { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios { get; set; }

        public virtual Usuario Usuario { get; set; }

        // Listar todas las solicitudes
        public List<Solicitud> Listar()
        {
            var lista = new List<Solicitud>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Solicitud
                              .Include("Usuario")
                              .Include("Proyecto")
                              .Include("Solicitud_Cambios")
                              .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        // Obtener solicitud por ID
        public Solicitud Obtener(int id)
        {
            Solicitud solicitud = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    solicitud = db.Solicitud
                                  .Include("Usuario")
                                  .Include("Proyecto")
                                  .Include("Solicitud_Cambios")
                                  .FirstOrDefault(x => x.Id_solicitud == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return solicitud;
        }

        // Guardar (crear o modificar) solicitud
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_solicitud > 0)
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

        // Eliminar solicitud
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

        // Buscar solicitudes por requerimiento, estado o nombre del solicitante
        public List<Solicitud> Buscar(string criterio)
        {
            var resultados = new List<Solicitud>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Solicitud
                        .Include("Usuario")
                        .Include("Proyecto")
                        .Include("Solicitud_Cambios")
                        .Where(x => x.Requerimiento.Contains(criterio)
                                 || x.Estado.Contains(criterio)
                                 || x.Usuario.Nombre.Contains(criterio)
                                 || x.Usuario.Apellido.Contains(criterio))
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
