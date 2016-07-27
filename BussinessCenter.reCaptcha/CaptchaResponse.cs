using System.Collections.Generic;
using Newtonsoft.Json;


namespace BussinessCenter.reCaptcha
{
    /// <summary>
    /// Used for Holding Response Objects
    /// </summary>
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

    }
}