using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassDemoKageLib.model;

namespace KagerApp.model.persistence
{
    interface IPersistence
    {
        Task<ICollection<Kage>> LoadKager();
        void SaveKager(ICollection<Kage> kager);

        bool UpdateKage(Kage kage);

        bool CreateKage(Kage kage);

        Kage DeleteKage(Kage kage); 
    }
}
