namespace APITest.Domain.Models.Responses
{
    public class FileGetResponse : BaseResponse {
        public FileRecord FileRecord { get; set; }
       
        public FileGetResponse(bool success, string message, FileRecord fileRecord) : base(success, message) {
            FileRecord = fileRecord;
        }
    }
}