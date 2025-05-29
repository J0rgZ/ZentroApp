namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Miembro_Elemento
    {
        [Key]
        public int Id_miembroelemento { get; set; }

        public int Id_miembro { get; set; }

        public int Id_elementoconfiguracion { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fechafin { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public decimal? Progreso { get; set; }

        [StringLength(1000)]
        public string Observaciones { get; set; }

        public decimal? Horas_estimadas { get; set; }

        public decimal? Horas_trabajadas { get; set; }

        public virtual Elemento_Configuracion Elemento_Configuracion { get; set; }

        public virtual Miembro_Proyecto Miembro_Proyecto { get; set; }

        // Listar todos los registros de Miembro_Elemento
        public List<Miembro_Elemento> Listar()
        {
            var lista = new List<Miembro_Elemento>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    lista = db.Miembro_Elemento
                              .Include("Elemento_Configuracion")
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

        // Obtener un registro por ID
        public Miembro_Elemento Obtener(int id)
        {
            Miembro_Elemento entidad = null;
            try
            {
                using (var db = new ModeloGestion())
                {
                    entidad = db.Miembro_Elemento
                                .Include("Elemento_Configuracion")
                                .Include("Miembro_Proyecto")
                                .FirstOrDefault(x => x.Id_miembroelemento == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entidad;
        }

        // Guardar o actualizar un registro
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloGestion())
                {
                    if (this.Id_miembroelemento > 0)
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

        // Eliminar un registro
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

        // Buscar por nombre del elemento o por nombre del usuario asignado
        public List<Miembro_Elemento> Buscar(string criterio)
        {
            var resultados = new List<Miembro_Elemento>();
            try
            {
                using (var db = new ModeloGestion())
                {
                    resultados = db.Miembro_Elemento
                        .Include("Elemento_Configuracion")
                        .Include("Miembro_Proyecto.Usuario")
                        .Where(x => x.Elemento_Configuracion.Nombre.Contains(criterio)
                                 || x.Miembro_Proyecto.Usuario.Nombre.Contains(criterio))
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
