﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Metro60.Core.Entities;

[DataContract]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //note: this doesn't work for the file based database. provide it manually
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;

    //note: in a real world scenario, we would use a salt so that the passwords are not readable. bcrypt is a good algorithm for this purpose. 
    [JsonIgnore]
    public string Password { get; set; } = null!;
}
