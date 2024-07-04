using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public static class Constants
    {
        public static class Application
        {
            public const string Is_Yes = "Y";
            public const string Is_No = "N";

            public const int Status_id = 1;
            public const string pending_completion = "PEDCM";
            public const string complete = "COMTE";

            public const int Active_Iactive_Status_id = 2;
            public const string Active = "ACTIV";
            public const string Inactive = "INACT";

            public const int Unit_type_id = 3;
            public const string PCS = "PCS";
            public const string MTR = "MTR";

            public const int Region_id = 4;
            public const int transport_name_id = 5;
            public const int package_type_id = 6;
            public const int mode_of_packing_id = 7;

            public const string consignee_name = "JJ Treaders";

            public const int Mode_Payment_id = 8;
            public const int Dispatched_name_id = 9;
            public const int Destination_id = 10;
            public const int customer_type_id = 11;
            public const int repaking_status_id = 12;
         
            public const string Approved_status = "APPRD";
            public const string Pending_Approved_status = "PEDAP";
            public const int Stock_status_id = 13;
            public const string InStock = "INSTK";
            public const string OutStock = "OUSTK";

            public const int PreOrder_Status_id = 15;
            public const string  Created = "CRETE";
            public const string Cancelled = "CANCD";
            public const string Completed = "COMPL";

            public const int discharge_port_id = 16; 
            public const int loading_port_id = 17;

            public const string ExportInvoice = "Export Invoice";

            public const int Screen_id = 20;
            public const int resource_type_id = 19;

        }
    }
}
