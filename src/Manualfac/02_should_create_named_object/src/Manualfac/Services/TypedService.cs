using System;

namespace Manualfac.Services
{
    class TypedService : Service, IEquatable<TypedService>
    {
        #region Please modify the following code to pass the test

        Type type;
        /*
         * This class is used as a key for registration by type.
         */

        public TypedService(Type serviceType)
        {
            this.type = serviceType;
        }
        
        public bool Equals(TypedService other)
        {
            if (other == null)
            {
                return false;
            }
            return other.type == this.type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is TypedService)
            {
                return this.Equals((TypedService) obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode();
        }

        #endregion
    }
}