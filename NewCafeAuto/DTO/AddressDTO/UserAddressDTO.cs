namespace NewCafeAuto.DTO.AddressDTO
{
    public class UserAddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
    }
}
