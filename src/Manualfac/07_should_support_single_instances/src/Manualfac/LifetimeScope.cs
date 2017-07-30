using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class LifetimeScope : Disposable, ILifetimeScope
    {
        readonly ComponentRegistry componentRegistry;

        // The shared instaces dicationary caches the reusable resolved instances.
        readonly Dictionary<Service, object> sharedInstances = new Dictionary<Service, object>();
        public Disposer Disposer { get; } = new Disposer();
        public ILifetimeScope RootScope { get; }

        public LifetimeScope(ComponentRegistry componentRegistry)
            : this(componentRegistry, null)
        {
        }

        public LifetimeScope(ComponentRegistry componentRegistry, ILifetimeScope parent)
        {
            if (componentRegistry == null) { throw new ArgumentNullException(nameof(componentRegistry));}

            this.componentRegistry = componentRegistry;

            #region Please initialize root scope

            RootScope = parent?.RootScope ?? parent;
            #endregion
        }

        public object ResolveComponent(Service service)
        {
            if (IsDisposed) { throw new ObjectDisposedException("I am dead~"); }
            if (service == null) { throw new ArgumentNullException(nameof(service)); }

            ComponentRegistration componentRegistration = GetComponentRegistration(service);
            ILifetimeScope lifetimeScope = componentRegistration.Lifetime.FindLifetimeScope(this);

            return lifetimeScope.GetCreateShare(componentRegistration);
        }

        public object GetCreateShare(ComponentRegistration registration)
        {
            /*
             * There is a missing concept in lifetime scope is instance storge. Currently, part of this
             * function is provided by ResolveComponent method. But Resolving is not equivalent with 
             * storage. Resolving means that it will not only creat the instance, but store it to 
             * correct lifetime scope (may be current one, and may be not) as well. And this is why
             * we extract the method.
             * 
             * This method will create, track and cache(if needed) instance in current lifetime scope.
             * Simple enough huh? The Sharing property will help you to determine whether the activated
             * instace be shared.
             */
            #region Please implement this method
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }
            if (registration.Sharing != InstanceSharing.Shared)
            {
                var instance = registration.Activator.Activate(this);
                this.Disposer.AddItemsToDispose(instance);
                return instance;
            }
            if (!this.sharedInstances.ContainsKey(registration.Service) || this.sharedInstances[registration.Service] == null)
            {
                var instance = registration.Activator.Activate(this);
                this.sharedInstances[registration.Service] = instance;
                this.Disposer.AddItemsToDispose(instance);
                return instance;
            }
            return this.sharedInstances[registration.Service];

            #endregion
        }

        public ILifetimeScope BeginLifetimeScope()
        {
            #region Please implement the method

            /*
             * Create a child life-time scope in this method.
             */
            return new LifetimeScope(this.componentRegistry, this);
            #endregion
        }

        ComponentRegistration GetComponentRegistration(Service service)
        {
            #region Please implement the method

            /*
             * This method will try get component registration from component registry.
             * We extract this method for isolation of responsibility.
             */
            ComponentRegistration componentRegistration;
            if (!this.componentRegistry.TryGetRegistration(service, out componentRegistration))
            {
                throw new DependencyResolutionException("component registration not found");
            }
            return componentRegistration;

            #endregion
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}