using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KagerApp.model.persistence
{
    public enum PersistenceType
    {
        Simple,
        File,
        Database
    };

    static class PersitenceFactory
    {
        public static IPersistence GetPersistency(PersistenceType peristenceType)
        {
            switch (peristenceType)
            {
                case PersistenceType.Simple: return new SimplePersistence();

                // File
                case PersistenceType.File: return new FilePersistence();

                // Database
                case PersistenceType.Database: return new DBPersistence();

                default:return new SimplePersistence();
            }


        }
    }
}
