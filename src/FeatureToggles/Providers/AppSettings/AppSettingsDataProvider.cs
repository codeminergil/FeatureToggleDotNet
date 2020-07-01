//-----------------------------------------------------------------------
// <copyright file="AppSettingsDataProvider.cs" company="Code Miners Limited">
//  Copyright (c) 2019 Code Miners Limited
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------

namespace FeatureToggles.Providers.AppSettings
{
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FeatureToggles.Providers;
    using FeatureToggles.Configuration.AppSettings;
    using FeatureToggles;
    using FeatureToggles.Models;

    public class AppSettingsDataProvider : IToggleDataProvider
    {
        private static ToggleConfigurationSection ToggleConfigurations;

        private Dictionary<string, ToggleElement> toggles = new Dictionary<string, ToggleElement>();

        public void Initialise()
        {
            for (int i = 0; i < ToggleConfigurations.toggles.Count; i++)
            {
                ToggleElement element = ToggleConfigurations.toggles[i];

                if (!toggles.ContainsKey(element.name))
                {
                    toggles.Add(element.name, ToggleConfigurations.toggles[i]);
                }

            }
        }

        public AppSettingsDataProvider(IConfiguration configuration)
        {
            ToggleConfigurations = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();
            Initialise();
        }

        public Toggle GetFlag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Toggle.Empty;
            }

            if (!toggles.ContainsKey(name))
            {
                return Toggle.Empty;
            }

            ToggleElement element = toggles[name];

            Toggle toggle = new Toggle(element.name, element.enabled);

            return toggle;
        }

        public Toggle GetFlag(string name, ToggleData userData)
        {
            if (!toggles.ContainsKey(name))
            {
                return Toggle.Empty;
            }

            ToggleElement element = toggles[name];

            if (!string.IsNullOrWhiteSpace(userData.UserRoles))
            {
                List<string> roles = element.roles.Select(x => x.role.name).ToList();
                bool found = false;
                foreach (string role in userData.UserRoles.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (roles.Contains(role))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return new Toggle(name, false);
                }
            }

            if (!string.IsNullOrWhiteSpace(userData.UserId))
            {
                List<string> users = element.users.Select(x => x.user.name).ToList();

                if (!users.Contains(userData.UserId))
                {
                    return new Toggle(name, false);
                }
            }

            if (!string.IsNullOrWhiteSpace(userData.IpAddress))
            {
                if (!IPAddress.TryParse(userData.IpAddress, out IPAddress candidate))
                {
                    return new Toggle(name, false);
                }

                List<string> addresses = element.ipaddresses.Select(x => x.ipaddress.value).ToList();
                bool found = false;
                foreach (string address in addresses)
                {
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        continue;
                    }

                    IPAddressRange range = IPAddressRange.FromCidrAddress(address);
                    if (range == IPAddressRange.Empty)
                    {
                        continue;
                    }

                    if (range.IPInRange(candidate))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return new Toggle(name, false);
                }
            }

            return new Toggle(name, true);
        }


    }
}

