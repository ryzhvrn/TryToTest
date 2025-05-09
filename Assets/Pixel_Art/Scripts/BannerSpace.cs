/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerSpace : MonoBehaviour
{
	private Vector2 m_sizeDelta = Vector2.zero;

	[SerializeField]
	private RectTransform m_canvas;

	[SerializeField]
	private List<BannerStrategy> m_bannerStrategies;

	private float m_pixels = 50f;

	private LayoutElement m_layoutElement;

	public float Height { get; private set; }

	private void Awake()
	{
		base.StartCoroutine(this.InitCoroutine()); 
        if (IAPWrapper.Instance != null)
            IAPWrapper.Instance.OnPurchase = (Action<bool, SubscriptionType>)Delegate.Combine(IAPWrapper.Instance.OnPurchase, new Action<bool, SubscriptionType>(this.OnPurchaseHandler));
		this.m_layoutElement = base.GetComponent<LayoutElement>();
		this.m_pixels = (float)((!MySystemInfo.IsTablet) ? 52 : 92);
	}

	private IEnumerator InitCoroutine()
	{
		yield return null;
		if (INPluginWrapper.Instance.GetAbTestGroup() == ABTestGroup.Undefined)
		{
			yield return null;
		}
		this.Height = this.m_pixels / (float)Screen.height * this.m_canvas.rect.height * ScreenToolWrapper.Density;
		this.m_sizeDelta = new Vector2(0f, this.Height);
		this.UpdateVisibility();
	}

	private void OnPurchaseHandler(bool arg1, SubscriptionType arg2)
	{
		this.UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		UnityEngine.Debug.Log("TryUpdateVisibility");
        if (INPluginWrapper.Instance.GetAbTestGroup() != 0 && AdsWrapper.Instance != null)
		{
			UnityEngine.Debug.Log("UpdateVisibility: " + AdsWrapper.Instance.BannerStrategy + ";" + AdsWrapper.Instance.BannerPosition);
			if (this.m_bannerStrategies.Contains(AdsWrapper.Instance.BannerStrategy))
			{
				if (IAPWrapper.Instance.NoAds)
				{
					(base.transform as RectTransform).sizeDelta = Vector2.zero;
					if (this.m_layoutElement != null)
					{
						LayoutElement layoutElement = this.m_layoutElement;
						float num = 0f;
						this.m_layoutElement.preferredHeight = num;
						layoutElement.minHeight = num;
					}
				}
				else
				{
					(base.transform as RectTransform).sizeDelta = this.m_sizeDelta;
					if (this.m_layoutElement != null)
					{
						LayoutElement layoutElement2 = this.m_layoutElement;
						float num = 0f;
						this.m_layoutElement.preferredHeight = num;
						layoutElement2.minHeight = num;
					}
					if (this.m_layoutElement != null)
					{
						LayoutElement layoutElement3 = this.m_layoutElement;
						float num = this.m_sizeDelta.y;
						this.m_layoutElement.preferredHeight = num;
						layoutElement3.minHeight = num;
					}
					if (AdsWrapper.Instance.BannerPosition == BannerPosition.Bottom)
					{
						base.gameObject.transform.SetSiblingIndex(base.gameObject.transform.GetSiblingIndex() + 1);
					}
				}
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void OnDestroy()
	{ 
        if (IAPWrapper.Instance != null)
            IAPWrapper.Instance.OnPurchase = (Action<bool, SubscriptionType>)Delegate.Remove(IAPWrapper.Instance.OnPurchase, new Action<bool, SubscriptionType>(this.OnPurchaseHandler));
	}
}
