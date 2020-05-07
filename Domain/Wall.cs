namespace Survival
{
    public class Wall : ICell
    {
        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new System.NotImplementedException();
        }

        public CreatureCommand Act(Player player)
        {
            return new CreatureCommand();
        }

        public void ChangeInConflict(ICell conflictedObject)
        {
            return;
        }
    }
}