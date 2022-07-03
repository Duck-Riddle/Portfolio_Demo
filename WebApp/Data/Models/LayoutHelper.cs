namespace WebApp.Data.Models;

public static class LayoutDataHelper
{
    public static RedirectLink Project = new RedirectLink
    {
        Path = "Projects",
        Label = "Projects"
    };

    public static List<RedirectLink> ProjectsLinks = new List<RedirectLink>(){
        new RedirectLink
        {
            Path = $"/{Project.Path}/ToDoList",
            Label = "Tasks To Do"
        },
        new RedirectLink
        {
            Path = $"/{Project.Path}/Battleships",
            Label = "Battleships Game"
        }
    };

}