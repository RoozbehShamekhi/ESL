﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESL.Web.Areas.Dashboard.Models.ViewModels
{
    public class Model_UsersExamInPersonPlan
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public string User { get; set; }

        [Display(Name = "شماره صندلی")]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public int? SeatNumber { get; set; }

        [Display(Name = "نمره")]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public int? Mark { get; set; }

        [Display(Name = "وضعیت حضور")]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public bool IsPresent { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "وضعیت ثبت نام")]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public bool IsDelete { get; set; }
    }
}