namespace TGPro.Service.Common
{
    public class ApiSuccessResponse<T> : ApiResponse<T>
    {
        public ApiSuccessResponse(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        public ApiSuccessResponse()
        {
            IsSuccessed = true;
        }
    }
}
