namespace Survival
{
    public interface ICell
    {
        string GetImageFileName();
        int GetDrawingPriority();
        CreatureCommand Act(Player player);

        void ChangeInConflict(ICell conflictedObject);
    }
}