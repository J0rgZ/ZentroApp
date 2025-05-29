namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Elemento_Configuracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Elemento_Configuracion()
        {
            Miembro_Elemento = new HashSet<Miembro_Elemento>();
            Solicitud_Cambios = new HashSet<Solicitud_Cambios>();
        }

        [Key]
        public int Id_elementoconfiguracion { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Nomenclatura { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(20)]
        public string Version { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public int Id_fase { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public virtual Fase Fase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Elemento> Miembro_Elemento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios { get; set; }

        public List<Elemento_Configuracion> Listar()
        {
            var lista = new List<Elemento_Configuracion>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Elemento_Configuracion.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public Elemento_Configuracion Obtener(int id)
        {
            Elemento_Configuracion entidad = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    entidad = db.Elemento_Configuracion.FirstOrDefault(x => x.Id_elementoconfiguracion == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entidad;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_elementoconfiguracion > 0)
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

        public List<Elemento_Configuracion> Buscar(string criterio)
        {
            var resultados = new List<Elemento_Configuracion>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Elemento_Configuracion
                        .Where(x => x.Nombre.Contains(criterio) ||
                                    (x.Descripcion != null && x.Descripcion.Contains(criterio)) ||
                                    x.Codigo.Contains(criterio) ||
                                    (x.Tipo != null && x.Tipo.Contains(criterio)) ||
                                    (x.Version != null && x.Version.Contains(criterio)))
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
