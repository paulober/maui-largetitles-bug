using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace BugTest;

public class LargeTitleNavBarAppearanceTracker : IShellNavBarAppearanceTracker
{
    public void Dispose() { }

    public void ResetAppearance(UINavigationController controller) { }

    public void SetAppearance(UINavigationController controller, ShellAppearance appearance)
    {
        var navBar = controller.NavigationBar;

        // Big titles
        navBar.PrefersLargeTitles = true;

        // Translucent / no separator line
        var navBarAppearance = new UINavigationBarAppearance();
        navBarAppearance.ConfigureWithTransparentBackground();
        navBarAppearance.BackgroundColor = UIColor.Clear;
        navBarAppearance.ShadowColor = UIColor.Clear;   // remove bottom hairline
        navBarAppearance.BackgroundEffect = null;

        // Respect Shell colors if used
        if (appearance?.ForegroundColor is not null)
        {
            var fg = appearance.ForegroundColor;
            var uiFg = UIColor.FromRGBA((nfloat)fg.Red, (nfloat)fg.Green, (nfloat)fg.Blue, (nfloat)fg.Alpha);

            navBarAppearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = uiFg };
            navBarAppearance.LargeTitleTextAttributes = new UIStringAttributes { ForegroundColor = uiFg };
        }

        navBar.StandardAppearance = navBarAppearance;
        navBar.ScrollEdgeAppearance = navBarAppearance;

        // Force large titles on the current VC without touching ContentPages
        if (controller.TopViewController is { } vc)
            vc.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
    }

    public void UpdateLayout(UINavigationController controller) { }

    public void SetHasShadow(UINavigationController controller, bool hasShadow)
    {
        // we already removed shadow via ShadowColor = Clear
        // so ignore
    }
}
