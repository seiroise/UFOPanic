using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UFOPanic
{

	public class PointIndicator : MonoBehaviour
	{

		[SerializeField]
		Image _front;

		public void SetRatio(float ratio)
		{
			Assert.IsNotNull(_front);
			ratio = Mathf.Clamp01(ratio);

			var max = _front.rectTransform.anchorMax;
			max.x = ratio;
			_front.rectTransform.anchorMax = max;
		}
	}
}