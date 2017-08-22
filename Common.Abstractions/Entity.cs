using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Abstractions.Entities;

namespace Common.Abstractions
{
    public abstract class Entity<TId> : IHaveId<TId>, IEquatable<Entity<TId>>
        where TId : struct 
    {
        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The Id can't be the default value.", nameof(id));
            }

            this.Id = id;
        }

        public Entity()
        { 

        }

        public TId Id { get; protected set; }

        public  bool IsDeleted { get; protected set; }

        public virtual void Delete()
        {
            this.IsDeleted = true;
        }

        public virtual void Restore()
        {
            this.IsDeleted = false;
        }

        public override bool Equals(object otherObject)
        {
            var entity = otherObject as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }

            return base.Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }
    }
}
