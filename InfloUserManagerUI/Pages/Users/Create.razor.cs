namespace InfloUserManagerUI.Pages.Users;

public partial class Create
{
    protected override void OnInitialized()
    {
        UserDTO.StatusId = SelectValue;
        MainLayout.SetHeaderValue("Create User");
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);

        UserNumberExists = false;
    }

    private async Task CreateUser()
    {
        UserNumberExists = false;

        await CheckForExistingUsersAsync();

        if (UserNumberExists)
        {
            Snackbar.Add($"A User with the User Number {UserDTO.UserNumber} already exists. Please choose an unique User Number.", Severity.Error);
            return;
        }

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
