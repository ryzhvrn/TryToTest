/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongTapReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	private DateTime m_downTime = DateTime.MaxValue;

	[SerializeField]
	private float m_duration = 1f;

	public void OnPointerDown(PointerEventData eventData)
	{
		this.m_downTime = DateTime.Now;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if ((DateTime.Now - this.m_downTime).TotalSeconds > (double)this.m_duration)
		{
			DebugController.Instance.ClickDebugTexts();
		}
	}
}


