using System.Configuration;
using System.Security.Permissions;

namespace System.Web.NHaml.Configuration
{
    [AspNetHostingPermission( SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal )]
    [AspNetHostingPermission( SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal )]
    public abstract class KeyedConfigurationElement : ConfigurationElement
    {
        public abstract string Key { get; }
    }
}