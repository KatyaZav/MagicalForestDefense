using DI.Game.Develop.CommonServices.AssetsManagment;
using DI.Game.Develop.CommonServices.ConfigsManagment;
using DI.Game.Develop.CommonServices.LevelsManagment;
using DI.Game.Develop.CommonServices.SceneManagment;
using DI.Game.Develop.DI;
using DI.Game.Develop.MainMenu.UI;
using UnityEngine;

namespace DI.Game.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelsMenuPopupFactory
    {
        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _resourcesAssetLoader;
        private readonly MainMenuUIRoot _mainMenuUIRoot;

        public LevelsMenuPopupFactory(DIContainer container)
        {
            _container = container;
            _resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();  
            _mainMenuUIRoot = container.Resolve<MainMenuUIRoot>();
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new LevelTilePresenter(_container.Resolve<CompletedLevelsService>(), _container.Resolve<SceneSwitcher>(), levelNumber, view);
        }

        public LevelTileListPresenter CreateLevelTilesListPresenter(LevelTileListView view)
        {
            return new LevelTileListPresenter(_container.Resolve<ConfigsProviderService>().LevelsListConfig, this, view);
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter()
        {
            LevelsMenuPopupView levelsMenuPopupViewPrefab = _resourcesAssetLoader.LoadResource<LevelsMenuPopupView>("MainMenu/UI/LevelsMenuPopup/LevelsMenuPopupView");
            LevelsMenuPopupView levelsMenuPopupView = Object.Instantiate(levelsMenuPopupViewPrefab, _mainMenuUIRoot.PopupsLayer);
            return new LevelsMenuPopupPresenter(this, levelsMenuPopupView);
        }
    }
}
