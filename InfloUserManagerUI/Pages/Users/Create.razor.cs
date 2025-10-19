namespace InfloUserManagerUI.Pages.Users;

public partial class Create
{
    protected override void OnInitialized()
    {
        UserDTO = new()
        {
            StatusId = SelectValue
        };
        MainLayout.SetHeaderValue("Create User");
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateUser()
    {
        var response = await Http.PostAsJsonAsync(UsersEndpoint, UserDTO);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User {UserDTO.Forename} {UserDTO.Surname} successfully created.", Severity.Success);
            NavigationManager.NavigateTo("users/index");
        }
        else
        {
            Snackbar.Add($" An error occurred creating User {UserDTO.Forename} {UserDTO.Surname}. Please try again.", Severity.Error);
        }
    }
}
