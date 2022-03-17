using System;

namespace Pfat_to_xml_comprehensive
{ 
	
    /// <summary>
    /// base class for person and family objects
    /// </summary>
    public class Avatar
    {
        protected string street = "";
        protected string city = "";
        protected string zip = "";
        protected string cell = "";
        protected string landline = "";
        //protected string[] unexpected;

        // A|gata|stad|postnummer  
        public void A(string pipeline)
        {
            string[] pipelist = pipeline.Split('|');
            try { this.street = pipelist[1].Trim(); } catch { };
            try { this.city = pipelist[2].Trim(); } catch { };
            try { this.zip = pipelist[3].Trim(); } catch { };
        }

        // T|mobilnummer|fastnätsnummer         
        public void T(string pipeline)
        {
            string[] pipelist = pipeline.Split('|');
            try { this.cell = pipelist[1].Trim(); } catch { };
            try { this.landline = pipelist[2].Trim(); } catch { };
        }

        public string Get_street()
        {
            return this.street;
        }

        public string Get_city()
        {
            return this.city;
        }

        public string Get_zip()
        {
            return this.zip;
        }

        public string Get_cell()
        {
            return this.cell;
        }

        public string Get_landline()
        {
            return this.landline;
        }
    }

    
}
