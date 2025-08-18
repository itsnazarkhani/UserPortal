# User Portal Database Schema

این سند ساختار پایگاه داده برنامه `User Portal` را توضیح می‌دهد.

## ساختار کلی

پایگاه داده این برنامه بر اساس Entity Framework Core و ASP.NET Core Identity طراحی شده است.

### جدول ApplicationUser
موجودیت اصلی برنامه که از `IdentityUser` ارث‌بری می‌کند و شامل فیلدهای زیر است:

```
ApplicationUser
├── Id (string, Primary Key)
├── UserName (string)
├── NormalizedUserName (string)
├── Email (string)
├── NormalizedEmail (string)
├── EmailConfirmed (bool)
├── PasswordHash (string)
├── SecurityStamp (string)
├── ConcurrencyStamp (string)
├── PhoneNumber (string)
├── PhoneNumberConfirmed (bool)
├── TwoFactorEnabled (bool)
├── LockoutEnd (DateTimeOffset?)
├── LockoutEnabled (bool)
└── AccessFailedCount (int)
```

### جداول Identity
سایر جداول مرتبط با Identity:
- AspNetRoles: نقش‌های کاربری
- AspNetUserRoles: ارتباط بین کاربران و نقش‌ها
- AspNetUserClaims: claims کاربران
- AspNetUserLogins: اطلاعات ورود کاربران
- AspNetUserTokens: توکن‌های کاربران

## نمودارهای ERD

### Chen Diagram
نمایش روابط موجودیت‌ها به روش Chen:

![chen diagram](./user_portal_chenERD.svg)

### Crow's Foot Diagram
نمایش روابط موجودیت‌ها به روش Crow's Foot:

![crow's foot diagram](./user_portal_crow'sFootERD.drawio.svg)

## نکات پیاده‌سازی
1. از SQL Server به عنوان RDBMS استفاده شده است
2. تصاویر پروفایل در فایل سیستم ذخیره می‌شوند و مسیر آن‌ها در دیتابیس نگهداری می‌شود
3. از Code-First Migration برای مدیریت تغییرات schema استفاده می‌شود

---
Note: نمودارهای ERD با استفاده از [draw.io](https://draw.io) ایجاد شده‌اند.