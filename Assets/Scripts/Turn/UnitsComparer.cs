using System.Collections.Generic;

using MainGame.Units;

namespace MainGame.Turns
{
    public class UnitsComparer : IComparer<UnitData>
    {
        private ArmyType _priority;

        public UnitsComparer(ArmyType priority)
        {
            _priority = priority;
        }

        public int Compare(UnitData first, UnitData second)
        {
            int value = CheckIntValue(first.Initiative, second.Initiative);

            if (value == 0)
            {
                value = CheckIntValue(first.Speed, second.Speed);
            }

            if (value == 0)
            {
                value = CheckEnum(first.ArmyType, second.ArmyType);
            }

            if (value == 0)
            {
                value = CheckIntValue(first.ArmyIndex, second.ArmyIndex);
            }

            return value;
        }

        private int CheckIntValue(int x, int y)
        {
            if (x == y) return 0;

            return x > y ? -1 : 1;
        }

        private int CheckEnum(ArmyType x, ArmyType y)
        {
            return IsFirstPriority() ? -1 : IsSecondPriority() ? 1 : 0;

            bool IsFirstPriority() => x == _priority && y != _priority;
            bool IsSecondPriority() => y == _priority && x != _priority;
        }
    }
}
