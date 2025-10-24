namespace InfloUserManagerUI.Pages.UserStatuses;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserStatusDTO = await Http.GetFromJsonAsync<UserStatusDTO>($"{UserStatusesEndpoint}/{UserStatusId}") ?? new();
            OkToDelete = UserStatusDTO.UserCount == 0;
            MainLayout.SetHeaderValue("Delete User Status");
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
            GetCustomBreadcrumbItem("Delete"),
        ]);
    }

    private async Task DeleteUserStatus()
    {
        var response = await Http.DeleteAsync($"{UserStatusesEndpoint}/{UserStatusId}");
        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User Status {UserStatusDTO.StatusName} successfully deleted.", Severity.Success);
            NavigationManager.NavigateTo("/userstatuses/index");
        }
        else
        {
            Snackbar.Add($"An error occurred deleting User Status {UserStatusDTO.StatusName}. Please try again.", Severity.Error);
        }
    }
}