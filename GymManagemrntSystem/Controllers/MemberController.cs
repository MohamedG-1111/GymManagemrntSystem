namespace GymManagemrntSystem.Controllers
{
    using GymManagementSystemBLL.Services.Interfaces;
    using GymManagementSystemBLL.ViewModels.Member;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class MemberController : Controller
    {
      
        private readonly IMemberServices memberServices;

 
        public MemberController(IMemberServices memberServices)
        {
            this.memberServices = memberServices;
        }


        public IActionResult Index()
        {
            var Data = memberServices.GetAllMembers();
            return View(Data);
        }

        public IActionResult MemberDetails(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var Member=memberServices.MemberDetails(id);
            if(Member == null)
            {
                TempData["ErrorMessage"] = "Not Found";

                return RedirectToAction(nameof(Index));

            }
            return View(Member);
        }
        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var MemberHealthRecord = memberServices.GetHealthRecord(id);
            if (MemberHealthRecord == null)
            {
                TempData["ErrorMessage"] = "Not Found";

                return RedirectToAction(nameof(Index));

            }
            return View(MemberHealthRecord);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateViewMemeberModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("IvaildData", "Check Data and Missing fields");
                return View(nameof(Create), CreatedMember);
                
            }
            if(memberServices.CreateMember(CreatedMember))
            {
                TempData["SuccessMessage"] = "Member Created Successfull";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Not Created";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = memberServices.MemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Not Found";

                return RedirectToAction(nameof(Index));

            }
            ViewBag.id = id;
            ViewBag.Name = Member.Name;
            return View();
        }
        public IActionResult DeletedConfirmed(int id)
        {
            var result = memberServices.RemoveMember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Deleted";
            }
            return RedirectToAction(nameof(Index));
        }




        public IActionResult EditMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

           
            var MemberToUpdate=memberServices.getMemberToUpdate(id);
            if (MemberToUpdate == null)
            {
                TempData["ErrorMessage"] = "Not Found";
                return RedirectToAction(nameof(Index));

            }

            return View(MemberToUpdate);
        }
        [HttpPost]
        public IActionResult EditMember([FromRoute]int id,MemberToUpdateModelView MemberToEdit)
        {
            if (!ModelState.IsValid)

                return View(MemberToEdit);
            var result = memberServices.UpdateMember(id, MemberToEdit);
            if (result)
            {
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated";
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
