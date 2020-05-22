using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NHibernate.Proxy;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Extensions
{
    public static class NhibernateSetExtensions
    {
		public static T CastEntity<T>(this object entity) where T : class
    	{
    		var proxy = entity as INHibernateProxy;
    		if (proxy != null)
    		{
    			return proxy.HibernateLazyInitializer.GetImplementation() as T;
    		}
    		else
    		{
    			return entity as T;
    		}
    	}
    }
}
