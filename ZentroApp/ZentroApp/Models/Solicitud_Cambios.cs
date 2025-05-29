namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Solicitud_Cambios
    {
        [Key]
        public int Id_solicitud_cambios { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(500)]
        public string Objetivo { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        [StringLength(1000)]
        public string Justificacion { get; set; }

        [StringLength(500)]
        public string Impacto { get; set; }

        [StringLength(500)]
        public string Respuesta { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        [Required]
        [StringLength(10)]
        public string Prioridad { get; set; }

        public int Id_solicitud { get; set; }

        public int Id_proyecto { get; set; }

        public int? Id_elemento_configuracion { get; set; }

        public int Id_usuario_solicitante { get; set; }

        public int? Id_usuario_aprobador { get; set; }

        public DateTime? Fecha_aprobacion { get; set; }

        public virtual Elemento_Configuracion Elemento_Configuracion { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public virtual Solicitud Solicitud { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }

        // Listar todas las solicitudes de cambios
        public List<Solicitud_Cambios> Listar()
        {
            var solicitudes = new List<Solicitud_Cambios>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    solicitudes = db.Solicitud_Cambios
                                    .Include("Elemento_Configuracion")
                                    .Include("Proyecto")
                                    .Include("Solicitud")
                                    .Include("Usuario")
                                    .Include("Usuario1")
                                    .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return solicitudes;
        }

        // Obtener solicitud de cambio por ID
        public Solicitud_Cambios Obtener(int id)
        {
            Solicitud_Cambios solicitud = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    solicitud = db.Solicitud_Cambios
                                  .Include("Elemento_Configuracion")
                                  .Include("Proyecto")
                                  .Include("Solicitud")
                                  .Include("Usuario")
                                  .Include("Usuario1")
                                  .FirstOrDefault(x => x.Id_solicitud_cambios == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return solicitud;
        }

        // Guardar (crear o editar) solicitud de cambio
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_solicitud_cambios > 0)
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

        // Eliminar una solicitud de cambio
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
