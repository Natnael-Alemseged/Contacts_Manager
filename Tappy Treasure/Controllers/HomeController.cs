using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tappy_Treasure.Data;
using Tappy_Treasure.Models;

namespace Tappy_Treasure.Controllers
{
	public class HomeController : Controller
	{

		private readonly myDBContext applicationDbContext;


		public class HomeViewModel
		{
			public required string message { get; set; }
		}

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, myDBContext applicationDbContext)
		{
			_logger = logger;
			this.applicationDbContext = (Tappy_Treasure.Data.myDBContext)applicationDbContext;
		}

		public IActionResult Index()
		{
			var viewModel = new HomeViewModel { message = "hello" };
			ViewData["Message"] = "hello";



			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> ViewContacts()
		{
			var Contacts =await  applicationDbContext.Contacts.ToListAsync();
			System.Diagnostics.Debug.WriteLine(Contacts.Count);
			
			return View(Contacts);
		}


		[HttpPost]
		public async Task<IActionResult> Contact(Contact form)
		{

			System.Diagnostics.Debug.WriteLine(form.Name);
			System.Diagnostics.Debug.WriteLine(form.Email);
			System.Diagnostics.Debug.WriteLine(form.Message);
			if (string.IsNullOrEmpty(form.Email))
			{
				System.Diagnostics.Debug.WriteLine("aza email new beya");
				ModelState.AddModelError("Name", "Name is required");

			}

			if (ModelState.IsValid)
			{
				var newContact = new Contact
				{
					Name = form.Name,
					Email = form.Email,
					Message = form.Message
				};

				try
				{
					applicationDbContext.Contacts.Add(newContact);
					await applicationDbContext.SaveChangesAsync();
					// Use async/await for asynchronous saving
					return View("ContactSuccess");
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"{ex.Message}");
					return View("Error");

				}
			}

			return View("ContactFail");


		}


		[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

			var contact = await applicationDbContext.Contacts.FindAsync(id);

            return View(contact);

		}
		[HttpPost]
		public async Task<IActionResult> Edit(Contact editedContact  ) { 
		
		var contact= await applicationDbContext.Contacts.FindAsync(editedContact.Id);

			if (contact != null) { 
			
			contact.Name = editedContact.Name;
				contact.Email = editedContact.Email;
				contact.Message = editedContact.Message;

				await applicationDbContext.SaveChangesAsync();
			}
			return RedirectToAction("ViewContacts", "Home");
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id) { 
		
		var contactToDelete=await applicationDbContext.Contacts.FindAsync(id);

			if (contactToDelete != null)
			{
				 applicationDbContext.Contacts.Remove(contactToDelete);
				applicationDbContext.SaveChanges();
                
            }
			return RedirectToAction("ViewContacts", "Home");
			
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

}
