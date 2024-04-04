using System.Linq.Expressions;

namespace GetPhone.Database.Interfaces;

public interface IRepository<TEntity> {
    List<TEntity> GetAll();
    List<TEntity> GetAll(Expression<Func<TEntity, object>>[] includeProperties);
    TEntity? GetById(int id);
    TEntity? GetById(int id, Expression<Func<TEntity, object>>[] includeProperties);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includeProperties);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Save();
}