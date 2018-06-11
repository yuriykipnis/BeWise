﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DataProvider.Providers.Banks.Hapoalim;
using DataProvider.Providers.Cards.Amex;
using DataProvider.Providers.Interfaces;
using GoldMountainShared;
using GoldMountainShared.Storage.Documents;

namespace DataProvider.Providers
{
    public class ProviderFactory : IProviderFactory
    {
        public async Task<IAccountProvider> CreateDataProvider(Provider provider)
        {
            IAccountProvider accountProvider = null;

            switch (provider.Name)
            {
                case "Bank Hapoalim":
                    //accountProvider = new HapoalimAccountProvider(provider, new HapoalimFileApi(provider));
                    accountProvider = new HapoalimAccountProvider(new HapoalimApi(provider));
                    break;
                case "Amex":
                    //accountProvider = new AmexAccountProvider(provider, new AmexFileApi(provider));
                    accountProvider = new AmexAccountProvider(new AmexApi(provider));
                    break;
                case "Visa Cal":
                    accountProvider = new AmexAccountProvider(new AmexFakeApi(provider));
                    break;
                default: break;
            }

            return accountProvider;
        }
       
    }
}