﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей "Events.EventsCategories"
    /// </summary>
    [Table("EventsCategories", Schema = "Events")]
    public class EventsCategoryEntity
    {
        [Key]
        [Column("EventsCategoryId", TypeName = "uuid")]
        public Guid EventsCategotyId { get; set; }

        [Column("CategoryName", TypeName = "nvarchar(150)")]
        public string CategoryName { get; set; }
    }
}
