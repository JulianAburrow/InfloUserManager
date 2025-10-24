namespace InfloUserManagerUI.Pages.UserStatuses;

public partial class Index
{
    private List<UserStatusDTO> UserStatusDTOs = null!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserStatusDTOs = await Http.GetFromJsonAsync<List<UserStatusDTO>>(UserStatusesEndpoint) ?? [];
            Snackbar.Add($"{UserStatusDTOs.Count} item(s) found.", UserStatusDTOs.Count > 0 ? Severity.Info : Severity.Warning);
            MainLayout.SetHeaderValue("User Statuses");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching User Statuses.", Severity.Error);
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("User Statuses")
        ]);
    }
}