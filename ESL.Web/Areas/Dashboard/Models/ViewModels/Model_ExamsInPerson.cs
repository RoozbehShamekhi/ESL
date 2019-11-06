﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESL.Web.Areas.Dashboard.Models.ViewModels
{
    public class Model_ExamsInPerson
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }

        [Display(Name = "آزمون")]
        public string Exam { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public string Description { get; set; }

        [Display(Name = "بها")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public int Cost { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "مکان")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public string Location { get; set; }

        [Display(Name = "فعال سازی")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public bool Activeness { get; set; }

        [Display(Name = "ظرفیت")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public int Capacity { get; set; }

        [Display(Name = "نمره")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public int TotalMark { get; set; }

        [Display(Name = "حداقل نمره قبولی")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public int PassMark { get; set; }

        [Display(Name = "تاریخ برگذاری")]
        public DateTime Date { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreationDate { get; set; }
    }
}