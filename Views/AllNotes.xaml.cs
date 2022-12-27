
namespace OnaNotes.Views;

public partial class AllNotes : ContentPage
{
	public AllNotes()
	{
		InitializeComponent();
		BindingContext = new Models.AllNotes();
	}

    protected override void OnAppearing()
    {
		((Models.AllNotes)BindingContext).LoadNotes();
    }

	private async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NotePage));
	}

	private async void notesCollection_SelectionChanges(object sender, SelectionChangedEventArgs e)
	{
		if(e.CurrentSelection.Count != 0)
		{
			// Get current note model
			Models.Note note = (Models.Note)e.CurrentSelection[0];

			// Navigate to "NotePage?ItemId=path\on\devic\\sample-xyz.onasonic_notes.txt
			await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.FileName}");

			// UnNselect the UI
			notesCollection.SelectedItem = null;
        }
    }
}