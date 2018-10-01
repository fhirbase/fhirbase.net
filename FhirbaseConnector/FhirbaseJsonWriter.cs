using System;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Newtonsoft.Json;

public class FhirbaseJsonWriter : JsonTextWriter
{
    private static ModelInspector s_modelInspector;

    static FhirbaseJsonWriter()
    {
	s_modelInspector = new ModelInspector();
	s_modelInspector.Import(typeof(Resource).Assembly);
    }

    private ClassMapping m_mapping;
    private bool m_isPolymorphic;

    public FhirbaseJsonWriter(StringWriter sw, Type t) : base(sw)
    {
	m_mapping = s_modelInspector.FindClassMappingByType(t);
    }

    private bool IsPolymorphic(string name)
    {
	var mappedProperty = m_mapping.FindMappedElementByName(name);
	if (mappedProperty != null)
	{
	    return mappedProperty.Choice == ChoiceType.DatatypeChoice;
	}
	return false;
    }

    private void WritePolymorphic(string name)
    {
	m_isPolymorphic = true;
	var mappedProperty = m_mapping.FindMappedElementByName(name);
	var t = mappedProperty.GetChoiceSuffixFromName(name);
	base.WritePropertyName(mappedProperty.Name);
	base.WriteStartObject();
	base.WritePropertyName(t.ToLower());
    }

    public override void WritePropertyName(string name)
    {
	if (IsPolymorphic(name))
	{
	    WritePolymorphic(name);
	}
	else
	{
	    base.WritePropertyName(name);
	}
    }

    public override void WritePropertyName(string name, bool escape)
    {
	if (IsPolymorphic(name))
	{
	    WritePolymorphic(name);
	}
	else
	{
	    base.WritePropertyName(name, escape);
	}
    }

    public override void WriteValue(Boolean v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Byte v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Byte[] v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Char v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(DateTime v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(DateTimeOffset v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Decimal v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Double v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Guid v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int16 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int32 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int64 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Boolean? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Byte? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Char? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(DateTime? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(DateTimeOffset? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Decimal? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Double? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Guid? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int16? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int32? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Int64? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(SByte? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Single? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(TimeSpan? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt16? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt32? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt64? v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Object v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(SByte v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Single v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(String v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(TimeSpan v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt16 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt32 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(UInt64 v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }

    public override void WriteValue(Uri v)
    {
	base.WriteValue(v);
	if (m_isPolymorphic)
	{
	    base.WriteEndObject();
	    m_isPolymorphic = false;
	}
    }
}
