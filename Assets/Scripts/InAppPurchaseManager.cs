

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPurchaseManager : MonoBehaviour, IStoreListener
{
	public static Action<int, string, string, PurchaseEventArgs> OnInAppPurchaseProcessComplete;

	public const string INAPP_PURCHASE_JEWEL_1 = "matchhero_jewel_1";

	public const string INAPP_PURCHASE_JEWEL_2 = "matchhero_jewel_2";

	public const string INAPP_PURCHASE_JEWEL_3 = "matchhero_jewel_3";

	public const string INAPP_PURCHASE_JEWEL_4 = "matchhero_jewel_4";

	public const string INAPP_PURCHASE_JEWEL_5 = "matchhero_jewel_5";

	public const string INAPP_PURCHASE_JEWEL_6 = "matchhero_jewel_6";

	public const string INAPP_PURCHASE_STARTER_PACK = "matchhero_starter_pack";

	public const string INAPP_PURCHASE_SPECIAL_OFFER = "matchhero_special_offer";

	public const string INAPP_PURCHASE_ARENA_PACK = "matchhero_arena_pack";

	private static InAppPurchaseManager instance;

	private static IStoreController storeController;

	private static IExtensionProvider extensionProvider;

	private int purchase_id;

	private PurchaseEventArgs purchase_args;

	public static void BuyProductID(string productId, int idx)
	{
		try
		{
			if (instance.IsInitialized())
			{
				Product product = storeController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					instance.purchase_id = idx;
					MWLog.Log($"Purchasing product asychronously: '{product.definition.id}'");
					storeController.InitiatePurchase(product);
				}
				else
				{
					MWLog.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				MWLog.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		catch (Exception arg)
		{
			MWLog.Log("BuyProductID: FAIL. Exception during purchase. " + arg);
		}
	}

	public static string GetPrice(string idstring, string origin)
	{
		return instance.GetProductMoneyString(idstring, origin);
	}

	private bool IsInitialized()
	{
		return storeController != null && extensionProvider != null;
	}

	private void InitializePurchasing()
	{
		if (!IsInitialized())
		{
			StandardPurchasingModule first = StandardPurchasingModule.Instance();
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(first);
			configurationBuilder.AddProduct("matchhero_jewel_1", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_1",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_1",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_jewel_2", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_2",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_2",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_jewel_3", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_3",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_3",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_jewel_4", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_4",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_4",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_jewel_5", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_5",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_5",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_jewel_6", ProductType.Consumable, new IDs
			{
				{
					"matchhero_jewel_6",
					"AppleAppStore"
				},
				{
					"matchhero_jewel_6",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_starter_pack", ProductType.Consumable, new IDs
			{
				{
					"matchhero_starter_pack",
					"AppleAppStore"
				},
				{
					"matchhero_starter_pack",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_special_offer", ProductType.Consumable, new IDs
			{
				{
					"matchhero_special_offer",
					"AppleAppStore"
				},
				{
					"matchhero_special_offer",
					"GooglePlay"
				}
			});
			configurationBuilder.AddProduct("matchhero_arena_pack", ProductType.Consumable, new IDs
			{
				{
					"matchhero_arena_pack",
					"AppleAppStore"
				},
				{
					"matchhero_arena_pack",
					"GooglePlay"
				}
			});
			UnityPurchasing.Initialize(this, configurationBuilder);
		}
	}

	private string GetProductMoneyString(string idString, string origin)
	{
		if (storeController == null)
		{
			return origin;
		}
		return storeController.products.WithStoreSpecificID(idString).metadata.localizedPriceString;
	}

#if false
    private void VerificationReceipt_AOS(PurchaseEventArgs args)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)JsonReader.Deserialize(args.purchasedProduct.receipt);
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)JsonReader.Deserialize((string)dictionary["Payload"]);
		string arg = (string)dictionary2["json"];
		string arg2 = (string)dictionary2["signature"];
		purchase_args = args;
		if (OnInAppPurchaseProcessComplete != null)
		{
			OnInAppPurchaseProcessComplete(purchase_id, arg2, arg, args);
		}
	}

	private void VerificationReceipt_AWS(PurchaseEventArgs args)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)JsonReader.Deserialize(args.purchasedProduct.receipt);
		string arg = (string)dictionary["Payload"];
		string id = args.purchasedProduct.definition.id;
		purchase_args = args;
		if (OnInAppPurchaseProcessComplete != null)
		{
			OnInAppPurchaseProcessComplete(purchase_id, id, arg, args);
		}
	}

	private void VerificationReceipt_IOS(PurchaseEventArgs args)
	{
		string receipt = args.purchasedProduct.receipt;
		c_Iap c_Iap = null;
		string text;
		if (receipt.Contains("Payload"))
		{
			c_Iap = JsonReader.Deserialize<c_Iap>(receipt);
			text = c_Iap.Payload;
		}
		else
		{
			text = receipt;
		}
		if (text.IndexOf("{") > -1 && text.IndexOf("}") > -1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			text = Convert.ToBase64String(bytes);
		}
		string id = args.purchasedProduct.definition.id;
		string arg = text;
		purchase_args = args;
		if (OnInAppPurchaseProcessComplete != null)
		{
			OnInAppPurchaseProcessComplete(purchase_id, id, arg, args);
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		MWLog.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
		if (BuildSet.CurrentPlatformType == PlatformType.aos)
		{
			VerificationReceipt_AOS(args);
		}
		else if (BuildSet.CurrentPlatformType == PlatformType.aws)
		{
			VerificationReceipt_AWS(args);
		}
		else if (BuildSet.CurrentPlatformType == PlatformType.ios)
		{
			VerificationReceipt_IOS(args);
		}
		return PurchaseProcessingResult.Complete;
	}
#endif


    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new NotImplementedException();
    }

    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
	{
		MWLog.Log("OnInitialized : PASS");
		storeController = sc;
		extensionProvider = ep;
	}

	public void OnInitializeFailed(InitializationFailureReason reason)
	{
		MWLog.Log("OnInitializeFailed InitializationFailureReason:" + reason);
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		MWLog.Log($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
	}

	private void Awake()
	{
		instance = this;
		InitializePurchasing();
	}
}
