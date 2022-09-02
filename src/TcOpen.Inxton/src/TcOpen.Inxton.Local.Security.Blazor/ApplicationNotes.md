# Application Notes

- BlazorAuthenticationStateProvider
  - extending AuthenticationStateProvider(from Blazor) and overriding GetAuthenticationStateAsync(it is calling when page is loading and return ClaimsIdentity with data from UserAccessor(the same one, than has WPF))
  - and implements IAuthenticationService(the same one, than has WPF) - it has lots of method, for example: AuthenticateUser, DeAuthenticateCurrentUser or CalculateHash. Some is not ussing
- in Service configuration added Service BlazorAuthenticationStateProvider
