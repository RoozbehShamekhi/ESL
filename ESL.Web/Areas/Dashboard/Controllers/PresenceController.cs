﻿using ESL.Common.Plugins;
using ESL.DataLayer.Domain;
using ESL.Services.BaseRepository;
using ESL.Web.Areas.Dashboard.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ESL.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PresenceController : Controller
    {
        private ESLEntities db = new ESLEntities();

        public ActionResult Index()
        {
            var q = db.Tbl_UserClass.Where(x => x.UC_IsDelete == false).Select(x => new Model_UserClasses
            {
                ID = x.UC_ID,
                User = x.Tbl_User.User_FirstName + " " + x.Tbl_User.User_lastName,
                Class = x.Tbl_ClassPlan.Tbl_Class.Class_Title,
                Type = x.Tbl_ClassPlan.Tbl_Code.Code_Display,
                Location = x.Tbl_ClassPlan.CP_Location,
                Time = x.Tbl_ClassPlan.CP_Time,
                CreationDate = x.UC_CreationDate

            }).ToList();

            return View(q);
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue && db.Tbl_UserClass.Any(x => x.UC_ID == id))
            {
                var p = db.Tbl_UserClass.Where(x => x.UC_IsDelete == false && x.UC_ID == id).SingleOrDefault();

                var q = db.Tbl_UserClassPresence.Where(x => x.UCP_IsDelete == false && x.Tbl_UserClass.UC_UserID == p.UC_UserID && x.Tbl_UserClass.UC_CPID == p.UC_CPID && x.Tbl_UserClass.Tbl_ClassPlan.CP_TypeCodeID == p.Tbl_ClassPlan.CP_TypeCodeID && x.Tbl_UserClass.Tbl_ClassPlan.CP_Location == p.Tbl_ClassPlan.CP_Location && x.Tbl_UserClass.Tbl_ClassPlan.CP_Time == p.Tbl_ClassPlan.CP_Time).Select(x => new Model_UserClassPresence
                {
                    ID = x.UCP_ID,
                    Cost = x.Tbl_Payment.Payment_Cost,
                    Discount = x.Tbl_Payment.Payment_Discount,
                    Presence = x.UCP_IsPresent,
                    CreationDate = x.UCP_CreationDate

                }).ToList();

                TempData["UserClassID"] = id;

                return View(q);
            }

            return HttpNotFound();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model_UserClassPresenceCreate model)
        {
            if (ModelState.IsValid)
            {
                int _UserClassID = (int)TempData["UserClassID"];

                var _UserClass = db.Tbl_UserClass.Where(x => x.UC_IsDelete == false && x.UC_ID == _UserClassID).SingleOrDefault();

                int _Credit = db.Tbl_Wallet.Where(x => x.Wallet_UserID == _UserClass.UC_UserID).SingleOrDefault().Wallet_Credit;

                Tbl_Payment q = new Tbl_Payment()
                {
                    Payment_Guid = Guid.NewGuid(),
                    Payment_UserID = _UserClass.UC_UserID,
                    Payment_TitleCodeID = (int)PaymentTitle.Presence,
                    Payment_WayCodeID = (int)PaymentWay.InPerson,
                    Payment_TypeCodeID = (int)PaymentType.Paid,
                    Payment_Cost = _UserClass.Tbl_ClassPlan.CP_CostPerSession,
                    Payment_Discount = model.Discount,
                    Payment_RemaingWallet = _Credit - _UserClass.Tbl_ClassPlan.CP_CostPerSession + model.Discount,
                    Payment_TrackingToken = "ESL-" + new Random().Next(100000, 999999).ToString(),
                    Payment_CreateDate = DateTime.Now,
                    Payment_ModifiedDate = DateTime.Now
                };

                Tbl_UserClassPresence p = new Tbl_UserClassPresence
                {
                    UCP_Guid = Guid.NewGuid(),
                    UCP_UCID = _UserClassID,
                    UCP_IsPresent = model.Presence,
                    UCP_Date = DateConverter.ToGeorgianDateTime(model.Date),
                    UCP_CreationDate = DateTime.Now,
                    UCP_ModifiedDate = DateTime.Now
                };

                p.Tbl_Payment = q;

                db.Tbl_Payment.Add(q);
                db.Tbl_UserClassPresence.Add(p);

                if (Convert.ToBoolean(db.SaveChanges() > 0))
                {
                    TempData["TosterState"] = "success";
                    TempData["TosterType"] = TosterType.Maseage;
                    TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                    return RedirectToAction("Details", new { id = _UserClassID });
                }
                else
                {
                    TempData["TosterState"] = "error";
                    TempData["TosterType"] = TosterType.Maseage;
                    TempData["TosterMassage"] = "عملیات با موفقیت انجام نشده";

                    return RedirectToAction("Details", new { id = _UserClassID });
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Model_MessageModal model = new Model_MessageModal();

                var q = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == id).SingleOrDefault();

                if (q != null)
                {
                    model.ID = id.Value;
                    model.Name = q.Tbl_UserClass.Tbl_User.User_FirstName + " " + q.Tbl_UserClass.Tbl_User.User_lastName;
                    model.Description = "آیا از حذف حضور مورد نظر اطمینان دارید ؟";

                    return PartialView(model);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Model_MessageModal model)
        {
            if (ModelState.IsValid)
            {
                var q = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault();

                if (q != null)
                {
                    q.UCP_IsDelete = true;

                    db.Entry(q).State = EntityState.Modified;

                    if (Convert.ToBoolean(db.SaveChanges() > 0))
                    {
                        TempData["TosterState"] = "success";
                        TempData["TosterType"] = TosterType.Maseage;
                        TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                        return RedirectToAction("Details", "Presence", new { area = "Dashboard", id = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault().Tbl_UserClass.UC_ID });
                    }
                    else
                    {
                        TempData["TosterState"] = "error";
                        TempData["TosterType"] = TosterType.Maseage;
                        TempData["TosterMassage"] = "عملیات با موفقیت انجام نشده";

                        return RedirectToAction("Details", "Presence", new { area = "Dashboard", id = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault().Tbl_UserClass.UC_ID });
                    }
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult SetPresence(int id)
        {
            var q = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == id).SingleOrDefault();

            if (q != null)
            {
                Model_SetActiveness model = new Model_SetActiveness()
                {
                    ID = id,
                    Activeness = q.UCP_IsPresent
                };

                return PartialView(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPresence(Model_SetActiveness model)
        {
            if (ModelState.IsValid)
            {
                var q = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault();

                if (q != null)
                {
                    q.UCP_IsPresent = model.Activeness;

                    db.Entry(q).State = EntityState.Modified;

                    if (Convert.ToBoolean(db.SaveChanges() > 0))
                    {
                        TempData["TosterState"] = "success";
                        TempData["TosterType"] = TosterType.Maseage;
                        TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                        return RedirectToAction("Details", "Presence", new { area = "Dashboard", id = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault().Tbl_UserClass.UC_ID });
                    }
                    else
                    {
                        TempData["TosterState"] = "error";
                        TempData["TosterType"] = TosterType.Maseage;
                        TempData["TosterMassage"] = "عملیات با موفقیت انجام نشده";

                        return RedirectToAction("Details", "Presence", new { area = "Dashboard", id = db.Tbl_UserClassPresence.Where(x => x.UCP_ID == model.ID).SingleOrDefault().Tbl_UserClass.UC_ID });
                    }
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}