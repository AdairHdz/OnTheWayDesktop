namespace PresentationLayer.PresentationModels
{
    public class RegistryPresentationModel
    {
        public string Names { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public StatePresentationModel State { get; set; }
    }
}
