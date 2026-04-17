using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.DTOs
{
    public class GetParkingFeeResponseDto
    {
        public string TicketNumber { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string RateTypeName { get; set; } = null!;
        public DateTime EntryTime { get; set; }
        public DateTime CurrentTime { get; set; }
        public decimal EstimatedAmount { get; set; }
        public string SpaceNumber { get; set; } = null!;
    }
}
