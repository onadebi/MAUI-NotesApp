
namespace OnaNotes.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

	public async void LearnMore_Clicked(object sender, EventArgs e)
	{
		await Launcher.Default.OpenAsync("https://onasonic.com");
	}
}