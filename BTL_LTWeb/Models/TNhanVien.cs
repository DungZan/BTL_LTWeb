using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.Models;

public partial class TNhanVien
{
    public int MaNhanVien { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng")]
    public string Email { get; set; } = null!;

    [Display(Name = "Tên nhân viên")]
    [Required(ErrorMessage = "Tên nhân viên không được để trống")]
    public string? TenNhanVien { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateOnly? NgaySinh { get; set; }

    [Display(Name = "Chức vụ")]
    [Required(ErrorMessage = "Chức vụ không được để trống")]
    public string? ChucVu { get; set; }

    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    [ValidateNever]
    public virtual TUser UsernameNavigation { get; set; } = null!;
}
