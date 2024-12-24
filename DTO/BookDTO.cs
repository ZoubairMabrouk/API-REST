namespace API_EXAMEN_APP.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Image { get; set; }

        public string Content { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public DateTime pubdate { get; set; }

        public DateTime lastupdate { get; set; }
        public int authorid { get; set; }
        public int typeid { get; set; }
    }
}
