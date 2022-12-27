using OnaNotes.Views;

namespace OnaNotes;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(NotePage),typeof(NotePage));
	}
}
