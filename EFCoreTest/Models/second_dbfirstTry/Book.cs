﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Models.second_dbfirstTry;

[Index("AuthorId", Name = "IX_Books_AuthorId")]
[Index("LanguageId", Name = "IX_Books_LanguageId")]
public partial class Book
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int NoOfPages { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public int LanguageId { get; set; }

    public int? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author? Author { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BookPrice> BookPrices { get; set; } = new List<BookPrice>();

    [ForeignKey("LanguageId")]
    [InverseProperty("Books")]
    public virtual Language Language { get; set; } = null!;
}