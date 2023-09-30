namespace IBIDCrud.Entities
{
    public class IBIDEvent
    {
        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<IBIDEventSpeaker> Speakers { get; set; }
        public bool IsDeleted { get; set; }

        public IBIDEvent()
        {
            Speakers = new List<IBIDEventSpeaker>();
            IsDeleted = false;
        }

        public void Update(String title, string description, DateTime startDate, DateTime endDate)
        {
            Title = title; 
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
