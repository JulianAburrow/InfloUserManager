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