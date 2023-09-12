namespace NewCafeAuto.DTO.AddressDTO
{
    public class AddAddressDTO
    {
        public string Street { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? UserId { get; set; }

    }
}
