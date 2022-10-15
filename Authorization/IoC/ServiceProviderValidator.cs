﻿using Authorization.AccessPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Authorization.IoC
{
    // TODO:
    class ServiceProviderValidator : IServiceProviderValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Validate(params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetTypes());
            var accessPolicyTypes = allTypes.Where(t => t.IsAssignableTo(typeof(IAccessPolicy)));
            var accessPolicyCheckerInterfaceTypes = accessPolicyTypes.Select(apt => typeof(IAccessPolicyChecker<>).MakeGenericType(apt));

            foreach (var accessPolicyCheckerInterfaceType in accessPolicyCheckerInterfaceTypes)
            {
                var implementation = allTypes
                    .Where(t => DoesTypeSupportInterface(t, accessPolicyCheckerInterfaceType));

                if (implementation.Count() != 1)
                    throw new Exception($"There must exactly one implementation for {accessPolicyCheckerInterfaceType.Name}"); // TODO: Custom exception
            }

            // TODO: find all implementations of IAccessPolicy and make sure there is exactly one checker for each
            // TODO: find all mediatR request, make sure they have IAuthorizedRequest implemented - add configuration: RequireAllRequestsToBeAuthorized
        }

        private static bool DoesTypeSupportInterface(Type type, Type interfaceType)
        {
            if (interfaceType.IsAssignableFrom(type))
                return true;
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                return true;

            return false;
        }
    }
}
