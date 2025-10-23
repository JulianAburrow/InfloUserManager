namespace InfloUserManagerUI.Shared.BasePageClasses;

public class UserBasePageClass : BasePageClass
{
    [Parameter]
    public int UserId { get; set; }

    protected UserDTO UserDTO = null!;

    protected bool UserNumberExists;

    protected BreadcrumbItem GetUserHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new("Users", "/Users/index", isDisabled);
    }

    protected async Task CheckForExistingUsersAsync()
    {
        var checkResponse = await Http
            .GetAsync($"{UsersEndpoint}/check/{UserDTO.UserNumber}/{UserId}");
        
        UserNumberExists = checkResponse.StatusCode.Equals(HttpStatusCode.Conflict);
    }
}
