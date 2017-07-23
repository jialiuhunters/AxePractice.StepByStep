using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ContainerBuilder
    {
        #region Please modify the following code to pass the test

        Dictionary<Type, Func<IComponentContext, object>> funcs = new Dictionary<Type, Func<IComponentContext, object>>();

        bool hasBeenBuilt = false;
        /*
         //* Hello, boys and girls. The container builder is a very good guy to store
         * all the definitions for instantiation as well as lifetime managing. Now,
         * let's forget about lifetime management.
         * 
         * ContainerBuilder however, has no idea how to create an instance. So it is
         * the users' job (func). We just store the procedure and call it when needed.
         * 
         * You can add non-public member functions or member variables as you like.
         */

        public void Register<T>(Func<IComponentContext, T> func)
        {
            this.funcs[typeof(T)] = a => func(a);
        }

        public IComponentContext Build()
        {
            if (hasBeenBuilt)
            {
                throw new InvalidOperationException();
            }
            hasBeenBuilt = true;
            return new ComponentContext(this.funcs);
        }

        #endregion
    }
}