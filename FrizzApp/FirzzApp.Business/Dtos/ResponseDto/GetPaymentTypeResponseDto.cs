﻿using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Dtos.ResponseDto
{
    public class GetPaymentTypeResponseDto
    {
        public PaymentTypeEnum Id { get; set; }

        public string Nombre;
    }
}
