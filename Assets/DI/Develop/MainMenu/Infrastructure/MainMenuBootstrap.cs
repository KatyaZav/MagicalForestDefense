using DI.Game.Develop.CommonServices.AssetsManagment;
using DI.Game.Develop.CommonServices.DataManagment.DataProviders;
using DI.Game.Develop.CommonServices.SceneManagment;
using DI.Game.Develop.CommonServices.Wallet;
using DI.Game.Develop.CommonUI.Wallet;
using DI.Game.Develop.DI;
using DI.Game.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup;
using DI.Game.Develop.MainMenu.UI;
using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        InitializeUI();

        yield return new WaitForSeconds(1f);
    }

    private void InitializeUI()
    {
        MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
        mainMenuUIRoot.OpenLevelsMenuButton.Initialize(() =>
        {
            LevelsMenuPopupPresenter levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
            levelsMenuPopupPresenter.Enable();
        });
    }

    private void ProcessRegistrations()
    {
        //������ ����������� ��� ����� ��������
        _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));
        _container.RegisterAsSingle(c => new WalletPresenterFactory(c));

        _container.RegisterAsSingle(c =>
        {
            MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");
            return Instantiate(mainMenuUIRootPrefab);
        }).NonLazy();

        _container
            .RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
            .CreateWalletPresenter(c.Resolve<MainMenuUIRoot>().WalletView))
            .NonLazy();

        _container.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            WalletService wallet = _container.Resolve<WalletService>();
            wallet.Add(CurrencyTypes.Gold, 100);
            Debug.Log($"�����: {wallet.GetCurrency(CurrencyTypes.Gold).Value}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _container.Resolve<PlayerDataProvider>().Save();
        }
    }
}
