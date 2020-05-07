namespace Survival
{
    public interface ICell
    {
        string GetImageFileName();
        int GetDrawingPriority();
        CreatureCommand Act(int x, int y, Player player);

        void ChangeInConflict(int x, int y, ICell cell);
    }
}