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
    public partial class DCBC_ENTITY_LOOKUP_INDEX
    {
        [Key]
        public string DATA_SOURCE { get; set; }
        [Key]
        public string LOOKUP_TYPE { get; set; }
        [Key]
        public int DCBC_ENTITY_ID { get; set; }
        [Key]
        public string LOOKUP_VALUE { get; set; }
        [Key]
        public string SEARCH_STRING { get; set; }
    }
}
