﻿using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.Identity.Authentication.Registration
{
    public class RegistrationFormFilter : Pagination
    {
        public string? SearchText { get; set; }
        public RegistrationFormState State { get; set; } = RegistrationFormState.None;
        public RegistrationFormFieldIdentifier SortBy { get; set; } = RegistrationFormFieldIdentifier.State;
        public SortOrder OrderBy {  get; set; } = SortOrder.Ascending;
    }
}
