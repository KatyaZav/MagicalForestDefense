using DI.Game.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace DI.Game.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        public List<int> CompletedLevels;
    }
}
