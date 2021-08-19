using System;
using System.Collections.Generic;
using System.Text;

namespace APIAutomationCust.Pages
{
    class Customer
    {
        public string id { get; set; }
        public string company_name { get; set; }
        public string vat_number { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
        public string default_language { get; set; }
    }
}
