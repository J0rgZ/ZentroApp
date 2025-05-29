namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Metodologia")]
    public partial class Metodologia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Metodologia()
        {
            Fase = new HashSet<Fase>();
            Proyecto = new HashSet<Proyecto>();
        }

        [Key]
        public int Id_metodologia { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(1)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fase> Fase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }

        public List<Metodologia> Listar()
        {
            var lista = new List<Metodologia>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Metodologia.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public Metodologia Obtener(int id)
        {
            Metodologia entidad = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    entidad = db.Metodologia.FirstOrDefault(x => x.Id_metodologia == id);
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
                    if (this.Id_metodologia > 0)
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

        public List<Metodologia> Buscar(string criterio)
        {
            var resultados = new List<Metodologia>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Metodologia
                        .Where(x => x.Nombre.Contains(criterio) || (x.Descripcion != null && x.Descripcion.Contains(criterio)))
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
