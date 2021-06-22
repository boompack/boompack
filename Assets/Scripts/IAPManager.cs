using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : Singleton<IAPManager>, IStoreListener
{

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.


    public static string premium = "com.pulsar.boompack.premium";
    public static string bonusBalls = "com.pulsar.boompack.bonus_balls";

    public GameObject messageBox;

    public List<GameObject> hideableObjects=new List<GameObject>();


    void Start()
    {

        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(premium, ProductType.NonConsumable);
        builder.AddProduct(bonusBalls, ProductType.Consumable);


        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }




    public void BuyPremium()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(premium);
    }

    public void BuyBonusBalls()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(bonusBalls);
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");

                if (result)
                {
                    SetPremium();

                    //StartCoroutine(MessageBox("Restore Purchases Completed"));
                }
                else
                {
                    //StartCoroutine(MessageBox("Restore Purchases Failed"));

                }


            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            //StartCoroutine(MessageBox("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform));

        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, premium, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.

            PurchasedPremiumPack();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, bonusBalls, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.

            PurchasedBonusBalls();
        }
        // Or ... a subscription product has been purchased by this user.

        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    IEnumerator MessageBox(string str)
    {
        messageBox.GetComponentInChildren<Text>().text = str;
        messageBox.SetActive(true);
        yield return new WaitForSeconds(2);
        messageBox.SetActive(false);
    }

    void PurchasedBonusBalls()
    {
        GameAnalytics.NewBusinessEvent("USD",1,"BonusBalls", "bonus_balls","Try Again Screen");
        PlayerPrefs.SetInt("BonusBalloon1Count", PlayerPrefs.GetInt("BonusBalloon1Count") + 10);
        GameManager.Instance.bonusBalloon1Count = PlayerPrefs.GetInt("BonusBalloon1Count");
        PlayerPrefs.Save();
        BonusBalloonUIManager.Instance.RefleshCounts();
    }

    void PurchasedPremiumPack()
    {
        for (int i = 1; i <= 300; i++)
        {
            SaveManager.Instance.levelStats.levelStatsDict[i].isLocked = false;
            if(!SaveManager.Instance.levelStats.levelStatsDict[i].isPlayable)
                SaveManager.Instance.levelStats.levelStatsDict[i].isPlayable = false;
            //levelStats.levelStatsDict[i].isPlayable = true;

        }

        PlayerPrefs.SetInt("PREMIUM",1);
        CheckPremium();

        GameAnalytics.NewBusinessEvent("USD", 1, "PremiumPack", "premium", "Purchase Button");

        if (GameObject.Find("GameManager") != null)
        {
            GameAnalytics.NewDesignEvent("What Level Was purchased", (float)GameManager.Instance.levelLoader.loadedLevel.levelID);

        }
        else
        {
            GameAnalytics.NewDesignEvent("What Level Was purchased", 0f);

        }

    }

    public void CheckPremium()
    {
        if (PlayerPrefs.GetInt("PREMIUM")==1)
        {
            SetPremium();
        }
        //else
        //{
        //    hideableObjects.ForEach(x=>x.SetActive((true)));

        //}
    }

    public void SetPremium()
    {
        for (int i = 1; i <= 300; i++)
        {
            SaveManager.Instance.levelStats.levelStatsDict[i].isLocked = false;
            if (!SaveManager.Instance.levelStats.levelStatsDict[i].isPlayable)
                SaveManager.Instance.levelStats.levelStatsDict[i].isPlayable = false;
            //levelStats.levelStatsDict[i].isPlayable = true;

        }
        hideableObjects.ForEach(x => x.SetActive((false)));

    }
}
