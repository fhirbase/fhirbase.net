using System;
using System.Data;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Npgsql;

namespace FhirbaseConnector
{
    public class Connector
    {
	private string m_connString;

	public Connector(string host = "localhost",
			 int port = 5432,
			 string user = "postgres",
			 string password = "",
			 string db = "fhirbase")
	{
	    m_connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
					 host, port, user, password, db);
	}

	private string SerializeResource<T>(T resource, Type type) where T : Base
	{
	    var sb = new StringBuilder();
	    var sw = new StringWriter(sb);
	    var serializer = new FhirJsonSerializer();
	    using(var writer = new FhirbaseJsonWriter(sw, type))
	    {
		serializer.Serialize(resource, writer);
	    }
	    return sb.ToString();
	}

	private List<T> Exec<T>(string sql, Type type) where T : Base
	{
	    using (var conn = new NpgsqlConnection(m_connString))
	    {
		conn.Open();
		var da = new NpgsqlDataAdapter(sql, conn);
		var ds = new DataSet();
                da.Fill(ds);

		var res = new List<T>();

		foreach(DataRow row in ds.Tables[0].Rows)
		{
		    var parser = new FhirJsonParser();
		    using (var reader = new FhirbaseJsonReader(row.ItemArray[0], type))
		    {
			res.Add(parser.Parse<T>(reader));
		    }
		}

		return res;
	    }
	}

	public T Create<T>(T resource) where T : Base
	{
	    var type = typeof(T);
	    var resourceStr = SerializeResource(resource, type);
	    var sql = String.Format("SELECT fhirbase_create($${0}$$::jsonb);", resourceStr);
	    var res = Exec<T>(sql, type);
	    return res[0];
	}

	public T Update<T>(T resource) where T : Base
	{
	    var type = typeof(T);
	    var resourceStr = SerializeResource(resource, type);
	    var sql = String.Format("SELECT fhirbase_update($${0}$$::jsonb);", resourceStr);
	    var res = Exec<T>(sql, type);
	    return res[0];
	}

	public T Delete<T>(T resource) where T : Resource
	{
	    var type = typeof(T);
	    var t = type.Name;
	    var sql = String.Format("SELECT fhirbase_delete('{0}', '{1}');", t, resource.Id);
	    var res = Exec<T>(sql, type);
	    return res[0];
	}

	public List<T> Read<T>(int limit = -1) where T : Base
	{
	    var limitStr = "";
	    if (limit > 0)
	    {
		limitStr = "LIMIT " + limit;
	    }
	    var type = typeof(T);
	    var t = type.Name.ToLower();
	    var sql = String.Format("SELECT _fhirbase_to_resource(row(r.*)::_resource) FROM {0} as r {1}",
				    t, limitStr);
	    return Exec<T>(sql, type);
	}

	public List<T> Read<T>(string sql) where T : Base
	{
	    var type = typeof(T);
	    return Exec<T>(sql, type);
	}
    }
}
