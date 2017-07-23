using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ComponentContext : IComponentContext
    {
        #region Please modify the following code to pass the test

        Dictionary<Type, Func<IComponentContext, object>> funcs;
        /*
         * A ComponentContext is used to resolve a component. Since the component
         * is created by the ContainerBuilder, it brings all the registration
         * information. 
         * 
         * You can add non-public member functions or member variables as you like.
         */
        internal ComponentContext(Dictionary<Type, Func<IComponentContext, object>> funcs)
        {
            this.funcs = funcs;
        }

        public object ResolveComponent(Type type)
        {
            if (funcs.ContainsKey(type))
            {
                return funcs[type](this);
            }
            throw new DependencyResolutionException();
        }

        #endregion
    }
}