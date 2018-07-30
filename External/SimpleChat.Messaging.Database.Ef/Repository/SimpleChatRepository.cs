namespace SimpleChat.Messaging.Database.Ef.Repository
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;

    public abstract class SimpleChatRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>
        where TEntity : class
    {
        private bool disposed;

        protected SimpleChatRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
            var dbContextOptions = GetDbContextOptions();
            if (dbContextOptions == null)
                throw new InvalidOperationException("Data base context options can't be null");

            SimpleChatContext = new SimpleChatContext(dbContextOptions);
            SimpleChatContext.Database.EnsureCreated();
        }

        protected SimpleChatContext SimpleChatContext { get; }

        public override TEntity GetById(TKey id)
        {
            CheckDisposed();
            return SimpleChatContext.Set<TEntity>()?.Find(id);
        }

        public override void Add(params TEntity[] entities)
        {
            CheckDisposed();
            SimpleChatContext.Set<TEntity>().AddRange(entities);
        }

        public override void Update(params TEntity[] entities)
        {
            CheckDisposed();
            SimpleChatContext.Set<TEntity>().UpdateRange(entities);
        }

        public override void Remove(params TEntity[] entities)
        {
            CheckDisposed();
            SimpleChatContext.Set<TEntity>().RemoveRange(entities);
        }

        public override void Save()
        {
            CheckDisposed();
            SimpleChatContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void CheckDisposed()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }

        protected abstract DbContextOptions<SimpleChatContext> GetDbContextOptions();

        protected void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) SimpleChatContext.Dispose();

            disposed = true;
        }
    }
}
