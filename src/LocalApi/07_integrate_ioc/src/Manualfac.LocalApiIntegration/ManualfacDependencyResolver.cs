﻿using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    public class ManualfacDependencyResolver : IDependencyResolver
    {
        #region Please implement the following class

        Container container;
        /*
         * We should create a manualfac dependency resolver so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */

        public ManualfacDependencyResolver(Container rootScope)
        {
            this.container = rootScope;
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public object GetService(Type type)
        {
            return this.container.Resolve(type);
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(container.BeginLifetimeScope());
        }

        #endregion
    }
}