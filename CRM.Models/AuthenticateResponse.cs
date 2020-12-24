using CRM.Entity;

namespace CRM.Models
{
    /// <summary>
    /// Represent the data for response login
    /// </summary>
    public class AuthenticateResponse
    {
        //public int      Id          { get; set; }
        //public string   FirstName   { get; set; }
        //public string   LastName    { get; set; }
        //public string   UserName    { get; set; }
        public string   Token       { get; set; }
        public AuthenticateResponse() { }
        public AuthenticateResponse(string token, User user = null) {
            this.Token = token;
        }
    }
}
