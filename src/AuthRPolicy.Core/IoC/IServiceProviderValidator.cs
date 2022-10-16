using System.Reflection;

namespace AuthRPolicy.Core.IoC
{
    internal interface IServiceProviderValidator
    {
        void Validate(params Assembly[] assemblies);
    }
}