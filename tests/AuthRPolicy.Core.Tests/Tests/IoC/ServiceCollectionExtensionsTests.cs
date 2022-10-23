using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.IoC;
using AuthRPolicy.Core.IoC.Exceptions;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Services;
using AuthRPolicy.Core.TestAssemblies.EmptyAssembly;
using AuthRPolicy.Core.TestAssemblies.MissingChecker;
using AuthRPolicy.Core.TestAssemblies.ValidCheckers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests.IoC
{
    public class ServiceCollectionExtensionsTests
    {
        public class NullRoleProvider : IRoleProvider
        {
            public IEnumerable<Role> GetAvailableRoles() => throw new NotImplementedException();
            public IEnumerable<IPermission> GetPermissionsForRole(Role role) => throw new NotImplementedException();
        }

        #region AddAuthorizationWithRoleProvider

        [Fact]
        public void AddAuthorizationWithRoleProvider_ShouldRegisterServices_GivenNullRoleProviderAndEmptyAssembly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorization<NullRoleProvider>(typeof(EmptyAssemblyClass).Assembly);

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IRoleProvider) &&
                sd.ImplementationType == typeof(NullRoleProvider) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.DoesNotContain(services, sd =>
                sd.ServiceType.IsGenericType &&
                sd.ServiceType.GetGenericTypeDefinition() == typeof(IAccessPolicyChecker<>) &&
                sd.ImplementationType != typeof(EmptyAccessPolicyChecker));
        }

        [Fact]
        public void AddAuthorizationWithRoleProvider_ShouldRegisterServices_GivenNullRoleProviderAndNoAssembly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorization<NullRoleProvider>();

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IRoleProvider) &&
                sd.ImplementationType == typeof(NullRoleProvider) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.DoesNotContain(services, sd =>
                sd.ServiceType.IsGenericType &&
                sd.ServiceType.GetGenericTypeDefinition() == typeof(IAccessPolicyChecker<>) &&
                sd.ImplementationType != typeof(EmptyAccessPolicyChecker));
        }

        [Fact]
        public void AddAuthorizationWithRoleProvider_ShouldThrowIncorrectAccessPolicyCheckerDefinitionException_GivenMissingCheckerClass()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            // Assert
            Assert.Throws<IncorrectAccessPolicyCheckerDefinitionException>(() =>
                services.AddAuthorization<NullRoleProvider>(typeof(MissingCheckerClass).Assembly)
            );
        }

        [Fact]
        public void AddAuthorizationWithRoleProvider_ShouldRegisterServices_GivenValidCheckersAssembly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorization<NullRoleProvider>(typeof(ValidCheckersClass).Assembly);

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IRoleProvider) &&
                sd.ImplementationType == typeof(NullRoleProvider) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<TestAssemblies.ValidCheckers.Policy1.AccessPolicy1>) &&
                sd.ImplementationType == typeof(TestAssemblies.ValidCheckers.Policy1.AccessPolicy1Checker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<TestAssemblies.ValidCheckers.Policy2.AccessPolicy2>) &&
                sd.ImplementationType == typeof(TestAssemblies.ValidCheckers.Policy2.AccessPolicy2Checker) &&
                sd.Lifetime == ServiceLifetime.Scoped);
        }

        #endregion

        #region AddAuthorizationWithOptions

        [Fact]
        public void AddAuthorizationWithOptions_ShouldRegisterServices_GivenEmptyAssembly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorization(options =>
            {
                options.Assemblies = new[] { typeof(EmptyAssemblyClass).Assembly };
            });

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IRoleProvider) &&
                sd.ImplementationInstance is not null &&
                sd.ImplementationInstance.GetType() == typeof(DefaultRoleProvider) &&
                sd.Lifetime == ServiceLifetime.Singleton);

            Assert.DoesNotContain(services, sd =>
                sd.ServiceType.IsGenericType &&
                sd.ServiceType.GetGenericTypeDefinition() == typeof(IAccessPolicyChecker<>) &&
                sd.ImplementationType != typeof(EmptyAccessPolicyChecker));
        }

        [Fact]
        public void AddAuthorizationWithOptions_ShouldRegisterServices_GivenNoAssembly()
        {
            // Arrange
            var services = new ServiceCollection();
            IDefaultRoleProviderBuilder? roleProviderBuilder = null;

            // Act
            services.AddAuthorization(options => { roleProviderBuilder = options.RolesBuilder; });

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IRoleProvider) &&
                sd.ImplementationInstance is not null &&
                sd.ImplementationInstance.GetType() == typeof(DefaultRoleProvider) &&
                sd.ImplementationInstance == roleProviderBuilder?.Build() &&
                sd.Lifetime == ServiceLifetime.Singleton);

            Assert.DoesNotContain(services, sd =>
                sd.ServiceType.IsGenericType &&
                sd.ServiceType.GetGenericTypeDefinition() == typeof(IAccessPolicyChecker<>) &&
                sd.ImplementationType != typeof(EmptyAccessPolicyChecker));
        }

        [Fact]
        public void AddAuthorizationWithOptions_ShouldThrowIncorrectAccessPolicyCheckerDefinitionException_GivenMissingCheckerClass()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            // Assert
            Assert.Throws<IncorrectAccessPolicyCheckerDefinitionException>(() =>
                services.AddAuthorization(options => { options.Assemblies = new[] { typeof(MissingCheckerClass).Assembly }; })
            );
        }

        [Fact]
        public void AddAuthorizationWithOptions_ShouldRegisterServices_GivenValidCheckersAssembly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorization(options =>
            {
                options.Assemblies = new[] { typeof(ValidCheckersClass).Assembly };
            });

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAuthorizationService) &&
                sd.ImplementationType == typeof(AuthorizationService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<EmptyAccessPolicy>) &&
                sd.ImplementationType == typeof(EmptyAccessPolicyChecker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<TestAssemblies.ValidCheckers.Policy1.AccessPolicy1>) &&
                sd.ImplementationType == typeof(TestAssemblies.ValidCheckers.Policy1.AccessPolicy1Checker) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IAccessPolicyChecker<TestAssemblies.ValidCheckers.Policy2.AccessPolicy2>) &&
                sd.ImplementationType == typeof(TestAssemblies.ValidCheckers.Policy2.AccessPolicy2Checker) &&
                sd.Lifetime == ServiceLifetime.Scoped);
        }

        #endregion
    }
}
