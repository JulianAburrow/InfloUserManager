namespace InfloUserManagerUI.Shared.Components;

public partial class ListUserStatusesComponent
{
    [Parameter] public List<UserStatusDTO> UserStatusDTOs { get; set; } = new();
}