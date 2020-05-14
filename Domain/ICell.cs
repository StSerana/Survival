namespace Survival
{
    public interface ICell
    {
        void Act(Game game, Player player);

        void ChangeInConflict(Game game, Player conflictedObject);
    }
}