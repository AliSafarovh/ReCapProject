using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public class Entity<TId>:IEntityTimestamps
    {
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public Entity() //Parametresiz konstruktor: Varsayılan olaraq Id-nin dəyərini təyin edir.
        {
            Id = default;
        }

        public Entity(TId id) //İdentifikatoru (id) təyin etmək üçün istifadə olunur.
        {
            Id=id;
        }

    }
}
