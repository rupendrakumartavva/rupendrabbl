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
    public partial class UserPasswordTracking
    {
        [Key]
        public string PasswordTrackId { get; set; }
        public string UserId { get; set; }
        public string PasswordTrack { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
