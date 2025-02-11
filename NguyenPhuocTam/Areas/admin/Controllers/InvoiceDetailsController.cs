﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NguyenPhuocTam.Models;

namespace NguyenPhuocTam.Areas.admin.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        private BanHang2Entities db = new BanHang2Entities();

        // GET: admin/InvoiceDetails
        public ActionResult Index()
        {
            var invoiceDetails = db.InvoiceDetails.Include(i => i.Invoice).Include(i => i.Product);
            return View(invoiceDetails.ToList());
        }

        // GET: admin/InvoiceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            if (invoiceDetail == null)
            {
                return HttpNotFound();
            }
            return View(invoiceDetail);
        }

        // GET: admin/InvoiceDetails/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Code");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Productname");
            return View();
        }

        // POST: admin/InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvoiceId,ProductId,Quantity,UnitPrice")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceDetails.Add(invoiceDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Code", invoiceDetail.InvoiceId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Productname", invoiceDetail.ProductId);
            return View(invoiceDetail);
        }

        // GET: admin/InvoiceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            if (invoiceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Code", invoiceDetail.InvoiceId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Productname", invoiceDetail.ProductId);
            return View(invoiceDetail);
        }

        // POST: admin/InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceId,ProductId,Quantity,UnitPrice")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "Code", invoiceDetail.InvoiceId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Productname", invoiceDetail.ProductId);
            return View(invoiceDetail);
        }

        // GET: admin/InvoiceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            if (invoiceDetail == null)
            {
                return HttpNotFound();
            }
            return View(invoiceDetail);
        }

        // POST: admin/InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            db.InvoiceDetails.Remove(invoiceDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
