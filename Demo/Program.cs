using System;
using System.Collections.Generic;
using System.Linq;
using FhirbaseConnector;
using Hl7.Fhir.Model;

namespace Demo
{
    class Program
    {
	private static void DisplayPatient(Patient patient)
	{
	    string name = "Unknown";
	    if (patient.Name.Count > 0)
	    {
		if (patient.Name[0].Given.Count() > 0 || !String.IsNullOrWhiteSpace(patient.Name[0].Family))
		{
		    name = "";
		    if (patient.Name[0].Given.Count() > 0)
		    {
			name = patient.Name[0].Given.First();
		    }
		    if (!String.IsNullOrWhiteSpace(patient.Name[0].Family))
		    {
			if (name != "")
			{
			    name += " ";
			}
			name += patient.Name[0].Family;
		    }
		}
	    }
	    string birthdate = "";
	    if (!string.IsNullOrWhiteSpace(patient.BirthDate))
	    {
		birthdate = " (" + patient.BirthDate + ")";
	    }
	    Console.WriteLine("Patient {0}{1}", name, birthdate);
	}

	private static void ReadPatients(Connector connector, int count)
	{
	    Console.WriteLine("---------------------");
	    Console.WriteLine("Read resource demo:");
	    foreach (var patient in connector.Read<Patient>(count))
	    {
		DisplayPatient(patient);
	    }
	}

	private static Patient CreatePatient(Connector connector, Patient patient)
	{
	    Console.WriteLine("---------------------");
	    Console.WriteLine("Create resource demo:");
	    var res = connector.Create(patient);
	    DisplayPatient(res);
	    return res;
	}

	private static Patient UpdatePatient(Connector connector, Patient patient)
	{
	    Console.WriteLine("---------------------");
	    Console.WriteLine("Update resource demo:");
	    var res = connector.Update(patient);
	    DisplayPatient(res);
	    return res;
	}

	private static void FindPatientByFamily(Connector connector)
	{
	    Console.WriteLine("---------------------");
	    Console.WriteLine("Find resource demo:");
	    var sql = "SELECT _fhirbase_to_resource(row(r.*)::_resource) FROM patient AS r WHERE resource#>>'{name,0,family}' ilike '%snow%'";
	    foreach (var patient in connector.Read<Patient>(sql))
	    {
		DisplayPatient(patient);
	    }
	}

	private static Patient DeletePatient(Connector connector, Patient patient)
	{
	    Console.WriteLine("---------------------");
	    Console.WriteLine("Delete resource demo:");
	    var res = connector.Delete(patient);
	    DisplayPatient(res);
	    return res;
	}

        static void Main(string[] args)
	{
	    try
	    {
		var connector = new Connector();
		ReadPatients(connector, 3);

		var patient = new Patient
		{
		    Name = new List<HumanName>
		    {
			new HumanName
			{
			    Given = new List<string> { "John" },
			    Family = "Doe"
			}
		    }
		};
		patient = CreatePatient(connector, patient);

		patient.Name[0].Family = "Snow";
		patient.BirthDate = "1985-03-20";
		patient = UpdatePatient(connector, patient);

		FindPatientByFamily(connector);

		DeletePatient(connector, patient);
	    }
	    catch (Exception e)
	    {
		Console.WriteLine("Something go wrong: {0}", e);
	    }

	    Console.WriteLine("Press any key to exit");
	    Console.ReadKey();
	}
    }
}
