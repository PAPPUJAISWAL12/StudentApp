using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models;

public partial class Document
{
    public int DocId { get; set; }

    public int DocCategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public string DocFile { get; set; } = null!;
    [NotMapped]
    [DataType(DataType.Upload)]
    public IFormFile? FileUpload { get; set; }

    public virtual DocumentCategory DocCategory { get; set; } = null!;
}
