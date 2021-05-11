﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Ordinario.Entidades
{
    public class Major
    {
        #region Propiedades
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        [StringLength(250)]
        public string Photo { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
