namespace OnaNotes.Views;


[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    //string _fileName = Path.Combine(FileSystem.AppDataDirectory, "onasonic_notes.txt");
    public NotePage()
    {
        InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFile = $"{Path.GetRandomFileName()}.onasonic_notes.txt";
        LoadNote(Path.Combine(appDataPath, randomFile));
    }

    public async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if(BindingContext is Models.Note note)
        {
            if (txtEditor.Text.Trim().Length < 3)
            {
                await DisplayAlert("Warning", "Note must be a minimum of 3 characters long", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                File.WriteAllText(note.FileName, txtEditor.Text);
                await DisplayAlert("Notification", "Note status updated successfully.", "Ok");
                await Shell.Current.GoToAsync("..");
            }
        }
       
    }
    public void txtEditor_TextChanged(object sender, EventArgs e)
    {
        lblEditorWordCount.Text = txtEditor.Text.Count().ToString();
    }

    public async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var confirmDelete = await DisplayAlert("Confirm", "This process is not reversible. Do you want to proceed?", accept: "Ok", cancel: "Cancel");
        if(BindingContext is Models.Note note)
        {
            if (confirmDelete)
            {
                if (File.Exists(note.FileName))
                {
                    File.Delete(note.FileName);
                    await DisplayAlert("Alert", "Note file deleted successfully", cancel: "Ok");
                }
                txtEditor.Text = string.Empty;
                lblEditorWordCount.Text = "0";
                
                await Shell.Current.GoToAsync("..");
            }
        }
        
    }

    #region HELPERS

    public string ItemId
    {
        set
        {
            LoadNote(value);
        }
    }
    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.FileName = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Text = File.ReadAllText(fileName);
            noteModel.Date = File.GetCreationTimeUtc(fileName);
        }
        BindingContext = noteModel;
    }
    #endregion
}