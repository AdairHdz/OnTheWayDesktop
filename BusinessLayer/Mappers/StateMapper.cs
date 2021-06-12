using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public static class StateMapper
    {
        public static StateDTO CreateStateDAO(State state)
        {
            StateDTO stateDAO = new StateDTO
            {
                ID = state.ID,
                Name = state.Name
            };
            return stateDAO;
        }

        public static State CreateState(StateDTO stateDAO)
        {
            State state = new State
            {
                ID = stateDAO.ID,
                Name = stateDAO.Name
            };
            return state;
        }

        public static List<State> CreateStatesList(List<StateDTO> stateDAOs)
        {
            List<State> statesList = new List<State>();
            stateDAOs.ForEach(stateDAO =>
            {
                statesList.Add(CreateState(stateDAO));
            });
            return statesList;
        }
    }
}
