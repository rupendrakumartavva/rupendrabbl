//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessCenter.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class DCBC_ENTITY_MultiColumn_LOOKUP_INDEX
    {
        [Key, Column(Order = 1)]
        public string DATA_SOURCE { get; set; }
        [Key, Column(Order = 2)]
        public int DCBC_ENTITY_ID { get; set; }
        [Key, Column(Order = 3)]
        public string LicenseNumberOrig { get; set; }
        [Key, Column(Order = 4)]
        public string LicenseNumberLookup { get; set; }
        [Key, Column(Order = 5)]
        public string CompanyNameOrig { get; set; }
        [Key, Column(Order = 6)]
        public string CompanyNameLookup { get; set; }
        [Key, Column(Order = 7)]
        public string FirstNameOrig { get; set; }
        [Key, Column(Order = 8)]
        public string FirstNameLookup { get; set; }
        [Key, Column(Order = 9)]
        public string LastNameOrig { get; set; }
        [Key, Column(Order = 10)]
        public string LastNameLookup { get; set; }
    }
}
