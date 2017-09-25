using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Database {

	public class ClastList
    {
        public List<Class> classes
        {
            get; set;
        }
    }

    public Database()
    {

    }

    public void Load()
    {
        LoadClasses();
    }

    private void LoadClasses()
    {
        TextAsset classesYaml = Resources.Load("Classes") as TextAsset;
        StringReader input = new StringReader(classesYaml.text);
        DeserializerBuilder deserializerBuilder = new DeserializerBuilder();
        deserializerBuilder.WithNamingConvention(new CamelCaseNamingConvention());
        Deserializer deserializer = deserializerBuilder.Build();
        ClassList classList = deserializer.Deserialize<ClassList>(input);
        Class.AddItems(classList.classes);
    }
}
