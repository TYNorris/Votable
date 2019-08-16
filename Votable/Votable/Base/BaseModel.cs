using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public abstract class BaseModel
    {
        public void AddEmptyProperties<T>(T other)
        {
            if(this.GetType().Equals(other.GetType()))
            {
                var type = typeof(T);
                var props = type.GetProperties();
                foreach(var p in props)
                {
                    if (p.GetValue(this) == null)
                    {
                        p.SetValue(this, p.GetValue(other));
                    }
                }
            }
        }
    }
}
