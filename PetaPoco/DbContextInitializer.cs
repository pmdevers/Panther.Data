using System;

namespace Panther.Data.PetaPoco
{
    public class DbContextInitializer
    {
        private static readonly object SyncLock = new object();
        private static DbContextInitializer _instance;

        protected DbContextInitializer(){}

        private bool _isInitialized = false;

        public static DbContextInitializer Instance()
        {
            if (_instance == null)
            {
                lock(SyncLock)
                {
                    if(_instance == null)
                    {
                        _instance = new DbContextInitializer();
                    }
                }
            }

            return _instance;
        }

        public void InitializeDbContextOnce(Action initMethod)
        {
            lock(SyncLock)
            {
                if(!_isInitialized)
                {
                    initMethod();
                    _isInitialized = true;
                }
            }
        }

    }
}
