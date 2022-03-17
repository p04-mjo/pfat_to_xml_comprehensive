using System;


namespace Pfat_to_xml_comprehensive
{
    /// <summary>
    /// Class for person-objects
    /// </summary>
    public class Person : Avatar
    {
        private string firstname = "";
        private string lastname = "";
        public List<FamilyMember> family = new List<FamilyMember>();

        // P constructor
        public Person()
        {
        }

        // P|förnamn|efternamn         
        public void P(string pipeline)
        {
            string[] pipelist = pipeline.Split('|');
            try { this.firstname = pipelist[1].Trim(); } catch { };
            try { this.lastname = pipelist[2].Trim(); } catch { };
        }

        public string Get_firstname()
        {
            return this.firstname;
        }

        public string Get_lastname()
        {
            return this.lastname;
        }
    }
}