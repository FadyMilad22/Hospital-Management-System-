﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

using System;
using System.Collections.Generic;

namespace HospitalManagementSystem2.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }


    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}