using MainGame.Units;

namespace MainGame.Turns.Visualization
{
    public interface ITurnsVisualizing
    {
        void AddTurnEnd(int nextTurnNumber);
        void MoveNext(UnitData data);
        void AddTurn(UnitData data);
        void RemoveTurn(UnitData data);
    }
}
