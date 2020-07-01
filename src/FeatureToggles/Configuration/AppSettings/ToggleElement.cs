//-----------------------------------------------------------------------
// <copyright file="ToggleElement.cs" company="Code Miners Limited">
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