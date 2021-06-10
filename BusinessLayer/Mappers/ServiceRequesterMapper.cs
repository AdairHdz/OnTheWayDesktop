using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;

namespace BusinessLayer.Mappers
{
    public class ServiceRequesterMapper
    {
        public static ServiceRequester CreateServiceRequesterFromUserOverviewDTO(UserOverviewDTO userOverviewDTO)
        {
            ServiceRequester serviceRequester = new ServiceRequester
            {
                ID = userOverviewDTO.ID,
                Names = userOverviewDTO.Name,
                Lastname = userOverviewDTO.LastName
            };
            return serviceRequester;
        }
    }
}