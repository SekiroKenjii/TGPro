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

        public const int TGNotFound = -1;
        public const int TGBadRequest = -2;
        public const int TGCloudError = -10;

        public const string undefinedError = "Something went wrong!";
        public const string emptyNameFieldError = "Name field cannot be null!";
        public const string cloudDeleteFailed = "Something went wrong! Cannot delete image in the cloud.";
    }
}
