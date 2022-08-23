namespace Entities.Responses
{
    // Acts as a DTO for Get Movie Worker endpoints
    public class MovieWorkerResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
    }
}
