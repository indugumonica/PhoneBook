using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PhoneBook
{
    public static class FileHelper
    {
        public static async Task WriteTextFileAsync(string filename, string content)
        {
            var localfolder = ApplicationData.Current.LocalFolder;
            var textFile = await localfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            var textWriter = new DataWriter(textStream);
            textWriter.WriteString(content);
            await textWriter.StoreAsync();
            await textWriter.FlushAsync();
            textWriter.DetachStream();
            textStream.Dispose();
            textWriter.Dispose();
        }

        public static async Task<string> ReadTextFileAsync(string filename)
        {
            var localfolder = ApplicationData.Current.LocalFolder;
            var textFile = await localfolder.GetFileAsync(filename);
            var textstream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textstream);
            var textLength = textstream.Size;
            await textReader.LoadAsync((uint)textLength);
            return textReader.ReadString((uint)(textLength));
        }

    }

}
