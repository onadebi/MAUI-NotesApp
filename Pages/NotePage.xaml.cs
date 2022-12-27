namespace OnaNotes.Pages;

public partial class NotePage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "onasonic_notes.txt");
    public NotePage()
    {
        InitializeComponent();
        if (File.Exists(_fileName))
        {
            txtEditor.Text = File.ReadAllText(_fileName);
        }
    }

    public async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (txtEditor.Text.Trim().Length < 3)
        {
            await DisplayAlert("Warning", "Note must be a minimum of 3 characters long", "OK");
            return;
        }
        else
        {
            File.WriteAllText(_fileName, txtEditor.Text);
            await DisplayAlert("Notification", "Note status updated successfully.", "Ok");
        }
    }
    public void txtEditor_TextChanged(object sender, EventArgs e)
    {
        lblEditorWordCount.Text = txtEditor.Text.Count().ToString();
    }

    public async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var confirmDelete = await DisplayAlert("Confirm", "This process is not reversible. Do you want to proceed?", accept: "Ok", cancel: "Cancel");
        if (confirmDelete)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
                await DisplayAlert("Alert", "Note file deleted successfully", cancel: "Ok");
            }
            txtEditor.Text = string.Empty;
            lblEditorWordCount.Text = "0";
        }
    }
}