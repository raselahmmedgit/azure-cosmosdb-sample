using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace lab.LocalCosmosDbApp.EntityModels
{
    public class Person //: Base
    {
        [Key]
        public string Id { get; set; }

        public string PersonId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)]
        public string PersonName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }

    }

    //public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    //{
    //    public void Configure(EntityTypeBuilder<Person> builder)
    //    {
    //        var iConfigurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    //        var containerName = iConfigurationRoot.GetValue<string>("AppDbConnectionConfig:ContainerName");

    //        builder.ToContainer(containerName);
    //        builder.HasKey(x => x.Id);
    //        builder.HasPartitionKey(x => x.Id);
    //    }
    //}
}

