using System.Reflection;

namespace Authorization.IoC
{
    internal interface IServiceProviderValidator
    {
        void Validate(params Assembly[] assemblies);
    }
}