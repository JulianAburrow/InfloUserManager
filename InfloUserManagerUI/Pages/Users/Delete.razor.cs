namespace InfloUserManagerUI.Pages.Users;
public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserDTO = await Http.GetFromJsonAsync<UserDTO>($"{UsersEndpoint}/{UserId}") ?? new();
            MainLayout.SetHeaderValue("Delete User");
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
            GetCustomBreadcrumbItem("Delete"),
        ]);
    }

    private async Task DeleteUser()
    {
        var response = await Http.DeleteAsync($"{UsersEndpoint}/{UserId}");
        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User {UserDTO.Forename} {UserDTO.Surname} successfully deleted.", Severity.Success);
            NavigationManager.NavigateTo("/users/index");
        }
        else
        {
            Snackbar.Add($"An error occurred deleting User {UserDTO.Forename} {UserDTO.Surname}. Please try again.", Severity.Error);
        }
    }
}