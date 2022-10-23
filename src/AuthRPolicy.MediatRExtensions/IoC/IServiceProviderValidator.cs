using System.Reflection;

namespace AuthRPolicy.MediatRExtensions.IoC
{
    internal interface IServiceProviderValidator
    {
        void Validate(params Assembly[] assemblies);
    }
}