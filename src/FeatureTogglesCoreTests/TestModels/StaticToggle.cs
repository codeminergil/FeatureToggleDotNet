//-----------------------------------------------------------------------
// <copyright file="StaticToggle.cs" company="Code Miners Limited">
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

namespace FeatureTogglesCoreTests.TestModels
{
    using Microsoft.Extensions.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using FeatureToggles;
    using FeatureToggles.Configuration.AppSettings.Providers;
    using FeatureToggles.Providers.AppSettings;

    [ExcludeFromCodeCoverage]
    public static class StaticToggle
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        public static IConfiguration configuration = InitConfiguration();
        private static readonly ToggleFactory Factory = new ToggleFactory(new AppSettingsConfigurationProvider(configuration), new AppSettingsDataProvider(configuration));

        public static bool IsEnabled
        {
            get
            {
                Toggle toggle = Factory.Get<StrongToggleId>();

                // OR:
                // Toggle toggle = Factory.Get("StaticToggle");

                return toggle.IsEnabled;
            }
        }
    }
}
