using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

public class Blog
{
    /**
     * Title string length 200
     * Author: FK, string for now
     * Date_created just date
     * excerpt string length 200
     * tags: foreign key not required yet
     * body: text
     */


    public int Id { get; set; }

    [Required] [MaxLength(200)] public string Title { get; set; }

    [Required] public DateOnly DateCreated { get; set; }

    [Required] [MaxLength(200)] public string Excerpt { get; set; }

    [Required] [Column(TypeName = "text")] public string Body { get; set; }


    public Blog() {}
    
    public Blog(string title, DateOnly dateCreated, string excerpt, string body)
    {
        Title = title;
        DateCreated = dateCreated;
        Excerpt = excerpt;
        Body = body;
    }
    
    

}