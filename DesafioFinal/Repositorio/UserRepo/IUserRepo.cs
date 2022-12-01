﻿using DesafioFinal.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.UserRepo
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> Get();
        Task<User> Create(User user);
            
        Task<User> Save(User user);
    }
}
