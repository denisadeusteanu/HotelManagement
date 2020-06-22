using HotelManagement.Data;
using HotelManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HotelContext _context;

        public ReservationService(HotelContext context)
        {
            _context = context;
        }

        public void GreedyOptimizationResevations()
        {
            throw new NotImplementedException();
        }
    }
}
