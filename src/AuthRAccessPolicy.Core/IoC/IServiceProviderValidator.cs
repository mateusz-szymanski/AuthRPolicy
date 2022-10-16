using System.Reflection;

namespace AuthRAccessPolicy.Core.IoC
{
    internal interface IServiceProviderValidator
    {
        void Validate(params Assembly[] assemblies);
    }
}