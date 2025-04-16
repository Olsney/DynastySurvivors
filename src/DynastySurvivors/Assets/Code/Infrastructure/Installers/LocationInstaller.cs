using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LocationInstaller : MonoInstaller
    {
    //     public List<GameObject> SavedObjects;
    //
    //     public void Initialize()
    //     {
    //
    //     }
    //
    //     public override void InstallBindings()
    //     {
    //         IGameFactory gameFactory = Container.Resolve<IGameFactory>();
    //
    //         foreach (var savedObject in SavedObjects)
    //         {
    //             if (savedObject.TryGetComponent(out ISavedProgressReader progressReader))
    //             {
    //                 gameFactory.ProgressReaders.Add(progressReader);
    //             }
    //
    //             if (savedObject.TryGetComponent(out ISavedProgress progressWriter))
    //             {
    //                 gameFactory.ProgressWriters.Add(progressWriter);
    //             }
    //         }
    //     }
    }
}