﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Atotupe.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalletSummaryPage : ContentPage
	{
	    private Wallet _wallet;

	    public Wallet Wallet
	    {
	        get => _wallet;
	        set => _wallet = value;
	    }

		public WalletSummaryPage ()
		{
			InitializeComponent ();

            //TODO: TEMP
		    _wallet = new Wallet {Name = "Wallet 1"};

            // Register events
		    AddCurrency.Clicked += OnAddCurrency;
		}

	    private async void OnAddCurrency(object sender, EventArgs args)
	    {
            List<string> missingCurrencies = new List<string>();

            if (!_wallet.ContainsCurrency("BTC"))
                missingCurrencies.Add("Bitcoin");
            if (!_wallet.ContainsCurrency("BCH"))
                missingCurrencies.Add("Bitcoin Cash");
            if (!_wallet.ContainsCurrency("ETH"))
                missingCurrencies.Add("Ethereum");
            if (!_wallet.ContainsCurrency("LTC"))
                missingCurrencies.Add("Litecoin");
            if (!_wallet.ContainsCurrency("XRP"))
                missingCurrencies.Add("Ripple");

	        var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, missingCurrencies.ToArray());

	        if (action != "Cancel")
	        {
	            var item = new Currency
	            {
	                Name = action,
	                Code = App.GetCodeOfCurrency(action),
	                Number = 0,
	                Price = 123,
	                Value = 0
	            };

	            if (!_wallet.ContainsCurrency(item))
	            {
                    _wallet.AddCurrency(item);
	                Currencies.AddCurrency(item);
	                item.ValueUpdated += OnCurrencyValueUpdated;
	            }
	        }
        }

	    private void OnCurrencyValueUpdated(object sender, EventArgs args)
	    {
	        ValueLabel.Text = ("$" + _wallet.Value().ToString());
	    }
	}
}