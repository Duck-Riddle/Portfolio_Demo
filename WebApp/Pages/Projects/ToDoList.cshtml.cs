using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Pages.Projects;
[Authorize]
public class ToDoListModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityAppUser> _userManager;

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    [BindProperty]
    public TaskToDo SelectedTask { get; set; }

    [BindProperty] public List<TaskToDo> TaskToDoList { get; set; }
    public ToDoListModel(AppDbContext db, UserManager<IdentityAppUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }
    public async Task OnGetAsync()
    {

        var user = await GetCurrentUserAsync();

        TaskToDoList = await LoadData(user);
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {

        if (!ModelState.IsValid) return RedirectToPage(this.RouteData.Values);

        var appUser = await GetCurrentUserAsync();

        var alias = await _db.Aliases.FirstAsync(alias => alias.IsGroup == false && alias.Users.Contains(appUser));

        alias.TasksToDo.Add(SelectedTask);
        await _db.SaveChangesAsync();


        return RedirectToPage(this.RouteData.Values);
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        var task = await _db.TasksToDo.FindAsync(id);

        _db.TasksToDo.Remove(task);
        await _db.SaveChangesAsync();

        return RedirectToPage(this.RouteData.Values);
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id)
    {
        return RedirectToPage(this.RouteData.Values);
    }

    public async Task<IActionResult> OnPostCompleteAsync(Guid id)
    {
        var task = await _db.TasksToDo.FindAsync(id);

        task.IsCompleted = !task.IsCompleted;

        await _db.SaveChangesAsync();

        return RedirectToPage(this.RouteData.Values);
    }

    private Task<IdentityAppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    private async Task<List<TaskToDo>> LoadData(IdentityAppUser user)
    {

        await _db.Entry(user).Collection(u => u.Aliases).LoadAsync();

        var alias = user.Aliases.First(a => a.IsGroup == false);

        await _db.Entry(alias).Collection(a => a.TasksToDo).LoadAsync();

        return alias.TasksToDo.ToList();
    }

}
