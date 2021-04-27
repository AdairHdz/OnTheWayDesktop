﻿using System;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceRequest
    {
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public Address DeliveryAddress { get; set; }
        public string Description { get; set; }
        public bool HasBeenReviewed { get; set; }
        public KindOfService KindOfService { get; set; }
        public ServiceStatus ServiceStatus { get; set; }
        public ServiceRequester ServiceRequester { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
    }
}