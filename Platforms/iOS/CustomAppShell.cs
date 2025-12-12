using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace BugTest;

public class CustomAppShell : ShellRenderer
{
    protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
    {
        return new LargeTitleNavBarAppearanceTracker();
    }
}
