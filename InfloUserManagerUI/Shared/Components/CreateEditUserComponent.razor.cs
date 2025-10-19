namespace InfloUserManagerUI.Shared.Components;

public partial class CreateEditUserComponent
{
    [Parameter]
    public UserDTO UserDTO { get; set; } = new();

    [Inject]
    public HttpClient Http { get; set; } = default!;

    private List<UserStatusDTO> UserStatusDTOs = [];

    protected override async Task OnInitializedAsync()
    {
        UserStatusDTOs = await Http.GetFromJsonAsync<List<UserStatusDTO>>(UserStatusesEndpoint) ?? [];
        UserStatusDTOs.Insert(0, new UserStatusDTO { StatusId = GlobalValues.SelectValue, StatusName = GlobalValues.Select });

        await InvokeAsync(StateHasChanged);
    }
}
