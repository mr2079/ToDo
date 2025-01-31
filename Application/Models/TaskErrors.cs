namespace Application.Models;

public static class TaskErrors
{
    public const string CreateFailed = "فرایند ایجاد وظیفه با خطا مواجه شد";
    public const string UpdateFailed = "فرایند ویرایش وظیفه با خطا مواجه شد";
    public const string DeleteFailed = "فرایند حذف وظیفه با خطا مواجه شد";
    public const string NotFound = "وظیفه ای با مشخصات وارد شده یافت نشد";
    public const string NotExist = "وظیفه ای با شناسه وارد شده یافت نشد";
    public const string InvalidDueDate = "تاریخ سررسید وظیفه نمی تواند قبل از تاریخ حال حاظر باشد";
}