using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Metro60.Core.Entities;

[DataContract]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //note: this doesn't work for the file based database. provide it manually
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    //todo: salt/has this password and then save it
    [JsonIgnore]
    public string Password { get; set; }
}
