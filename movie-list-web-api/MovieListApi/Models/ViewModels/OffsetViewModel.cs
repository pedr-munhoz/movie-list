using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Models.ViewModels;

public class OffsetViewModel
{
    public const int MaxLength = 30;

    [Range(0, int.MaxValue)]
    public int? Index { get; set; }

    [Range(0, MaxLength)]
    public int? Length { get; set; }
}
