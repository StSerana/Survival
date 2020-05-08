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

        public void Act(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInConflict(Player conflictedObject)
        {
            return;
        }
    }
}