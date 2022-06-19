// See https://aka.ms/new-console-template for more information

using System.Text;
using DataModels;
using MDEngine;

Console.WriteLine("testing db");


var db = new AppDbContext();

//Console.WriteLine("Creating a blog...");

//db.Add(new Blog("Test blog", new DateOnly(2022, 12, 6), "This is the excerpt", "Body stuffs"));


// try
// {
//     db.SaveChanges();
// }
// catch
// {
//     // Ignore
// }

// Console.WriteLine("Querying data...");
//
// var firstBlog = db.Blogs.First();
//
// Console.WriteLine("The first blog is: " + firstBlog.Title);
// Console.WriteLine("The date is created was at: " + firstBlog.DateCreated.Day);



var folder = "/Users/corycobb/RiderProjects/GoodEveningTech/TestingApp/test.md";
var desktopPath = Environment.SpecialFolder.Desktop;
var path = Environment.GetFolderPath(desktopPath);
var newFile = System.IO.Path.Join(path, "test.html");


var md = new Md(folder, newFile);

md.GenerateMd();

