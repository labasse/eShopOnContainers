// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebIdentity
{
    public static class Config
    {
        const int SpaPort = 32776;

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
                new Client
                {
                    ClientId = "spa",
                    ClientName = "eShopOnContainers (React)",
                      
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    //RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { $"https://localhost:{SpaPort}/" },

                    PostLogoutRedirectUris  = { $"https://localhost:{SpaPort}/" },
                    AllowedCorsOrigins      = { $"https://localhost:{SpaPort}" },

                    AllowedScopes = { 
                        "openid",
                        "profile",
                        "catalog.create",
                        "catalog.edit",  
                        "catalog.stock" 
                    }
                }
            };
    }
}