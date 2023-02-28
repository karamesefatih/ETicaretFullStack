using Domain.Entities.Ortak;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        //ürün ve sipariş arasında ki çoka çok ilişki olduğu için hem ürün hem de siparişte ICollection tanımlanmıştır
        public ICollection<Order> Orders { get; set; }
    }
}
