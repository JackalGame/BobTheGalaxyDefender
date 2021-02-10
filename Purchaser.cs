using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    private string moons3 = "moons_3";
    private string moons10 = "moons_10";
    private string stars500 = "stars_500";
    private string stars2000 = "stars_2000";
    private string stars10000 = "stars_10000";


    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(moons3, ProductType.Consumable);
        builder.AddProduct(moons10, ProductType.Consumable);
        builder.AddProduct(stars500, ProductType.Consumable);
        builder.AddProduct(stars2000, ProductType.Consumable);
        builder.AddProduct(stars10000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void Buy3Moons()
    {
        BuyProductID(moons3);
    }

    public void Buy10Moons()
    {
        BuyProductID(moons10);
    }

    public void Buy500Stars()
    {
        BuyProductID(stars500);
    }

    public void Buy2000Stars()
    {
        BuyProductID(stars2000);
    }

    public void Buy10000Stars()
    {
        BuyProductID(stars10000);
    }





    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, moons3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("Added 3 moons'", args.purchasedProduct.definition.id));
            FindObjectOfType<Shop>().PurchaseMoons(3);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, moons10, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("Added 10 moons'", args.purchasedProduct.definition.id));
            FindObjectOfType<Shop>().PurchaseMoons(10);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stars500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("Added 500 Stars'", args.purchasedProduct.definition.id));
            FindObjectOfType<Shop>().PurchaseStars(500);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stars2000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("Added 2000 Stars'", args.purchasedProduct.definition.id));
            FindObjectOfType<Shop>().PurchaseStars(2000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stars10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("Added 10000 Stars'", args.purchasedProduct.definition.id));
            FindObjectOfType<Shop>().PurchaseStars(10000);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}

