
namespace FeatureToggles.Configuration.AppSettings
{
    using System.Collections.Generic;

    public class ToggleElement
    {
        public List<RolesObject> roles { get; set; }

        public List<UsersObject> users { get; set; }

        public List<IpAddressesObject> ipaddresses { get; set; }

        public string name { get; set; }

        public bool enabled { get; set; }
    }
}