namespace BussinessCenter.reCaptcha
{
    /// <summary>
    /// Used to Validate Recaptcha
    /// </summary>
    public interface IReCaptcha
    {
         string ReCaptchaValidation(string secretKey, string responseData);
    }
}