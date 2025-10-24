namespace InfloUserManagerUI.Shared.Components;

public partial class CreateEditUserStatusComponent
{
    [Parameter]
    public UserStatusDTO UserStatusDTO { get; set; } = new();
}