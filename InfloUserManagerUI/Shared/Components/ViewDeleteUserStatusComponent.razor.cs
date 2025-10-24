namespace InfloUserManagerUI.Shared.Components;

public partial class ViewDeleteUserStatusComponent
{
    [Parameter] public UserStatusDTO UserStatusDTO { get; set; } = new();
}