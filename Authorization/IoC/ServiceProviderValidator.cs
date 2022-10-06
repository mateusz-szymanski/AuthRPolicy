using System;

namespace Authorization.IoC
{
    class ServiceProviderValidator : IServiceProviderValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Validate()
        {
            // TODO: find all implementations of IAccessPolicy and make sure there is exactly one checker for each
            // TODO: find all mediatR request, make sure they have IAuthorizedRequest implemented - add configuration: RequireAllRequestsToBeAuthorized
        }
    }
}
