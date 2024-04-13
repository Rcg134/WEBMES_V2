namespace WEBMES_V2.Models.StaticModels.Enums
{
    public class StatusEnum
    {
        public enum StatusListEnum
        {
            InQueue = 1,
            InProcess = 2,
            Skipped = 3,
            Hold = 4,
            LotComplete = 5,
            LotTerminated = 6,
            Shipped = 7,
            InStore = 8
        }
    }
}
