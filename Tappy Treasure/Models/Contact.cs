using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts_Manager.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public required string Name { get; set; }
        [Required][EmailAddress] public required string Email { get; set; }
        [Required][StringLength(maximumLength: 200, MinimumLength = 10)] public required string Message { get; set; }
    }





}
