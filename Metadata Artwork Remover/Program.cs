using System;
using System.Windows.Forms;

namespace Metadata_Artwork_Remover
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!System.IO.File.Exists("TagLibSharp.dll"))
            {
                MessageBox.Show("TagLibSharp.dll file is missing.");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "إختر مجلد الملفات الصوتية\nSelect the audio files folder",
                ShowNewFolderButton = false
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;

            string[] Mp3Files = System.IO.Directory.GetFiles(fbd.SelectedPath, "*.mp3", System.IO.SearchOption.AllDirectories);

            foreach (string path in Mp3Files)
            {
                using (var file = TagLib.File.Create(path))
                {
                    file.Tag.Pictures = new TagLib.IPicture[0];
                    file.Save();
                }
            }

            MessageBox.Show($"تمت العملية بنجاح.\nعدد الملفات: {Mp3Files.Length}");
        }

    }
}
