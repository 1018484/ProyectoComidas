﻿using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IMessageRemotoRepository
    {
        Task SendMessageAsync(TWMessage sms);
    }
}