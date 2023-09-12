using NewCafeAuto.Models;

namespace NewCafeAuto.DTO.CategoryDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public int? SubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int? MenuId { get; set; }
        public List<CategoryDTO> SubCategory { get; set; }
        public virtual ICollection<ProductDTO.ProductDTO> Product { get; set; }
    }
}
