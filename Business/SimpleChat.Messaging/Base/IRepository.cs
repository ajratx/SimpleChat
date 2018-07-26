namespace SimpleChat.Messaging.Base
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        TEntity GetById(TKey id);

        void Add(params TEntity[] items);

        void Update(params TEntity[] items);

        void Remove(params TEntity[] items);

        void Save();
    }
}
