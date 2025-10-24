namespace InfloUserManagerUI.Pages.UserStatuses;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserStatusDTO = await Http.GetFromJsonAsync<UserStatusDTO>($"{UserStatusesEndpoint}/{UserStatusId}") ?? new();
            OkToDelete = UserStatusDTO.UserCount == 0;
            MainLayout.SetHeaderValue("View User Status");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching User Status details.", Severity.Error);
        }
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserStatusHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}