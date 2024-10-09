using System;
using System.ComponentModel.DataAnnotations;

namespace HangfireDemoApi.Models
{
    public sealed class Job
    {
        [Key]
        public required Guid Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public string? CronExpression { get; set; }

        public string? InternalId { get; set; }
    }
}
