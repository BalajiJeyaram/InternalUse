using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWRTemplate.Models
{
    public class EWRTemplate_Model
    {
        public string usecase { get; set; }
        public string problemstatement { get; set; }
        public string desiredbehavior { get; set; }

        public string currentbehavior { get; set; }

        public string casenumber { get; set; }

        public string arealldeviceaffected { get; set; }

        public string devicesaffectedpercentage { get; set; }
        public string areallsiteaffected { get; set; }

        public string competitivedevicefunctioning { get; set; }

        public string wasitworkingpreviosuly{ get; set; }

        public string wasitrecentlychanged { get; set; }

        public string DeviceInformation { get; set; }

        public string specificrequestofengineering { get; set; }

        public string reproducewithinhoneywell { get; set; }

        public string logscolletedfromcustomer { get; set; }

        public string didlogshowanyissue { get; set; }

        public string hardwareshipment { get; set; }

        public string accesstocustomertool { get; set; }

        public string environmentdetails { get; set; }

        public string reproductionsteps { get; set; }


    }
}