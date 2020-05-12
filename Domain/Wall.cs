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

        public void Act(Game game, Player player)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInConflict(Game game, Player conflictedObject)
        {
            return;
        }
    }
}