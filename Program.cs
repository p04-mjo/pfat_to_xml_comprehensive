// See https://aka.ms/new-console-template for more information
using System.Xml; //added

namespace Pfat_to_xml_comprehensive
{

    /// <summary>
    /// Main Program, reads data from file, add data to array of objects and finally prints it and pushes it to an output file. 
    /// uses the schema above to push data from input file to an array of objects
    /// 
    /// P|förnamn|efternamn 
    /// T|mobilnummer|fastnätsnummer 
    /// F|namn|födelseår 
    /// A|gata|stad|postnummer  
    ///  
    /// </summary>
    public class Program
    {
        private const string xmlPath = ""; //where xml-file is created
        private const string xmlFile = "people.xml"; //name for created xml-file

        private const string ptafPath = ""; 

        private const string ptafFile = "indata.txt";

        public string[] FetchData(string ptafPath, string ptafFile)
        {
            //fetch input data from file
            string[] data = { };
            try { data = File.ReadAllLines(Path.Combine(ptafPath, ptafFile)); } catch { };
            return data;
        }

        public List<Person> BuildPersonList(string[] data)
        {
            // read PFTA data into the list people
            bool pExist = false;
            bool fExist = false;
            List<Person> people = new List<Person>();
            Person person = new Person();
            FamilyMember familyMember = new FamilyMember();
            foreach (string pipeline in data)
            {
                switch (pipeline[0])
                {
                    case 'P': // P|förnamn|efternamn 
                        if (fExist && pExist)
                        {
                            person.family.Add(familyMember);
                            people.Add(person);
                        }
                        if (!fExist && pExist)
                        {
                            people.Add(person);
                        }
                        person = new Person();
                        pExist = true;
                        fExist = false;
                        person.P(pipeline);
                        break;
                    case 'T': // T|mobilnummer|fastnätsnummer 
                        if (fExist)
                        {
                            familyMember.T(pipeline);
                        }
                        if (!fExist && pExist)
                        {
                            person.T(pipeline);
                        }
                        break;
                    case 'F': // F|namn|födelseår 
                        if (fExist)
                        {
                            person.family.Add(familyMember);
                        }
                        familyMember = new FamilyMember();
                        fExist = true;
                        familyMember.F(pipeline);
                        break;
                    case 'A': // A|gata|stad|postnummer  
                        if (fExist)
                        {
                            familyMember.A(pipeline);
                        }
                        if (!fExist && pExist)
                        {
                            person.A(pipeline);
                        }
                        break;
                    default: // undefind tag found, add to current object?
                        break;
                }
            }
            if (fExist)
            {
                person.family.Add(familyMember);
                people.Add(person);
            }
            if (!fExist && pExist)
            {
                people.Add(person);
            }
            return people;
        }

