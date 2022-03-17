using System;


namespace Pfat_to_xml_comprehensive
{
    /// <summary>
    /// Class for family member-objects
    /// </summary>
    public class FamilyMember : Avatar
    {
        private string name = "";
        private string dob = "";

        // F constructor
        public FamilyMember()
        {
        }

        // F|namn|födelseår  
        public void F(string pipeline)
        {
            string[] pipelist = pipeline.Split('|');
            try { this.name = pipelist[1].Trim(); } catch { };
            try { this.dob = pipelist[2].Trim(); } catch { };
        }

        public string Get_name()
        {
            return this.name;
        }

        public string Get_dob()
        {
            return this.dob;
        }

    }
}