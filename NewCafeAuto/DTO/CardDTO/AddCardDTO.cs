namespace NewCafeAuto.DTO.CardDTO
{
    public class AddCardDTO
    {
        public bool? HasBillingCard { get; set; }
        public string BillingCardBrand { get; set; }
        public string BillingCardLast4 { get; set; }
        public int? BillingCardExpMonth { get; set; }
        public int? BillingCardExpYear { get; set; }
        public string TosAcceptedByIp { get; set; }
        public int? UserId { get; set; }
    }
}