        public void BuildXMLFile(List<Person> people, string xmlPath, string xmlFile)
        {
            // create xml file and write content in list people to that
            XmlWriter xmlWriter = XmlWriter.Create(Path.Combine(xmlPath, xmlFile)); // filepath + filename concatinated, built in method exists!!
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("people");
            if (people.Any())
            {
                xmlWriter.WriteStartElement("person");
                foreach (Person p in people)
                {
                    //p
                    if (!string.IsNullOrEmpty(p.Get_firstname()))
                    {
                        xmlWriter.WriteStartElement("firstname");
                        xmlWriter.WriteString(p.Get_firstname());
                        xmlWriter.WriteEndElement(); //firstname
                    }
                    if (!string.IsNullOrEmpty(p.Get_lastname()))
                    {
                        xmlWriter.WriteStartElement("lastname");
                        xmlWriter.WriteString(p.Get_lastname());
                        xmlWriter.WriteEndElement(); //lastname
                    }

                    //a
                    if (!string.IsNullOrEmpty(p.Get_street()) || !string.IsNullOrEmpty(p.Get_city()) || !string.IsNullOrEmpty(p.Get_zip()))
                    {
                        xmlWriter.WriteStartElement("address");
                        if (!string.IsNullOrEmpty(p.Get_street()))
                        {
                            xmlWriter.WriteStartElement("street");
                            xmlWriter.WriteString(p.Get_street());
                            xmlWriter.WriteEndElement(); //street
                        }
                        if (!string.IsNullOrEmpty(p.Get_zip()))
                        {
                            xmlWriter.WriteStartElement("zip");
                            xmlWriter.WriteString(p.Get_zip());
                            xmlWriter.WriteEndElement(); //zip
                        }
                        if (!string.IsNullOrEmpty(p.Get_city()))
                        {
                            xmlWriter.WriteStartElement("city");
                            xmlWriter.WriteString(p.Get_city());
                            xmlWriter.WriteEndElement(); //city
                        }
                        xmlWriter.WriteEndElement(); //address
                    }

                    //t
                    if (!string.IsNullOrEmpty(p.Get_landline()) || !string.IsNullOrEmpty(p.Get_cell()))
                    {
                        xmlWriter.WriteStartElement("phone");
                        if (!string.IsNullOrEmpty(p.Get_landline()))
                        {
                            xmlWriter.WriteStartElement("landline");
                            xmlWriter.WriteString(p.Get_landline());
                            xmlWriter.WriteEndElement(); //landline
                        }
                        if (!string.IsNullOrEmpty(p.Get_cell()))
                        {
                            xmlWriter.WriteStartElement("cell");
                            xmlWriter.WriteString(p.Get_cell());
                            xmlWriter.WriteEndElement(); //cell
                        }
                        xmlWriter.WriteEndElement(); //phone
                    }

                    //f
                    foreach (FamilyMember f in p.family)
                    {
                        xmlWriter.WriteStartElement("familymember");
                        //f.p
                        if (!string.IsNullOrEmpty(f.Get_name()))
                        {
                            xmlWriter.WriteStartElement("name");
                            xmlWriter.WriteString(f.Get_name());
                            xmlWriter.WriteEndElement(); //name
                        }
                        if (!string.IsNullOrEmpty(f.Get_dob()))
                        {
                            xmlWriter.WriteStartElement("dob");
                            xmlWriter.WriteString(f.Get_dob());
                            xmlWriter.WriteEndElement(); //dob
                        }

                        //f.a
                        if (!string.IsNullOrEmpty(f.Get_street()) || !string.IsNullOrEmpty(f.Get_city()) || !string.IsNullOrEmpty(f.Get_zip()))
                        {
                            xmlWriter.WriteStartElement("address");
                            if (!string.IsNullOrEmpty(f.Get_street()))
                            {
                                xmlWriter.WriteStartElement("street");
                                xmlWriter.WriteString(f.Get_street());
                                xmlWriter.WriteEndElement(); //street
                            }
                            if (!string.IsNullOrEmpty(f.Get_zip()))
                            {
                                xmlWriter.WriteStartElement("zip");
                                xmlWriter.WriteString(f.Get_zip());
                                xmlWriter.WriteEndElement(); //zip
                            }
                            if (!string.IsNullOrEmpty(f.Get_city()))
                            {
                                xmlWriter.WriteStartElement("city");
                                xmlWriter.WriteString(f.Get_city());
                                xmlWriter.WriteEndElement(); //city
                            }
                            xmlWriter.WriteEndElement(); //address
                        }

                        //f.t
                        if (!string.IsNullOrEmpty(f.Get_landline()) || !string.IsNullOrEmpty(f.Get_cell()))
                        {
                            xmlWriter.WriteStartElement("phone");
                            if (!string.IsNullOrEmpty(f.Get_landline()))
                            {
                                xmlWriter.WriteStartElement("landline");
                                xmlWriter.WriteString(f.Get_landline());
                                xmlWriter.WriteEndElement(); //landline
                            }
                            if (!string.IsNullOrEmpty(f.Get_cell()))
                            {
                                xmlWriter.WriteStartElement("cell");
                                xmlWriter.WriteString(f.Get_cell());
                                xmlWriter.WriteEndElement(); //cell
                            }
                            xmlWriter.WriteEndElement(); //phone
                        }
                        xmlWriter.WriteEndElement(); //familymember
                    }
                }
                xmlWriter.WriteEndElement(); //person
            }
            xmlWriter.WriteEndElement(); //people
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Script som konverterar från PFAT till XML-format");
            Console.WriteLine("defaultnamn för PFAT-fil är .\\indata.txt");
            Console.WriteLine("defaultnamn för XML-fil är .\\output.xml");
            Program prog = new Program();
            prog.BuildXMLFile(prog.BuildPersonList(prog.FetchData(ptafFile, ptafPath)), xmlPath, xmlFile);
            Console.ReadKey();
        }
    }
}