﻿using System.Configuration;

namespace NSimpleOLAP.Configuration
{
  /// <summary>
  /// Represents a single XML tag inside a ConfigurationSection
  /// or a ConfigurationElementCollection.
  /// </summary>
  public sealed class DimensionConfig : ConfigurationElement
  {
    /// <summary>
    /// The attribute <c>name</c> of a <c>DimensionElement</c>.
    /// </summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\.,", MinLength = 0, MaxLength = 120)]
    public string Name
    {
      get { return (string)this["name"]; }
      set { this["name"] = value; }
    }

    [ConfigurationProperty("source", IsRequired = true)]
    [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
    public string Source
    {
      get { return (string)this["source"]; }
      set { this["source"] = value; }
    }

    [ConfigurationProperty("descFieldName")]
    [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
    public string DesFieldName
    {
      get { return (string)this["descFieldName"]; }
      set { this["descFieldName"] = value; }
    }

    [ConfigurationProperty("valueFieldName")]
    [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
    public string ValueFieldName
    {
      get { return (string)this["valueFieldName"]; }
      set { this["valueFieldName"] = value; }
    }

    [ConfigurationProperty("descFieldIndex")]
    public int? DesFieldIndex
    {
      get { return (int?)this["descFieldIndex"]; }
      set { this["descFieldIndex"] = value; }
    }

    [ConfigurationProperty("valueFieldIndex")]
    public int? ValueFieldIndex
    {
      get { return (int?)this["valueFieldIndex"]; }
      set { this["valueFieldIndex"] = value; }
    }

    [ConfigurationProperty("allowsNameDuplicates", DefaultValue = false)]
    public bool AllowsMembersWithSameName
    {
      get { return (bool)this["allowsNameDuplicates"]; }
      set { this["allowsNameDuplicates"] = value; }
    }
  }
}