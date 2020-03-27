using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClassDemoKageLib.model;
using Newtonsoft.Json;

namespace KagerApp.model.persistence
{
    public class DBPersistence:IPersistence
    {
        private string URI = "http://localhost:49258/api/Kager/";
        public async Task<ICollection<Kage>> LoadKager()
        {
            List<Kage> liste = new List<Kage>();

            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(URI);
                liste = JsonConvert.DeserializeObject<List<Kage>>(json);
            }

            return liste;
        }

        public void SaveKager(ICollection<Kage> kager)
        {
            throw new NotImplementedException();
        }

        public bool UpdateKage(Kage kage)
        {
            throw new NotImplementedException();
        }

        public bool CreateKage(Kage kage)
        {
            throw new NotImplementedException();
        }

        public Kage DeleteKage(Kage kage)
        {
            throw new NotImplementedException();
        }
    }
}
