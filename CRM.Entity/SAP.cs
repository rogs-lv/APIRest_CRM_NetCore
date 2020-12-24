using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entity
{
    public class SAP
    {
        /// <summary>
        /// Properties to entity SAP
        /// </summary>
        #region Properties SAP
        public string Server        { get; set; }
        public string DbServerType  { get; set; }
        public string SLDServer     { get; set; }
        public string CompanyDB     { get; set; }
        public string UserName      { get; set; }
        public string Password      { get; set; }
        public string DbUserName    { get; set; }
        public string DbPassword    { get; set; }
        public string language      { get; set; }
        #endregion
        /// <summary>
        /// Constructor class
        /// </summary>
        public SAP() { 
        }
        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="_server">IP/Name Server</param>
        /// <param name="_dbServerType">Server type</param>
        /// <param name="_sldServer">License server</param>
        /// <param name="_companyDB">Company SAP</param>
        /// <param name="_userName">User SAP</param>
        /// <param name="_password">Password SAP</param>
        /// <param name="_dbUserName">User Database</param>
        /// <param name="_dbPassword">Password Database</param>
        /// <param name="_languaje">Languaje</param>
        public SAP(string _server, string _dbServerType, string _sldServer, string _companyDB, string _userName, string _password, string _dbUserName, string _dbPassword, string _languaje) {
            this.Server         = _server;
            this.DbServerType   = _dbServerType;
            this.SLDServer      = _sldServer;
            this.CompanyDB      = _companyDB;
            this.UserName       = _userName;
            this.Password       = _password;
            this.DbUserName     = _dbUserName;
            this.DbPassword     = _dbPassword;
            this.language       = _languaje;
        }
    }
}
