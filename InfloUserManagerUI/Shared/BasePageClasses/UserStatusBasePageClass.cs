namespace InfloUserManagerUI.Shared.BasePageClasses;

public class UserStatusBasePageClass : BasePageClass
{
    [Parameter] public int UserStatusId { get; set; }

    protected UserStatusDTO UserStatusDTO = new();

    protected bool UserStatusNameExists;

    protected bool OkToDelete;

    protected BreadcrumbItem GetUserStatusHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new("User Statuses", "/userstatuses/index", isDisabled);
    }

    protected async Task CheckForExistingUserStatusesAsync()
    {
        var checkResponse = await Http
            .GetAsync($"{UserStatusesEndpoint}/checkuserstatuses/{UserStatusDTO.StatusName}/{UserStatusId}");
        UserStatusNameExists = checkResponse.StatusCode.Equals(HttpStatusCode.Conflict);
    }
}
