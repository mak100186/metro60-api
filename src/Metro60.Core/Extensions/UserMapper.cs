using Metro60.Core.Entities;
using Metro60.Core.Models;

using Riok.Mapperly.Abstractions;

namespace Metro60.Core.Extensions;

[Mapper]
public static partial class UserMapper
{
    public static partial UserModel ToDto(this User user);
}
