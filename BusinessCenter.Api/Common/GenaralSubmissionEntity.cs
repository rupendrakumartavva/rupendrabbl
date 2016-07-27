namespace BusinessCenter.Api.Common
{
    public class GenaralSubmissionEntity
    {
        public string MasterId { get; set; }
        public string UserId { get; set; }
    }

    public class DownloadEhopEntity
    {
        public string MasterId { get; set; }
        public string UserName { get; set; }

        public string EhopNumber { get; set; }
    }
}