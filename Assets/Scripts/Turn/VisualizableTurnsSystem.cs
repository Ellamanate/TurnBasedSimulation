using MainGame.Units;
using MainGame.Turns.Visualization;

namespace MainGame.Turns
{
    public class VisualizableTurnsSystem : TurnsSystem
    {
        private ITurnsVisualizing _visualizer;

        public VisualizableTurnsSystem(ITurnsVisualizing visualizer, UnitData[] unitTurns, int maxTurns) : base (unitTurns, maxTurns)
        {
            _visualizer = visualizer;

            _visualizer.AddTurnEnd(TurnNumber + 1);
        }

        protected override void OnNextTurn()
        {
            _visualizer.AddTurnEnd(TurnNumber + 1);
        }

        protected override void OnMoveNext()
        {
            _visualizer.MoveNext(CurrentTurn);
        }

        protected override void OnAdding(UnitData data)
        {
            _visualizer.AddTurn(data);
        }

        protected override void OnRemove(UnitData data)
        {
            _visualizer.RemoveTurn(data);
        }
    }
}