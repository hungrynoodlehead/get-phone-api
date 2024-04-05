using System.Linq.Expressions;
using GetPhone.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GetPhone.Database;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Model {
    private readonly ApplicationContext context;
    private readonly DbSet<TEntity> dbSet;

    public Repository(ApplicationContext context){
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll(){
        return dbSet;
    }
    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includeProperties) {
        IQueryable<TEntity> query = dbSet;
        foreach(var includeProperty in includeProperties){
            query = query.Include(includeProperty);
        }
        return query;
    }

    public TEntity? GetById(int id){
        return dbSet.FirstOrDefault(e => e.Id == id);
    }
    public TEntity? GetById(int id, Expression<Func<TEntity, object>>[] includeProperties){
        IQueryable<TEntity> query = dbSet;
        foreach(var includeProperty in includeProperties){
            query = query.Include(includeProperty);
        }
        return query.FirstOrDefault(e => e.Id == id);
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate){
        return dbSet.FirstOrDefault(predicate);
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includeProperties){
        IQueryable<TEntity> query = dbSet;
        foreach(var includeProperty in includeProperties){
            query = query.Include(includeProperty);
        }
        return query.FirstOrDefault(predicate);
    }

    public void Add(TEntity entity) {
        dbSet.Add(entity);
    }

    public void Update(TEntity entity){
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity){
        if (context.Entry(entity).State == EntityState.Detached) {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    public void Save(){
        context.SaveChanges();
    }
}