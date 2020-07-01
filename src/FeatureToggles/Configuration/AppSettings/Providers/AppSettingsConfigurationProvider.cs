//-----------------------------------------------------------------------
// <copyright file="AppSettingsConfigurationProvider.cs" company="Code Miners Limited">
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

namespace FeatureToggles.Configuration.AppSettings.Providers
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using FeatureToggles.Configuration.AppSettings.ToggleSettings;

    public class AppSettingsConfigurationProvider : IToggleConfiguration
    {
        private readonly ToggleSettings appSettings;

        private Dictionary<string, string> Settings = new Dictionary<string, string>();

        public void Initialise()
        {
            for (int i = 0; i < appSettings.toggleSettings.Count; i++)
            {
                ToggleSettingData element = appSettings.toggleSettings[i];

                if (!Settings.ContainsKey(element.key))
                {
                    Settings.Add(element.key, element.value);
                }

            }
        }

        public AppSettingsConfigurationProvider(IConfiguration configuration)
        {
            appSettings = configuration.GetSection("appSettings").Get<ToggleSettings>();
            Initialise();
        }

        public bool SystemEnabled
        {
            get
            {
                string value = Settings["Toggle:Enabled"];

                if (string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }

                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public bool DefaultValue
        {
            get
            {
                string value = Settings["Toggle:DefaultValue"];

                if (string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }

                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public string Environment
        {
            get
            {
                string value = Settings["Toggle:Environment"];

                if (string.IsNullOrWhiteSpace(value))
                {
                    return "production";
                }

                return value;
            }
        }
    }
}
