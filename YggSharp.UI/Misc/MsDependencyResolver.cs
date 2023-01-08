using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Splat;

namespace YggSharp.UI.Misc;

public class MsDependencyResolver : IDependencyResolver
{
    private readonly IServiceCollection _serviceCollection;
    private readonly IServiceProvider _serviceProvider;

    public MsDependencyResolver(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
    
    public object? GetService(Type serviceType, string? contract = null)
    {
        return _serviceProvider.GetService(serviceType);
    }

    public IEnumerable<object?> GetServices(Type serviceType, string? contract = null)
    {
        return _serviceProvider.GetServices(serviceType);
    }

    public bool HasRegistration(Type serviceType, string? contract = null)
    {
        return _serviceProvider.GetService(serviceType) != null;
    }

    public void Register(Func<object> factory, Type serviceType, string? contract = null)
    {
        _serviceCollection.AddSingleton(serviceType, factory());
    }

    public void UnregisterCurrent(Type serviceType, string? contract = null)
    {
        throw new NotSupportedException();
    }

    public void UnregisterAll(Type serviceType, string? contract = null)
    {
        throw new NotSupportedException();
    }

    public IDisposable? ServiceRegistrationCallback(Type serviceType, string? contract, Action<IDisposable> callback)
    {
        return null;
    }

    public void Dispose()
    {
        throw new NotSupportedException();
    }
}