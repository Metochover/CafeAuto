using NewCafeAuto.DTO.CategoryDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.DTO.MenuDTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsAvailable { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int? CafeId { get; set; }
        public virtual ICollection<CategoryDTO.CategoryDTO> Category { get; set; }
    }
}
