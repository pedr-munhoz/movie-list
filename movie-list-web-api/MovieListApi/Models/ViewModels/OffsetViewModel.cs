using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Models.ViewModels;

public class OffsetViewModel
{
    [Range(0, int.MaxValue)]
    public int? Index { get; set; }

    [Range(0, 30)]
    public int? Length { get; set; }
}
