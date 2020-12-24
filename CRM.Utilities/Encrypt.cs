using CRM.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Helpers
{
    public class Encrypt
    {
        /// <summary>
        /// Encrypt string connection
        /// </summary>
        /// <param name="modelo">entity to encrypt</param>
        /// <returns></returns>
        public string EncrytConnection(SAP modelo)
        {
            string stringConnection = GetStringConnection(modelo);
            if (stringConnection == null)
                return null;
            return StringCipher.Encrypt(stringConnection, "#Ida5of$");
        }
        /// <summary>
        /// Descrypt string connection
        /// </summary>
        /// <param name="stringConnection">string encrypt</param>
        /// <returns></returns>
        public SAP DescryConnection(string stringConnection)
        {
            string cadena = StringCipher.Decrypt(stringConnection, "#Ida5of$");
            return FromStringConnection(cadena);
        }
        /// <summary>
        /// Return entity type SAP for string connection
        /// </summary>
        /// <param name="stringConnection">string connection for create entity</param>
        /// <returns></returns>
        private SAP FromStringConnection(string stringConnection) {
            string[] elements = stringConnection.Split(';');
            List<string> values = new List<string>();
            foreach (string val in elements) {
                values.Add(val.Split('=')[1]);
            }
            return new SAP(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]);
        }
        /// <summary>
        /// return string connection
        /// </summary>
        /// <param name="model">entity to create string connection</param>
        /// <returns></returns>
        private string GetStringConnection(SAP model)
        {
            return
                string.Format(
                "Server={0};Database={1};UserId={2};Password={3};UserIdSAP={4};PasswordSAP={5};ServerType={6};LicenseServer={7};Language={8}",
                model.Server, model.CompanyDB, model.DbUserName, model.DbPassword, model.UserName, model.Password, model.DbServerType, model.SLDServer, model.language);
        }
    }
}
