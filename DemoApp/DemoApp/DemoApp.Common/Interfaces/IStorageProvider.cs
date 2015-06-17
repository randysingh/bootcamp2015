using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace DemoApp.Common.Interfaces
{
    public interface IStorageProvider
    {
        Task<List<string>> GetAllAvailablePhotos();
        Task<bool> SavePhoto(IStorageFile storageFile);
        Task DeletePhoto(string path);
        Task DeletePhoto(IStorageFile path);
    }
}
