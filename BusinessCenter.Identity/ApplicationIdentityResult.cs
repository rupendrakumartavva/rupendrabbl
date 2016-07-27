using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessCenter.Identity
{
   public  class ApplicationIdentityResult
    {
       public ApplicationIdentityResult(IEnumerable<string> errors, bool succeeded)
        {
            Succeeded = succeeded;
            Errors = errors;
          
        }

        public IEnumerable<string> Errors
        {
            get;
            private set;
        }

        public bool Succeeded
        {
            get;
            private set;
        }
       
    }

   public class CaptchaResponse
   {
       [JsonProperty("success")]
       public bool Success { get; set; }

       [JsonProperty("error-codes")]
       public List<string> ErrorCodes { get; set; }

   }
}
