using System;
using System.Collections.Generic;

namespace Code.Infrastructure.Services
{
    // public class AllServices
    // {
    //     private static AllServices _instance;
    //     public static AllServices Container => _instance ?? (_instance = new AllServices());
    //     private Dictionary<Type, IService> _services;
    //
    //     public AllServices()
    //     {
    //         _services = new Dictionary<Type, IService>();
    //     }
    //
    //     public void RegisterSingle<TService>(TService implementation) where TService : IService
    //     {
    //         _services[typeof(TService)] = implementation;
    //     }
    //     
    //     public TService Single<TService>() where TService : class, IService
    //     {
    //         if (_services.ContainsKey(typeof(TService)) == false)
    //             throw new KeyNotFoundException();
    //         
    //         return _services[typeof(TService)] as TService;
    //     }
    // }
}