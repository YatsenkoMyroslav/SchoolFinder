using SchoolFinder.Common.Abstraction;

namespace SchoolFinder.Common.Identity.Authentication.Registration
{
    public class RegistrationForm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public RegistrationFormType Type { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassWord { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public string PositionInSchool { get; set; } = string.Empty;
        public FileBytes DocumentApprove { get; set; } = new FileBytes();
        public RequestState State { get; set; } = RequestState.None;

        public RegistrationForm(RegistrationFormType type)
        {
            Type = type;
        }
    }
}
