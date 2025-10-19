namespace InfloUserManagerUI.Pages.Users;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserDTO = await Http.GetFromJsonAsync<UserDTO>($"{UsersEndpoint}/{UserId}") ?? new();
            MainLayout.SetHeaderValue("View User");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching User details.", Severity.Error);
        }
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}
