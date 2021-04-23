namespace TGPro.Service.Common
{
    public class ConstantStrings
    {
        public const string DbConnectionString = "TGProDb";
        public const string CloudinarySetting = "CloudinarySettings";

        public const string AllowOrigin = "AllowOrigin";
        public const string ManagementUrl = "https://localhost:4200";

        public const string PRODUCT_IMAGE_FOLDER = @"wwwroot\contents\images\product\";
        public const string TRADEMARK_IMAGE_FOLDER = @"wwwroot\contents\images\trademark\";
        public const string USER_IMAGE_FOLDER = @"wwwroot\contents\images\user\";

        public const string defaultProductImage = "default_product_image.jpg";
        public const string defaultTrademarkImage = "default_trademark_image.jpg";
        public const string defaultMaleImage = "default_male_image.jpg";
        public const string defaultFemaleImage = "default_female_image.jpg";

        public const string blankProductImageUrl = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1619054413/upload/img/product/blank/default_product_image_zjdfbn.jpg";
        public const string blankProductImagePublicId = "upload/img/product/blank/default_product_image_zjdfbn";

        public const string CL_TRADEMARK_IMAGE_FOLDER = "upload/img/trademark/";
        public const string CL_USER_IMAGE_FOLDER = "upload/img/user/";
        public const string CL_PRODUCT_IMAGE_FOLDER = "upload/img/product/";

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
        public const string confirmPasswordError = "Mật khẩu cũ không đúng!";
        public const string cloudDeleteFailed = "Lỗi xoá ảnh trên đám mây!";
        public const string getAllError = "Không có dữ liệu nào được tìm thấy!";
        public const string lockSuccessfully = "Khoá tài khoản thành công!";
        public const string unlockSuccessfully = "Mở khoá tài khoản thành công!";

        public const string createConnectionError = "Không thể kết nối tới server!";

        public const string roleExisted = "Quyền này đã tồn tại!";
        public const string roleNotExisted = "Quyền này không tồn tại!";

        public const string imgIsEmptyOrNull = "Chưa có ảnh nào được chọn!";

        public static string FindByIdError(int id)
        {
            return $"Không thể tìm thấy dữ liệu nào ứng với id: {id}!";
        }
    }
}
