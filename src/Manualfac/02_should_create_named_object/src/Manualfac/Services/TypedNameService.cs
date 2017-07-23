using System;

namespace Manualfac.Services
{
    class TypedNameService : Service, IEquatable<TypedNameService>
    {
        #region Please modify the following code to pass the test

        Type type;
        string name;
        /*
         * This class is used as a key for registration by both type and name.
         */

        public TypedNameService(Type serviceType, string name)
        {
            this.type = serviceType;
            this.name = name;
        }

        public bool Equals(TypedNameService other)
        {
            if (other == null)
            {
                return false;
            }
            return this.type == other.type && this.name == other.name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is TypedNameService)
            {
                return Equals((TypedNameService) obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode() ^ this.name.GetHashCode();
        }

        #endregion
    }
}