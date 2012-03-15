using System.Configuration;

namespace NHaml4.Configuration
{
    public  class ImportConfigurationElement : KeyedConfigurationElement
    {
        public ImportConfigurationElement()
        {
        }

        public ImportConfigurationElement(string name)
        {
            Name = name;
        }

        public override string Key
        {
            get { return Name; }
        }

        private const string NamespaceElement = "namespace";
        [ConfigurationProperty(NamespaceElement, IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this[NamespaceElement]; }
            set { this[NamespaceElement] = value; }
        }
    }
}