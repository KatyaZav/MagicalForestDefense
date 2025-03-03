using DI.Game.Develop.CommonServices.AssetsManagment;
using DI.Game.Develop.CommonServices.ConfigsManagment;
using DI.Game.Develop.CommonServices.CoroutinePerfomer;
using DI.Game.Develop.CommonServices.DataManagment;
using DI.Game.Develop.CommonServices.DataManagment.DataProviders;
using DI.Game.Develop.CommonServices.LevelsManagment;
using DI.Game.Develop.CommonServices.LoadingScreen;
using DI.Game.Develop.CommonServices.SceneManagment;
using DI.Game.Develop.CommonServices.Wallet;
using DI.Game.Develop.DI;
using System;
using UnityEngine;

namespace DI.Game.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            //регистрация сервисов на целый проект
            //Аналог global context из популярных DI фреймворков
            //Самый родительский контейнер для всех будущих

            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);

            RegisterLoadingCurtain(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);

            RegisterSaveLoadService(projectContainer);
            Debug.LogError("Cheack saving system");
            RegisterPlayerDataProvider(projectContainer);

            RegisterWalletService(projectContainer);

            RegisterConfigsProviderService(projectContainer);

            RegisterCompletedLevelsService(projectContainer);

            projectContainer.Initialize();

            //все регистрации прошли
            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));
        }

        private void RegisterCompletedLevelsService(DIContainer container)
            => container.RegisterAsSingle(c => new CompletedLevelsService(c.Resolve<PlayerDataProvider>())).NonLazy();

        private void SetupAppSettings()
        {
            Debug.LogError("Cheak settings before build");
            QualitySettings.vSyncCount = 0; 
            Application.targetFrameRate = 60;
        }

        private void RegisterSaveLoadService(DIContainer container)
            => container.RegisterAsSingle<ISaveLoadSerivce>(c => new SaveLoadService(new JsonSerializer(), new LocalDataRepository()));

        private void RegisterConfigsProviderService(DIContainer container)
            => container.RegisterAsSingle(c => new ConfigsProviderService(c.Resolve<ResourcesAssetLoader>()));

        private void RegisterPlayerDataProvider(DIContainer container)
            => container.RegisterAsSingle(c => new PlayerDataProvider(c.Resolve<ISaveLoadSerivce>(), c.Resolve<ConfigsProviderService>()));

        private void RegisterWalletService(DIContainer container) 
            => container.RegisterAsSingle(c => new WalletService(c.Resolve<PlayerDataProvider>())).NonLazy();

        private void RegisterSceneSwitcher(DIContainer container)
            => container.RegisterAsSingle(c 
                => new SceneSwitcher(
                    c.Resolve<ICoroutinePerformer>(), 
                    c.Resolve<ILoadingCurtain>(),
                    c.Resolve<ISceneLoader>(), 
                    c));

        private void RegisterResourcesAssetLoader(DIContainer container) 
            => container.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                CoroutinePerformer coroutinePerformerPrefab = resourcesAssetLoader
                .LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterLoadingCurtain(DIContainer container)
        {
            container.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandardLoadingCurtain standardLoadingCurtainPrefab = resourcesAssetLoader
                .LoadResource<StandardLoadingCurtain>(InfrastructureAssetPaths.LoadingCurtainPath);

                return Instantiate(standardLoadingCurtainPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer container)
            => container.RegisterAsSingle<ISceneLoader>(c => new DefaultSceneLoader());
    }
}
