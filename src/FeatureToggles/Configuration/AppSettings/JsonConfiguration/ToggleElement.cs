
namespace FeatureTogglesIConfiguration.JsonConfiguration
{
    using System.Collections.Generic;

    public class ToggleElement
    {
        public List<RolesElementCollection> roles { get; set; }

        public List<UsersElementCollection> users { get; set; }

        public List<IpAddressesElementCollection> ipaddresses { get; set; }

        public string name { get; set; }

        public bool enabled { get; set; }
    }
}