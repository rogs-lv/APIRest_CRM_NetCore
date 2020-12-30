using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entity
{
    public class Item
    {
        public string   ItemCode    { get; set; }
        public string   ItemName    { get; set; }
        public char     VATLiable   { get; set; }
        public string   TaxCodeAR   { get; set; }
        public double   Rate        { get; set; }
        public char     IndirctTax  { get; set; }
        public double   Stock       { get; set; }
        public string   SalUnitMsr  { get; set; }
        public decimal  Price       { get; set; }
        public int      ItmsGrpCod  { get; set; }
        public string   ItmsGrpNam  { get; set; }
        public string   WhsCode     { get; set; }
    }
}
