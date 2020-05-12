namespace Survival
{
    public interface ICell
    {
        string GetImageFileName();
        int GetDrawingPriority();
        void Act(Game game, Player player);

        void ChangeInConflict(Game game, Player conflictedObject);
    }
}