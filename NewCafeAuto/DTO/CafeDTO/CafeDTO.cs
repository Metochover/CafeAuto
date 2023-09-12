namespace NewCafeAuto.DTO.CafeDTO
{
    public class CafeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? CompanyId { get; set; }
        public int? MenuId { get; set; }
    }
}
