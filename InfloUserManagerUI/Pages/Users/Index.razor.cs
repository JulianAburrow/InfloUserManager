namespace InfloUserManagerUI.Pages.Users;

public partial class Index
{
    private List<UserDTO> UserDTOs = null!;
    private List<UserDTO> FilteredUserDTOs = null!;
    private List<UserStatusDTO> UserStatusDTOs = null!;
    private int SelectedFilterOption = 0;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserStatusDTOs = await Http.GetFromJsonAsync<List<UserStatusDTO>>(UserStatusesEndpoint) ?? [];
            UserStatusDTOs.Insert(0, new UserStatusDTO { StatusId  = AllValue, StatusName = All });
            UserDTOs = await Http.GetFromJsonAsync<List<UserDTO>>(UsersEndpoint) ?? [];
            FilteredUserDTOs = UserDTOs;
            Snackbar.Add($"{UserDTOs.Count} item(s) found.", UserDTOs.Count > 0 ? Severity.Info : Severity.Warning);
            MainLayout.SetHeaderValue("Users");
        }
        catch
        {
            Snackbar.Add("An error occurred fetching Users.", Severity.Error);
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        MainLayout.SetBreadCrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("Users")
        ]);
    }

    private async Task FilterUsers()
    {
        FilteredUserDTOs = SelectedFilterOption switch
        {
            1 or 2 => UserDTOs.Where(u => u.StatusId == SelectedFilterOption).ToList(),
            _ => UserDTOs,
        };
        await InvokeAsync(StateHasChanged);
    }
}
