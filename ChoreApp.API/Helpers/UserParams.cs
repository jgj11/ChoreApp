namespace ChoreApp.API.Helpers
{
    public class UserParams
    {
        private const int Max = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > Max) ? Max : value ;}
        }

        public int UserId { get; set; }
        public int MinYoe { get; set; } = 0;
        public int MaxYoe { get; set; } = 99;

        public bool Networkees { get; set; } = false;
        public bool Networkers { get; set; } = false;

        public string OrderBy { get; set; }

    }
}