namespace Tips.api
{
    public class TipCreateReq
    {
        public string [] Category { get; set; }
        public string Title { get; set; }
        public string Content{ get; set; }
        public string Username { get; set; }
        public string CreateDate { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public string Location { get; set; }
    }
}