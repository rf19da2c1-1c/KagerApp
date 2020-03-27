using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassDemoKageLib.model;

namespace KagerApp.model.persistence
{
    class SimplePersistence:IPersistence
    {
        // simple list in memory  disappear when shutting down the app
        private static List<Kage> _kager;

        public SimplePersistence()
        {
            _kager = populateKager();
        }

        

        public async Task<ICollection<Kage>> LoadKager()
        {
            return new List<Kage>(_kager);
        }

        public void SaveKager(ICollection<Kage> kager)
        {
            _kager.Clear();
            foreach (Kage kage in kager)
            {
                _kager.Add(kage);
            }
        }

        public bool UpdateKage(Kage kage)
        {
            Kage kageToBeUpdated = _kager.Find(k => k.Id == kage.Id);
            if (kageToBeUpdated == null)
            {
                return false; 
            }

            kageToBeUpdated.Name = kage.Name;
            kageToBeUpdated.Price = kage.Price;
            kageToBeUpdated.NoOfPieces = kage.NoOfPieces;

            return true;
        }

        public bool CreateKage(Kage kage)
        {
            _kager.Add(kage);
            return true;
        }

        public Kage DeleteKage(Kage kage)
        {
            Kage kageToBeDeleted = _kager.Find(k => k.Id == kage.Id);
            _kager.Remove(kageToBeDeleted);
            return kageToBeDeleted;
        }




        private List<Kage> populateKager()
        {
            List<Kage> initialKager = new List<Kage>();

            initialKager.Add(new Kage("snegl", 15.5, 1, 1));
            initialKager.Add(new Kage("sandkage", 45, 8, 2));
            initialKager.Add(new Kage("dagmar", 25, 6, 3));
            initialKager.Add(new Kage("romkugle", 12, 1, 4));
            initialKager.Add(new Kage("kajkage", 21, 1, 5));




            return initialKager;
        }
    }
}
