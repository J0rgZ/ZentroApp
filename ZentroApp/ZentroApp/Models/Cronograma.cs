namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cronograma")]
    public partial class Cronograma
    {
        [Key]
        public int Id_cronograma { get; set; }

        public int Id_proyecto { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? Fechafin { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        [Required]
        [StringLength(10)]
        public string Prioridad { get; set; }

        public decimal? Progreso { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public List<Cronograma> Listar()
        {
            var lista = new List<Cronograma>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Cronograma.Include("Proyecto").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public Cronograma Obtener(int id)
        {
            Cronograma entidad = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    entidad = db.Cronograma.Include("Proyecto")
                        .FirstOrDefault(x => x.Id_cronograma == id);
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
                    if (this.Id_cronograma > 0)
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

        public List<Cronograma> Buscar(string criterio)
        {
            var resultados = new List<Cronograma>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Cronograma
                        .Where(x => x.Nombre.Contains(criterio) ||
                                    (x.Descripcion != null && x.Descripcion.Contains(criterio)) ||
                                    x.Estado.Contains(criterio) ||
                                    x.Prioridad.Contains(criterio))
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
