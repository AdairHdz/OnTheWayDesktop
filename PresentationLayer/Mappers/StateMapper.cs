using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public static class StateMapper
    {
        public static List<StatePresentationModel> CreateListOfStatePrecentationModel(List<State> statesList)
        {
            List<StatePresentationModel> listOfStatePresentationModels = new List<StatePresentationModel>();
            statesList.ForEach(stateEntity =>
            {
                StatePresentationModel statePresentationModel = new StatePresentationModel
                {
                    ID = stateEntity.ID,
                    Name = stateEntity.Name
                };

                listOfStatePresentationModels.Add(statePresentationModel);
            });
            return listOfStatePresentationModels;
        }

        public static State CreateStateEntity(StatePresentationModel statePresentationModel)
        {
            State state = new State
            {
                ID = statePresentationModel.ID,
                Name = statePresentationModel.Name
            };

            return state;
        }
    }
}
