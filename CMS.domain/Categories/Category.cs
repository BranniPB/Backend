using CMS.domain.Common;

namespace CMS.domain.Categories
{
    public class Category : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaDeCreacion { get; set; }

        public Category()
        {
            FechaDeCreacion = DateTime.Now;
        }
    }
}
