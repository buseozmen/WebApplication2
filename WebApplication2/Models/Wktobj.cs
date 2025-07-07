using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("wktobj")]
public partial class Wktobj
{
    [Key]
    public int ObjectId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(300)]
    public string? Wkt { get; set; }
}
