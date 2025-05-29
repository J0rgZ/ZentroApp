namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Fase")]
    public partial class Fase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fase()
        {
            Elemento_Configuracion = new HashSet<Elemento_Configuracion>();
        }

        [Key]
        public int Id_fase { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public int Id_metodologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Elemento_Configuracion> Elemento_Configuracion { get; set; }

        public virtual Metodologia Metodologia { get; set; }

        public List<Fase> Listar()
        {
            var lista = new List<Fase>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Fase.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public Fase Obtener(int id)
        {
            Fase entidad = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    entidad = db.Fase.FirstOrDefault(x => x.Id_fase == id);
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
                    if (this.Id_fase > 0)
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

        public List<Fase> Buscar(string criterio)
        {
            var resultados = new List<Fase>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Fase
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
