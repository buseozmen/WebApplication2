using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace WebApplication2.Models;

[Table("wktobj")]
public partial class Wktobj
{
    [Key]
    public int ObjectId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public Geometry Wkt { get; set; }
}
