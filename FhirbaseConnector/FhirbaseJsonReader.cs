using System;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Newtonsoft.Json;

public class FhirbaseJsonReader : JsonReader
{
    private static ModelInspector s_modelInspector;

    static FhirbaseJsonReader()
    {
	s_modelInspector = new ModelInspector();
	s_modelInspector.Import(typeof(Resource).Assembly);
    }

    private JsonTextReader m_reader;
    private ClassMapping m_mapping;

    private bool m_isPolymorphic;
    private JsonToken m_tokenType;
    private object m_value;

    public FhirbaseJsonReader(object json, Type t)
    {
	m_reader = new JsonTextReader(new StringReader(json as string));
	m_mapping = s_modelInspector.FindClassMappingByType(t);
    }

    private void ReadPolymorphic()
    {
	var prop = m_reader.Value as string;

	m_reader.Read();
	m_reader.Read();

	var t = m_reader.Value as string;
	prop += char.ToUpper(t[0]) + t.Substring(1);

	SetToken(JsonToken.PropertyName, prop);

	m_reader.Read();
	m_tokenType = m_reader.TokenType;
	m_value = m_reader.Value;
	m_isPolymorphic = true;

	m_reader.Read();
    }

    private bool IsPolymorphic()
    {
	if (m_reader.TokenType == JsonToken.PropertyName)
	{
	    var mappedProperty = m_mapping.FindMappedElementByName(m_reader.Value as string);
	    if (mappedProperty != null)
	    {
		return mappedProperty.Choice == ChoiceType.DatatypeChoice;
	    }
	}
	return false;
    }

    public override bool Read()
    {
	if (m_isPolymorphic)
	{
	    SetToken(m_tokenType, m_value);
	    m_isPolymorphic = false;
	    return true;
	}
	else
	{
	    var res = m_reader.Read();

	    if (IsPolymorphic())
	    {
		ReadPolymorphic();
	    }
	    else
	    {
		SetToken(m_reader.TokenType, m_reader.Value);
	    }

	    return res;
	}
    }
}
