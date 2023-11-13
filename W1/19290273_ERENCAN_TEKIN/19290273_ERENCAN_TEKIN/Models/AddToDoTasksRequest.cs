namespace _19290273_ERENCAN_TEKIN.Models
{
    public class AddToDoTasksRequest
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int Priority { get; set; }

        public bool isDone { get; set; }
    }
}
