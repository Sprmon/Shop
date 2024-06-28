namespace Sprmon.Shop.Identity.API.Services;

public interface IRedirectService
{
  string ExtractRedirectUriFromReturnUrl(string url);
}
