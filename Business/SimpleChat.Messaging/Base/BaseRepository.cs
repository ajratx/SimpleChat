namespace SimpleChat.Messaging.Base
{
    using System;

    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected BaseRepository(IDatabaseSettings databaseSettings)
        {
            CheckSettings(databaseSettings);
            DatabaseSettings = databaseSettings;
        }

        protected IDatabaseSettings DatabaseSettings { get; }

        public abstract TEntity GetById(TKey id);

        public abstract void Add(params TEntity[] items);

        public abstract void Update(params TEntity[] items);

        public abstract void Remove(params TEntity[] items);

        public abstract void Save();

        private static void CheckSettings(IDatabaseSettings databaseSettings)
        {
            if (databaseSettings == null)
                throw new ArgumentNullException(nameof(databaseSettings));

            if (string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
                throw new ArgumentException(
                    "Parameter can't be null or white space",
                    nameof(databaseSettings.ConnectionString));
        }
    }
}
