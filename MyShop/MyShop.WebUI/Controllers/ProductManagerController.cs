using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.VIewModels;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategory;
        IRepository<ImagePath> productImages;
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext, IRepository<ImagePath> productImagesContext)
        {
            context = productContext;
            productCategory = productCategoryContext;
            productImages = productImagesContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategory.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategory.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id,HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            } 
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UploadImage(string Id)
        {
            ProductImagesViewModel obj = new ProductImagesViewModel();
            obj.ProductId = Id;
            obj.ProductName = context.Collection().FirstOrDefault(x => x.Id == Id).Name;
            obj.ImageUrl = productImages.Collection().Where(x => x.ProductId == Id).Select(y => y.ImagesPath).ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult UploadImage(ProductImagesViewModel model, HttpPostedFileBase productImage)
        {
            //if (productImage != null)
            //{
            //    System.Drawing.Image img = System.Drawing.Image.FromStream(productImage.InputStream);
            //    if ((img.Width != 800) || (img.Height != 356))
            //    {
            //        ModelState.AddModelError("", "Image resolution must be 800 x 356 pixels");
            //        return View();
            //    }
            //}
            ImagePath insertValue = new ImagePath();
            insertValue.ProductId = model.ProductId;
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (productImage != null)
                {
                    model.ImagePath = model.ProductId + Path.GetExtension(productImage.FileName);
                    productImage.SaveAs(Server.MapPath("//Content//Images//") + model.ImagePath);
                }
                insertValue.ImagesPath = model.ImagePath;
                productImages.Insert(insertValue);
                productImages.Commit();
                return RedirectToAction("UploadImage","ProductManager",new { Id =model.ProductId});
            }
        }

    }
}