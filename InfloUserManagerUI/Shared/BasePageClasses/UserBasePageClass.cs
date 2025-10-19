namespace InfloUserManagerUI.Shared.BasePageClasses;

public class UserBasePageClass : BasePageClass
{
    [Parameter]
    public int UserId { get; set; }

    protected UserDTO UserDTO = null!;

    protected bool UserExists;

    protected BreadcrumbItem GetUserHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new("Users", "/Users/index", isDisabled);
    }
}
