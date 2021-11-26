using FriendsGamesTools.DebugTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superball
{
    public class SuperballLevelConfig : BalanceSettings<SuperballLevelConfig>
    {
        /// <summary>
        /// Стартовая длина уровна
        /// </summary>
        public float startLength = 100;

        /// <summary>
        /// Инкременте длины за каждый уровнеь
        /// </summary>
        public float lengthInc = 100;

        /// <summary>
        /// Спаунить трубы начиная с этой позиции
        /// </summary>
        public float pipesStartSpawnFrom = 25;

        /// <summary>
        /// Верхняя граница для рандома
        /// </summary>
        public float topBound = 15;

        /// <summary>
        /// Нижняя граница  для рандома
        /// </summary>
        public float bottomBound = 15;

        /// <summary>
        /// Интервал спауна труб
        /// </summary>
        public float metersPerPipe = 7;

        /// <summary>
        /// Порог интервала спауна труб
        /// </summary>
        public float metersPerPipeThreshold = 3;

        private int GetLevelIndex()
        {
            return SuperballRoot.instance.Get<SuperballLevelsController>().currLocationInd;
        }

        public float GetLevelLength()
        {
            int levelIndex = GetLevelIndex();

            return instance.startLength + levelIndex * lengthInc;// - (((levelIndex) / 5) * 150); //Декремент пока закомментим
        }

        public int GetPipesCount()
        {
            return Mathf.RoundToInt((GetLevelLength() - pipesStartSpawnFrom) / instance.metersPerPipe);
        }
    }
}