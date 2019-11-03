﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESL.Web.Areas.Dashboard.Models.ViewModels
{
    public class Model_UserExamInPersonCreate
    {
        [Display(Name = "شناسه آزمون")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public int ExamID { get; set; }

        [Display(Name = "شناسه کاربر")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public Guid UserGuid { get; set; }
    }
}