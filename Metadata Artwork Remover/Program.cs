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

            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "إختر مجلد الملفات الصوتية\nSelect the audio files folder",
                ShowNewFolderButton = false
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;

            string[] Mp3Files = System.IO.Directory.GetFiles(fbd.SelectedPath, "*.mp3", System.IO.SearchOption.AllDirectories);
            
            foreach (string file in Mp3Files)
                RemoveImageFromAudioFile(file);

            MessageBox.Show($"لقد تمت العملية بنجاح.\nعدد الملفات: {Mp3Files.Length}");
        }

        public static void RemoveImageFromAudioFile(string mp3File)
        {
            if (System.IO.File.Exists(mp3File))
            {
                using (var file = TagLib.File.Create(mp3File))
                {
                    file.Tag.Pictures = new TagLib.IPicture[0];
                    file.Save();
                }
            }
        }
    }
}
