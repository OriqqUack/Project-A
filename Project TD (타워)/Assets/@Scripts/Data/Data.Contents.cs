using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{ 
#region Stat
	[Serializable]
	public class Stat
	{
		public int level;
		public int maxHp;
		public int attack;
		public int totalExp;
	}
	[Serializable]
	public class MonsterStat
	{
		public string monsterName;
		public int level;
		public int maxHp;
		public int attack;
		public float moveSpeed;
		public float attackSpeed;
		public float scanRange;
		public float attackRange;
	}

    [Serializable]
	public class StatData : ILoader<int, Stat>
	{
		public List<Stat> stats = new List<Stat>();
		

		public Dictionary<int, Stat> MakeDict()
		{
			Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
			foreach (Stat stat in stats)
				dict.Add(stat.level, stat);
			return dict;
		}
	}

    [Serializable]
    public class MonsterData : ILoader<string, MonsterStat>
    {
        public List<MonsterStat> monsters = new List<MonsterStat>();

        public Dictionary<string, MonsterStat> MakeDict()
        {
            Dictionary<string, MonsterStat> dict = new Dictionary<string, MonsterStat>();
            foreach (MonsterStat monsterStat in monsters)
                dict.Add(monsterStat.monsterName, monsterStat);
            return dict;
        }
    }
    #endregion
}