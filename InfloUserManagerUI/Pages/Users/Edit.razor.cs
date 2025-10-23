namespace InfloUserManagerUI.Pages.Users;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserDTO = await Http.GetFromJsonAsync<UserDTO>($"{UsersEndpoint}/{UserId}") ?? new();
            MainLayout.SetHeaderValue("Edit User");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching User details.", Severity.Error);
        }

        UserNumberExists = false;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetUserHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("Edit"),
        ]);
    }

    private async Task UpdateUser()
    {
        UserNumberExists = false;

        await CheckForExistingUsersAsync();

        if (UserNumberExists)
        {
            Snackbar.Add($"A User with the User Number {UserDTO.UserNumber} already exists. Please choose an unique User Number.", Severity.Error);
            return;
        }

        var response = await Http.PutAsJsonAsync($"{UsersEndpoint}/{UserId}", UserDTO);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"User {UserDTO.Forename} {UserDTO.Surname} successfully updated.", Severity.Success);
            NavigationManager.NavigateTo("/users/index");
        }
        else
        {
            Snackbar.Add($"An error occurred updating User {UserDTO.Forename} {UserDTO.Surname}. Please try again.", Severity.Error);
        }
    }
}