namespace Survival
{
    public interface ICell
    {
        string GetImageFileName();
        int GetDrawingPriority();
        void Act(Player player);

        void ChangeInConflict(Player conflictedObject);
    }
}