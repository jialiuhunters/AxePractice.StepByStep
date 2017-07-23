using System;
using System.Reflection;
using System.Linq;
using Manualfac.Services;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Please modify the following code to pass the test

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         * 
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            ConstructorInfo[] constructorInfos = serviceType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (constructorInfos.Length != 1)
            {
                throw new DependencyResolutionException();
            }
            ParameterInfo[] parameterInfos = constructorInfos[0].GetParameters();
            object[] parameters = parameterInfos
                .Select(p => componentContext.ResolveComponent(new TypedService(p.ParameterType)))
                .ToArray();
            return Activator.CreateInstance(serviceType, parameters);
        }

        #endregion
    }
}