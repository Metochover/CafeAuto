﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NewCafeAuto.Models
{
    public partial class Cards
    {
        public int Id { get; set; }
        public bool? HasBillingCard { get; set; }
        public string BillingCardBrand { get; set; }
        public string BillingCardLast4 { get; set; }
        public int? BillingCardExpMonth { get; set; }
        public int? BillingCardExpYear { get; set; }
        public string TosAcceptedByIp { get; set; }
        public int? UserId { get; set; }

        public virtual Users User { get; set; }
    }
}