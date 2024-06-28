namespace Sprmon.Shop.Identity.API.Configuration;

public class Config
{
  // ApiResources define the apis in your system
  public static IEnumerable<ApiResource> GetApis()
  {
    return
    [
      new("orders", "Orders Service"),
      new("basket", "Basket Service"),
      new("webhooks", "Webhooks registration Service"),
    ];
  }

  // ApiScope is used to protect the API 
  //The effect is the same as that of API resources in IdentityServer 3.x
  public static IEnumerable<ApiScope> GetApiScopes()
  {
    return
    [
      new("orders", "Orders Service"),
      new("basket", "Basket Service"),
      new("webhooks", "Webhooks registration Service"),
    ];
  }

  // Identity resources are data like user ID, name, or email address of a user
  // see: http://docs.identityserver.io/en/release/configuration/resources.html
  public static IEnumerable<IdentityResource> GetResources()
  {
    return
    [
      new IdentityResources.OpenId(),
      new IdentityResources.Profile()
    ];
  }

  // client want to access resources (aka scopes)
  public static IEnumerable<Client> GetClients(IConfiguration configuration)
  {
    return
    [
      new() {
        ClientId = "maui",
        ClientName = "eShop MAUI OpenId Client",
        AllowedGrantTypes = GrantTypes.Code,                    
        //Used to retrieve the access token on the back channel.
        ClientSecrets =
        {
          new Secret("secret".Sha256())
        },
        RedirectUris = { configuration["MauiCallback"] },
        RequireConsent = false,
        RequirePkce = true,
        PostLogoutRedirectUris = { $"{configuration["MauiCallback"]}/Account/Redirecting" },
        //AllowedCorsOrigins = { "http://eshopxamarin" },
        AllowedScopes =
        [
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          IdentityServerConstants.StandardScopes.OfflineAccess,
          "orders",
          "basket",
          "mobileshoppingagg",
          "webhooks"
        ],
        //Allow requesting refresh tokens for long lived API access
        AllowOfflineAccess = true,
        AllowAccessTokensViaBrowser = true,
        AlwaysIncludeUserClaimsInIdToken = true,
        AccessTokenLifetime = 60*60*2, // 2 hours
        IdentityTokenLifetime= 60*60*2 // 2 hours
      },
      new() {
        ClientId = "webapp",
        ClientName = "WebApp Client",
        ClientSecrets =
        [
          new("secret".Sha256())
        ],
        ClientUri = $"{configuration["WebAppClient"]}", // public uri of the client
        AllowedGrantTypes = GrantTypes.Code,
        AllowAccessTokensViaBrowser = false,
        RequireConsent = false,
        AllowOfflineAccess = true,
        AlwaysIncludeUserClaimsInIdToken = true,
        RequirePkce = false,
        RedirectUris =
        [
          $"{configuration["WebAppClient"]}/signin-oidc"
        ],
        PostLogoutRedirectUris =
        [
          $"{configuration["WebAppClient"]}/signout-callback-oidc"
        ],
        AllowedScopes =
        [
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          IdentityServerConstants.StandardScopes.OfflineAccess,
          "orders",
          "basket",
          "webshoppingagg",
          "webhooks"
        ],
        AccessTokenLifetime = 60*60*2, // 2 hours
        IdentityTokenLifetime= 60*60*2 // 2 hours
      },
      new() {
        ClientId = "webhooksclient",
        ClientName = "Webhooks Client",
        ClientSecrets =
        [
          new("secret".Sha256())
        ],
        ClientUri = $"{configuration["WebhooksWebClient"]}",                             // public uri of the client
        AllowedGrantTypes = GrantTypes.Code,
        AllowAccessTokensViaBrowser = false,
        RequireConsent = false,
        AllowOfflineAccess = true,
        AlwaysIncludeUserClaimsInIdToken = true,
        RedirectUris =
        [
          $"{configuration["WebhooksWebClient"]}/signin-oidc"
        ],
        PostLogoutRedirectUris =
        [
          $"{configuration["WebhooksWebClient"]}/signout-callback-oidc"
        ],
        AllowedScopes =
        {
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          IdentityServerConstants.StandardScopes.OfflineAccess,
          "webhooks"
        },
        AccessTokenLifetime = 60*60*2, // 2 hours
        IdentityTokenLifetime= 60*60*2 // 2 hours
      },
      new() {
        ClientId = "basketswaggerui",
        ClientName = "Basket Swagger UI",
        AllowedGrantTypes = GrantTypes.Implicit,
        AllowAccessTokensViaBrowser = true,

        RedirectUris = { $"{configuration["BasketApiClient"]}/swagger/oauth2-redirect.html" },
        PostLogoutRedirectUris = { $"{configuration["BasketApiClient"]}/swagger/" },

        AllowedScopes =
        {
          "basket"
        }
      },
      new() {
        ClientId = "orderingswaggerui",
        ClientName = "Ordering Swagger UI",
        AllowedGrantTypes = GrantTypes.Implicit,
        AllowAccessTokensViaBrowser = true,

        RedirectUris = { $"{configuration["OrderingApiClient"]}/swagger/oauth2-redirect.html" },
        PostLogoutRedirectUris = { $"{configuration["OrderingApiClient"]}/swagger/" },

        AllowedScopes =
        {
          "orders"
        }
      },
      new() {
        ClientId = "webhooksswaggerui",
        ClientName = "WebHooks Service Swagger UI",
        AllowedGrantTypes = GrantTypes.Implicit,
        AllowAccessTokensViaBrowser = true,

        RedirectUris = { $"{configuration["WebhooksApiClient"]}/swagger/oauth2-redirect.html" },
        PostLogoutRedirectUris = { $"{configuration["WebhooksApiClient"]}/swagger/" },

        AllowedScopes =
        {
          "webhooks"
        }
      }
    ];
  }
}
