namespace TGPro.Service.SystemResources
{
    public class ConstantStrings
    {
        public const string DbConnectionString = "TGProDb";
        public const string CloudinarySetting = "CloudinarySettings";

        public const string SwaggerUrl = "/swagger/v1/swagger.json";
        public const string SwaggerName = "TGPro.BackendAPI v1";
        public const string OpenApiTitle = "TGPro.BackendAPI";
        public const string OpenApiVersion = "v1";

        public const string AllowOrigin = "AllowOrigin";
        public const string ManagementPageUrl = "https://localhost:4200";

        public static string _productFolder;
        public static string _trademarkFolder;
        public static string _userFolder;

        public const string PRODUCT_IMAGE_FOLDER = @"contents\images\product\";
        public const string TRADEMARK_IMAGE_FOLDER = @"wwwroot\contents\images\trademark\";
        public const string USER_IMAGE_FOLDER = @"contents\images\user\";

        public const string defaultProductImage = "default_product_image.jpg";
        public const string defaultTrademarkImage = "default_trademark_image.jpg";
        public const string defaultMaleImage = "default_male_image.jpg";
        public const string defaultFemaleImage = "default_female_image.jpg";

        public const string CL_TRADEMARK_IMAGE_FOLDER = "upload/img/trademark/";
        public const string CL_USER_IMAGE_FOLDER = "upload/img/user/";

        public const string AdminRole = "Admin";
        public const string EmployeeRole = "Employee";
        public const string WarehouseRole = "Warehouse Staff";
        public const string CustomerOfficerRole = "Customer Officer";
        public const string CustomerRole = "Customer";

        public const string undefinedError = "Lỗi chưa xác định.";
        public const string emptyNameFieldError = "Trường tên không được rỗng!";

        public const string addSuccessfully = "Tạo mới thành công!";
        public const string editSuccessfully = "Chỉnh sửa thành công!";
        public const string deleteSuccessfully = "Xoá thành công!";
        public const string addUnuccessfully = "Tạo mới không thành công!";
        public const string editUnuccessfully = "Chỉnh sửa không thành công!";
        public const string deleteUnuccessfully = "Xoá không thành công!";
        public const string userInfoIncorrect = "Tài khoản hoặc mật khẩu không đúng!";
        public const string userNotExist = "Tài khoản không tồn tại!";
        public const string cloudDeleteFailed = "Lỗi xoá ảnh trên đám mây!";
        public const string getAllError = "Không có dữ liệu nào được tìm thấy!";

        public const string createConnectionError = "Không thể kết nối tới server!";

        public const string roleExisted = "Quyền này đã tồn tại!";
        public const string roleNotExisted = "Quyền này không tồn tại!";

        public static string FindByIdError(int id)
        {
            return $"Không thể tìm thấy dữ liệu nào ứng với id: {id}!";
        }
    }
}
