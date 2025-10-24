namespace InfloUserManagerUI.Pages.UserStatuses;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserStatusDTO = await Http.GetFromJsonAsync<UserStatusDTO>($"{UserStatusesEndpoint}/{UserStatusId}") ?? new();
            MainLayout.SetHeaderValue("Edit User Status");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching User Status details.", Severity.Error);
        }

        UserStatusNameExists = false;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserStatusHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("Edit"),
        ]);
    }

    private async Task UpdateUserStatus()
    {
        UserStatusNameExists = false;

        await CheckForExistingUserStatusesAsync();

        if (UserStatusNameExists)
        {
            Snackbar.Add($"A User Status with the name {UserStatusDTO.StatusName} already exists. Please choose an unique User Status name.", Severity.Error);
            return;
        }
        var response = await Http.PutAsJsonAsync($"{UserStatusesEndpoint}/{UserStatusId}", UserStatusDTO);
        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User Status {UserStatusDTO.StatusName} successfully updated.", Severity.Success);
            NavigationManager.NavigateTo("/userstatuses/index");
        }
        else
        {
            Snackbar.Add($"An error occurred updating User Status {UserStatusDTO.StatusName}. Please try again.", Severity.Error);
        }
    }
}