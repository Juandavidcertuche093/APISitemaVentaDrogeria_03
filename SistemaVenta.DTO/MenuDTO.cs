﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class MenuDTO
    {
        public int IdMenu { get; set; }
        public string? Nombre { get; set; }
        public string? Icono { get; set; }
        public string? Url { get; set; }
        public List<MenuDTO>? Submenus { get; set; } = new List<MenuDTO>();

    }
}
