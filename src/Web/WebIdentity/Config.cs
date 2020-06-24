﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebIdentity
{
    public static class Config
    {
        #region Utility methods
        private static T[] NewList<T>(params T[] args) => args;

        private static ICollection<Secret> NewSecrets(params string[] sList) => sList.Select(s => new Secret(s.Sha256())).ToList();

        private static Scope NewScope(
            string name, string displayName, string description = "",
            bool required = true, bool emphasize = false,
            ICollection<string> userClaims = null 
        ) => new Scope
        {
            Name = name,
            DisplayName = displayName,
            Required = required,
            Emphasize = emphasize,
            Description = description,
            UserClaims = userClaims ?? new string[] { }
        };
        #endregion

        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis => NewList(
            new ApiResource("catalog", "Catalog API")
            {
                ApiSecrets = NewSecrets("870717F5-647C-4206-984C-D1347B1E203F"),
                Description = "Catalog related service for eShopOnContainers items and stock",
                Scopes = {
                    NewScope("catalog.create", "Catalog creation", "Fill catalog with new products"),
                    NewScope("catalog.edit"  , "Catalog editing" , "Manage items in catalog"       ),
                    NewScope("catalog.stock" , "Inventory"       , "Edit catalog items inventory"  , true, true, NewList("EmployeeNumber"))
                }
            },
            new ApiResource("basket", "Cart Management") { },
            new ApiResource("ordering", "Order Management") { }
        );


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "catalog" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "catalog" }
                },

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",
                      
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "catalog" }
                    
                }
            };
    }
}