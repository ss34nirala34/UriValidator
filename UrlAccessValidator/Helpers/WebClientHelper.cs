using System.Diagnostics;
using System.Net;
using UrlAccessValidator.BO;

namespace UrlAccessValidator.Helpers
{
    public static class WebClientHelper
    {
        /// <summary>
        /// This method will check a url to see that it does not return server or protocol errors
        /// </summary>
        /// <param name="url">The path to check</param>
        /// <returns></returns>
        public static ValidationResponseBO ValidateUrl(string url)
        {
            ValidationResponseBO status = new ValidationResponseBO() { IsSuccess = true, Message = "PASS", StatusCode = HttpStatusCode.OK };
            try
            {
                HttpWebRequest? request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        status.IsSuccess = true;
                        status.StatusCode = response.StatusCode;
                        status.Message = "PASS";
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        //log.Warn(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        Debug.WriteLine(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        status.IsSuccess = false;
                        status.StatusCode = response.StatusCode;
                        status.Message = "FAIL";
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    status.IsSuccess = false;
                    status.StatusCode = 0; // HttpStatusCode.ProxyAuthenticationRequired;
                    status.Message = $"ProtocolError: {ex.Message}";
                }
                else
                {
                    status.IsSuccess = false;
                    status.StatusCode = 0;
                    status.Message = $"Unhandled Web Exception: {ex.Message}";
                    //log.Warn(String.Format("Unhandled status [{0}] returned for url: {1}", ex.Status, url), ex);
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.StatusCode = 0;
                status.Message = $"Exception: {ex.Message}";
                //log.Error(String.Format("Could not test url {0}.", url), ex);
            }
            return status;
        }
    }
}
