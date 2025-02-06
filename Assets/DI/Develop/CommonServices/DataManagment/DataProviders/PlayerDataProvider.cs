using DI.Game.Develop.CommonServices.ConfigsManagment;
using DI.Game.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace DI.Game.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private ConfigsProviderService _configsProviderService;

        //тут будем передавать сервис конфигов
        public PlayerDataProvider(
            ISaveLoadSerivce saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            UnityEngine.Debug.LogError("Cheak original player data");
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                CompletedLevels = new()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData.Add(currencyType, _configsProviderService.StartWalletConfig.GetStartValueFor(currencyType));

            return walletData;
        }
    }
}
