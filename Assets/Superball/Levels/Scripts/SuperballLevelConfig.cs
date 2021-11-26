using FriendsGamesTools.DebugTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class SuperballLevelConfig : BalanceSettings<SuperballLevelConfig>
    {
        /// <summary>
        /// ��������� ����� ������
        /// </summary>
        public float startLength = 100;

        /// <summary>
        /// ���������� ����� �� ������ �������
        /// </summary>
        public float lengthInc = 100;

        /// <summary>
        /// �������� ����� ������� � ���� �������
        /// </summary>
        public float pipesStartSpawnFrom = 25;

        /// <summary>
        /// ������� ������� ��� �������
        /// </summary>
        public float topBound = 15;

        /// <summary>
        /// ������ �������  ��� �������
        /// </summary>
        public float bottomBound = 15;

        /// <summary>
        /// �������� ������ ����
        /// </summary>
        public float metersPerPipe = 7;

        /// <summary>
        /// ����� ��������� ������ ����
        /// </summary>
        public float metersPerPipeThreshold = 3;

        private int GetLevelIndex()
        {
            return SuperballRoot.instance.Get<SuperballLevelsController>().currLocationInd;
        }

        public float GetLevelLength()
        {
            int levelIndex = GetLevelIndex();

            return instance.startLength + levelIndex * lengthInc;// - (((levelIndex) / 5) * 150); //��������� ���� �����������
        }

        public int GetPipesCount()
        {
            return Mathf.RoundToInt((GetLevelLength() - pipesStartSpawnFrom) / instance.metersPerPipe);
        }
    }
}