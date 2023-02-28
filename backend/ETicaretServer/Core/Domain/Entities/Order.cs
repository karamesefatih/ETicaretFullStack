using Domain.Entities.Ortak;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }

        //ürün ve sipariş arasında ki çoka çok ilişki olduğu için hem ürün hem de siparişte ICollection tanımlanmıştır

        public ICollection<Product> Products { get; set; }

        //siparişin sadece bir customerı olacağı için tekil isimlendirilmiştir
        public Customer Customer { get; set; }
    }
}
