using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.Commons
{
    public enum CourseTypeEnum
    {
        [Display(Name = "Ngắn hạn")]
        ShortTerm=1,
        [Display(Name="Dài hạn")]
        LongTerm=2,
    }
    public enum GenderEnum
    {
        [Display(Name = "Giới tính name")]
        Male=1,
        [Display(Name="Giới tính nữ")]
        Female=2,
    }
    public enum CourseStatusEnum
    {
        [Display(Name = "Nháp")]//Mentor tạo khóa học nháp
        Draft=1,
        [Display(Name="Chờ duyệt")]//Mentor đã submit khóa học, chờ duyệt
        Pending=2,
        [Display(Name="Chờ đủ mentee")]//Khóa học đã được duyệt chờ đủ mentee
        Waiting=3,
        [Display(Name="Cancel vì không đủ thành viên")]//Khóa học kết thúc do không đủ thành viên
        CancelNotEnough=4,
        [Display(Name = "Khóa học bắt đầu")]
        Start=5,
        [Display(Name = "Khóa học Kết thúc")]
        End=6,
    }

    public enum RoleEnum
    {
        Mentee = 1,
        Mentor = 2,
        Admin = 3,
    }

    public enum UserStatusEnum
    {
        UnActive = 0,
        Active = 1
    }
    
}