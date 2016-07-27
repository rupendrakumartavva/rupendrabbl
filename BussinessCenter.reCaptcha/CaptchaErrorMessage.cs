namespace BussinessCenter.reCaptcha
{
    /// <summary>
    /// This class is used Trace Captcha Errors
    /// </summary>
    public class CaptchaErrorMessage
    {
        /// <summary>
        /// It gives the Exact Error Message For any given Error Type
        /// </summary>
        /// <param name="errorType">Takes Error Type as a parameter</param>
        /// <returns>Returns Error Type's Message as String</returns>
        public string GetErrorMessage(string errorType)
        {
            string googleResponse = string.Empty;
            switch (errorType)
            {
                case ("missing-input-secret"):
                    googleResponse = "The secret parameter is missing.";
                    break;
                case ("invalid-input-secret"):
                    googleResponse = "The secret parameter is invalid or malformed.";
                    break;

                case ("missing-input-response"):
                    googleResponse = "The response parameter is missing.";
                    break;
                case ("invalid-input-response"):
                    googleResponse = "The response parameter is invalid or malformed.";
                    break;

                default:
                    googleResponse = "Error occured. Please try again";
                    break;
            }
            return googleResponse;
        }
    }
}