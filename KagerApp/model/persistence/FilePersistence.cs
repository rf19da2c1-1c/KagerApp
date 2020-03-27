using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http.Headers;
using ClassDemoKageLib.model;
using Newtonsoft.Json;

namespace KagerApp.model.persistence
{
    class FilePersistence:IPersistence
    {
        private static StorageFolder folder = ApplicationData.Current.LocalFolder;
        private const String FileName = "KagerFil.json";
        private List<Kage> _cacheKager = new List<Kage>();


        public async Task<ICollection<Kage>> LoadKager()
        {
            if (await DoesExists(FileName))
            {
                StorageFile dataFile = await folder.GetFileAsync(FileName);
                string dataJSON = await FileIO.ReadTextAsync(dataFile);

                _cacheKager = JsonConvert.DeserializeObject<List<Kage>>(dataJSON);
            }

            _cacheKager = (_cacheKager == null)? new List<Kage>() : new List<Kage>(_cacheKager);
            return _cacheKager;
        }

        public async void SaveKager(ICollection<Kage> kager)
        {
            _cacheKager = new List<Kage>(kager);

            StorageFile file;
            if (await DoesExists(FileName))
            {
                file = await folder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                file = await folder.CreateFileAsync(FileName);
            }
            
            await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(kager));
        }

        public bool UpdateKage(Kage kage)
        {
            Kage fundetkage = _cacheKager.Find(k => k.Id == kage.Id);

            if (fundetkage != null)
            {
                fundetkage.Name = kage.Name;
                fundetkage.Price = kage.Price;
                fundetkage.NoOfPieces = kage.NoOfPieces;

                SaveKager(_cacheKager);
                return true;
            }

            return false;
        }

        public bool CreateKage(Kage kage)
        {
            _cacheKager.Add(kage);
            SaveKager(_cacheKager);
            return true;
        }

        public Kage DeleteKage(Kage kage)
        {
            Kage fundetkage = _cacheKager.Find(k => k.Id == kage.Id);

            if (fundetkage != null)
            {
                _cacheKager.Remove(fundetkage);
                SaveKager(_cacheKager);
            }

            return kage;
        }


        private async Task<bool> DoesExists(String filename)
        {
            bool exists = false;
            IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
            foreach (StorageFile file in files)
            {
                if (file.Name == filename)
                {
                    return true; // exists 
                }
            }
            return exists;
        }
    }
}
