using System.Net;
using Newtonsoft.Json;

namespace BussinessCenter.reCaptcha
{
    /// <summary>
    /// Used for validation of Response data Sent by Google reCaptcha
    /// </summary>
    public class ReCaptcha:IReCaptcha
    {
        /// <summary>
        /// It validates the Response
        /// </summary>
        /// <param name="secretKey">The secretkey provided by Google reCaptcha</param>
        /// <param name="responseData">The responseData Which we get from Google reCaptcha</param>
        /// <returns>Returns string Message after validating the responseData with secretKey</returns>
        public string ReCaptchaValidation(string secretKey, string responseData)
        {
       
            string googleResponse = string.Empty;
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, responseData));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return googleResponse;
                var error = captchaResponse.ErrorCodes[0].ToLower();
                CaptchaErrorMessage errorMeaage=new CaptchaErrorMessage();
                googleResponse = errorMeaage.GetErrorMessage(error.ToString());
              }
            else
            {
                googleResponse = "true";
            }

            return googleResponse;
        }
    }
}