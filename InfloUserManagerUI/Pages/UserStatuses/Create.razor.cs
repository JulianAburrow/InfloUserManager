namespace InfloUserManagerUI.Pages.UserStatuses;

public partial class Create
{
    protected override void OnInitialized()
    {
        UserStatusDTO = new();
        MainLayout.SetHeaderValue("Create User Status");
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserStatusHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateUserStatus()
    {
        UserStatusNameExists = false;

        await CheckForExistingUserStatusesAsync();

        if (UserStatusNameExists)
        {
            Snackbar.Add($"A User Status with the name {UserStatusDTO.StatusName} already exists. Please choose an unique User Status Name.", Severity.Error);
            return;
        }

        var response = await Http.PostAsJsonAsync(UserStatusesEndpoint, UserStatusDTO);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User Status {UserStatusDTO.StatusName} successfully created.", Severity.Success);
            NavigationManager.NavigateTo("userstatuses/index");
        }
        else
        {
            Snackbar.Add($" An error occurred creating User Status {UserStatusDTO.StatusName}. Please try again.", Severity.Error);
        }
    }
}