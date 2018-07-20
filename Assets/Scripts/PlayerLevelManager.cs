using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace UFOPanic
{

	public class PlayerLevelManager : MonoBehaviour
	{

		[SerializeField]
		Player _player;

		[SerializeField]
		int[] _needPoints = { 10, 20, 40, 80, 120, 160, 200 };

		[SerializeField]
		int[] _attractorValues = { 15, 20, 25, 30};

		[SerializeField]
		int[] _speedValues = { 5, 10, 20, 30};

		[SerializeField]
		PointIndicator _pointIndicator;

		[SerializeField]
		Text _levelText;

		[SerializeField]
		Text _pointText;

		[SerializeField]
		Text _attractText, _speedText;

		public int currentNeedPoint { get { return _needPoints[_currentLevel - 1]; } }

		int _currentLevel = 1;
		int _sumPoint = 0;

		int _canUsePoint = 0;

		int _attractorLevel = 0;
		int _speedLevel = 0;

		#region MonoBehaviourイベント

		private void Start()
		{
			ApplyPointIndicator();
		}

		#endregion

		#region 外部インタフェース

		public void AddPoint(int point)
		{
			_sumPoint += point;
			var nextNeed = currentNeedPoint;
			if (_sumPoint >= nextNeed)
			{
				_sumPoint -= nextNeed;
				_currentLevel++;
				_levelText.text = _currentLevel.ToString();
				_canUsePoint++;
				ApplyPointText();
			}

			ApplyPointIndicator();
		}

		public void OnLevelUpPowerButton()
		{
			if (_canUsePoint > 0)
			{
				_canUsePoint--;
				_attractorLevel++;
				_player.attractPower = _attractorValues[_attractorLevel];
				ApplyPointText();
				_attractText.text = (_attractorLevel + 1).ToString();
			}
		}

		public void OnLevelUpSpeedButton()
		{
			if (_canUsePoint > 0)
			{
				_canUsePoint--;
				_speedLevel++;
				_player.speed = _speedValues[_speedLevel];
				ApplyPointText();
				_speedText.text = (_speedLevel + 1).ToString();
			}
		}

		#endregion

		#region 内部処理

		void ApplyPointIndicator()
		{
			Assert.IsNotNull(_pointIndicator);
			var ratio = (float)_sumPoint / currentNeedPoint;

			_pointIndicator.SetRatio(ratio);
		}

		void ApplyPointText()
		{
			Assert.IsNotNull(_pointText);
			_pointText.text = _canUsePoint.ToString();
		}

		#endregion
	}
}