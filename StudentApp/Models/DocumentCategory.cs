using System;
using System.Collections.Generic;

namespace StudentApp.Models;

public partial class DocumentCategory
{
    public int DocCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
