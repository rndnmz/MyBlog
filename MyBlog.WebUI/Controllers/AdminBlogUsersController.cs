using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;
using MyBlog.WebUI.Data;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.Controllers
{
    public class AdminBlogUsersController : Controller
    {
        BlogUserManager _blogUserManager = new BlogUserManager();

       

        // GET: AdminBlogUsers
        public  IActionResult Index()
        {
              return View(_blogUserManager.List());
        }

        // GET: AdminBlogUsers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var blogUser = _blogUserManager.Find(m => m.Id == id);
            if (blogUser == null)
            {
                return NotFound();
            }

            return View(blogUser);
        }

        // GET: AdminBlogUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBlogUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogUser blogUser)
        {
            ModelState.Remove("Notes");
            ModelState.Remove("Likes");
            ModelState.Remove("Comments");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("UserProfileImage");
         
            if (ModelState.IsValid)
            {
                blogUser.ModifiedUserName = CurrentSession.CurrentUser.UserName;
                blogUser.UserProfileImage = "user-profile.jpg";
                blogUser.CreatedDate= DateTime.Now;
                blogUser.ModifiedDate= DateTime.Now;
                blogUser.ActivateGuid = Guid.NewGuid();
                _blogUserManager.Insert(blogUser);
                
                return RedirectToAction(nameof(Index));
            }
            return View(blogUser);
        }

        //GET: AdminBlogUsers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var blogUser = _blogUserManager.GetById(id.Value);
            if (blogUser == null)
            {
                return NotFound();
            }
            return View(blogUser);
        }

        //// POST: AdminBlogUsers/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogUser blogUser)
        {
            // Kullanıcıyı veritabanında güncelleyen kodlar yazılacak.
            // Aşağıdaki satırlar ile ilgili alanların Validation işlemlerini ModelState için yapmayacak.
            ModelState.Remove("Notes");
            ModelState.Remove("Comments");
            ModelState.Remove("Likes");
            if (ModelState.IsValid)
            {
               
                // Artık User nesnesini güncellemek için gerekli kodları yazabiliriz.

                BusinessLayerResult<BlogUser> blResult = _blogUserManager.UpdateProfile(blogUser);
                if (blResult.Errors.Count > 0)
                {
                    // hata varsa buraya gireceğiz.
                    blResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(blogUser);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogUser);
        }

        //// GET: AdminBlogUsers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.BlogUser == null)
        //    {
        //        return NotFound();
        //    }

        //    var blogUser = await _context.BlogUser
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (blogUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(blogUser);
        //}

        //// POST: AdminBlogUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.BlogUser == null)
        //    {
        //        return Problem("Entity set 'MyBlogWebUIContext.BlogUser'  is null.");
        //    }
        //    var blogUser = await _context.BlogUser.FindAsync(id);
        //    if (blogUser != null)
        //    {
        //        _context.BlogUser.Remove(blogUser);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
