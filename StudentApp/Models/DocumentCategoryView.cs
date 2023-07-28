using System;
using System.Collections.Generic;

namespace StudentApp.Models;

public partial class DocumentCategoryView
{
    public string CategoryName { get; set; } = null!;

    public int DocId { get; set; }

    public int DocCategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public string DocFile { get; set; } = null!;
}
