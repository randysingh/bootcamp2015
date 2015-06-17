using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using DemoApp.Common.Interfaces;

namespace DemoApp.Windows.Services
{
    public class WindowsStorageProvider : IStorageProvider
    {
        private const string PhotosPath = "Photos";
        public WindowsStorageProvider()
        {
            try
            {
                var storageFolder = ApplicationData.Current.LocalFolder;
                var photosFolder = storageFolder.GetFolderAsync(PhotosPath).GetAwaiter().GetResult();
            }
            catch (FileNotFoundException)
            {
                var storageFolder = ApplicationData.Current.LocalFolder;
                storageFolder.CreateFolderAsync(PhotosPath);
            }

        }
        public async Task<List<string>> GetAllAvailablePhotos()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var photosFolder = await storageFolder.GetFolderAsync(PhotosPath);
            var files = new List<string>();
            var photos = await photosFolder.GetFilesAsync();
            foreach (var photo in photos)
            {
                files.Add(photo.Path);
            }
            return files;
        }

        public async Task<bool> SavePhoto(IStorageFile file)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var photosFolder = await storageFolder.GetFolderAsync(PhotosPath);
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var newFile = await photosFolder.CreateFileAsync(file.Name);
                using (var newStream = await newFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    stream.AsStream().CopyTo(newStream.AsStream());
                    return true;
                }
            }
        }

        public async Task DeletePhoto(string path)
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(path);
                await DeletePhoto(file);
            }
            catch (FileNotFoundException e)
            {
                
            }

        }

        public async Task DeletePhoto(IStorageFile path)
        {
            await path.DeleteAsync();
        }
    }
}
