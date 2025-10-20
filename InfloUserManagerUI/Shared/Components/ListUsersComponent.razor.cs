namespace InfloUserManagerUI.Shared.Components;

public partial class ListUsersComponent
{
    [Parameter] public List<UserDTO> UserDTOs { get; set; } = new();
}
